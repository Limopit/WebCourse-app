using CourseAppUserService_Domain.Entities;
using CourseAppUserService_Persistance;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using Xunit;

public class SqlDbFixture : IAsyncLifetime
{
    private const string DatabaseName = "UserServiceDb";
    private readonly INetwork _network;

    private MsSqlContainer MsSqlContainer { get; set; }
    public UserServiceDbContext DbContext { get; set; } = null!;
    public UserManager<User> UserManager { get; set; } = null!;
    private RoleManager<IdentityRole> RoleManager { get; set; } = null!;

    public string ConnectionString { get; private set; } = null!;

    public SqlDbFixture(INetwork network)
    {
        _network = network;

        MsSqlContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("StrongPassword123")
            .WithNetwork(_network)
            .WithNetworkAliases("mssql")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await MsSqlContainer.StartAsync();

        ConnectionString = $"{MsSqlContainer.GetConnectionString().Replace("Database=master", $"Database={DatabaseName}")}";

        await Task.Delay(TimeSpan.FromSeconds(10));
        
        await EnableSaAccount();

        await CreateDatabaseIfNotExists(ConnectionString);

        var options = new DbContextOptionsBuilder<UserServiceDbContext>()
            .UseSqlServer(ConnectionString, x => x.MigrationsAssembly("CourseAppUserService_Infrastructure"))
            .Options;

        DbContext = new UserServiceDbContext(options);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        serviceCollection.AddDbContext<UserServiceDbContext>(opt =>
            opt.UseSqlServer(ConnectionString, x => x.MigrationsAssembly("CourseAppUserService_Infrastructure")));
        serviceCollection.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<UserServiceDbContext>()
            .AddDefaultTokenProviders();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
        RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        try
        {
            await DbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying migrations: {ex.Message}");
            throw;
        }

        await DbInitializer.Initialize(DbContext, UserManager, RoleManager); 
    }

    private async Task EnableSaAccount()
    {
        var masterConnectionString = MsSqlContainer.GetConnectionString();
        using (var connection = new SqlConnection(masterConnectionString))
        {
            await connection.OpenAsync();

            var enableSaCommand = new SqlCommand(
                "ALTER LOGIN sa ENABLE; " +
                "ALTER LOGIN sa WITH PASSWORD = 'StrongPassword123';", 
                connection);

            await enableSaCommand.ExecuteNonQueryAsync();
        }
    }

    private async Task CreateDatabaseIfNotExists(string connectionString)
    {
        var masterConnectionString = connectionString.Replace($"Database={DatabaseName}", "Database=master");

        using (var connection = new SqlConnection(masterConnectionString))
        {
            await connection.OpenAsync();

            var command = new SqlCommand(
                $"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{DatabaseName}') CREATE DATABASE [{DatabaseName}];", 
                connection);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task DisposeAsync()
    {
        await MsSqlContainer.DisposeAsync();
    }
}