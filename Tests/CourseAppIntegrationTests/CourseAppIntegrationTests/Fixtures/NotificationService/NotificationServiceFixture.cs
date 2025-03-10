using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Xunit;

namespace CourseAppIntegrationTests.Fixtures.NotificationService;

public class NotificationServiceFixture: IAsyncLifetime
{
    private IContainer WebApiNotificationContainer { get; set; }

    private readonly INetwork _network;

    public NotificationServiceFixture(INetwork network)
    {
        _network = network;
        WebApiNotificationContainer = new ContainerBuilder()
            .WithImage("webcourse-app-webapi_notification:latest")
            .WithNetworkAliases("webapi_notification")
            .WithPortBinding(5004, 5004)
            .WithEnvironment("DOTNET_ENVIRONMENT", "Docker")
            .WithEnvironment("ASPNETCORE_Kestrel__Certificates__Default__Path", "/https/cert.crt")
            .WithEnvironment("ASPNETCORE_Kestrel__Certificates__Default__KeyPath", "/https/cert.key")
            .WithBindMount(Path.GetFullPath("../../../../../../certs"), "/https", AccessMode.ReadOnly)
            .WithOutputConsumer(Consume.RedirectStdoutAndStderrToConsole())
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5004))
            .WithNetwork(_network)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _network.CreateAsync();
        await WebApiNotificationContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await WebApiNotificationContainer.DisposeAsync();
        await _network.DeleteAsync();
    }
}