using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Xunit;

public class UserServiceFixture : IAsyncLifetime
{
    private readonly SqlDbFixture _sqlDbFixture;
    public string port;
    private IContainer WebApiUserContainer { get; set; } = null!;
    private readonly INetwork _network;

    public UserServiceFixture(SqlDbFixture sqlDbFixture, INetwork network)
    {
        _network = network;
        _sqlDbFixture = sqlDbFixture;

        WebApiUserContainer = new ContainerBuilder()
            .WithImage("webcourse-app-webapi_user:latest")
            .WithNetworkAliases("webapi_user")
            .WithPortBinding(5002, 5002)
            .WithEnvironment("DOTNET_ENVIRONMENT", "Docker")
            .WithEnvironment("IDENTITY_CONFIG_PATH", "appsettings.Identity.Docker.json")
            .WithEnvironment("ASPNETCORE_Kestrel__Endpoints__Grpc__Protocols", "Http2")
            .WithEnvironment("DbConnection", _sqlDbFixture.ConnectionString)
            .WithEnvironment("ASPNETCORE_Kestrel__Certificates__Default__Path", "/https/cert.crt")
            .WithEnvironment("ASPNETCORE_Kestrel__Certificates__Default__KeyPath", "/https/cert.key")
            .WithBindMount(Path.GetFullPath("../../../../../../certs"), "/https", AccessMode.ReadOnly)
            .WithOutputConsumer(Consume.RedirectStdoutAndStderrToConsole())
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5002))
            .WithNetwork(_network)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _network.CreateAsync();
        await WebApiUserContainer.StartAsync();
        port = WebApiUserContainer.GetMappedPublicPort(5002).ToString();
    }

    public async Task DisposeAsync()
    {
        await WebApiUserContainer.DisposeAsync();
        await _network.DeleteAsync();
    }
}