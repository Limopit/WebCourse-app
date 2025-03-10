using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Xunit;

namespace CourseAppIntegrationTests.Fixtures.UserService;

public class UserServiceWithDbFixture : IAsyncLifetime
{
    private readonly INetwork _network;
    public SqlDbFixture SqlDbFixture { get; private set; } = null!;
    public UserServiceFixture UserServiceFixture { get; private set; } = null!;
    private ElasticFixture ElasticFixture { get; set; } = null!;

    public UserServiceWithDbFixture()
    {
        _network = new NetworkBuilder()
            .WithName(Guid.NewGuid().ToString("D"))
            .Build();
    }

    public async Task InitializeAsync()
    {
        ElasticFixture = new ElasticFixture(_network);
        await ElasticFixture.InitializeAsync();
        
        SqlDbFixture = new SqlDbFixture(_network);
        await SqlDbFixture.InitializeAsync();

        UserServiceFixture = new UserServiceFixture(SqlDbFixture, _network);
        await UserServiceFixture.InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        await UserServiceFixture.DisposeAsync();
        await UserServiceFixture.DisposeAsync();
        await SqlDbFixture.DisposeAsync();
        await _network.DeleteAsync();
    }
}