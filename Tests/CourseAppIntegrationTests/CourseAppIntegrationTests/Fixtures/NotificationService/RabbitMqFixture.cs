using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Xunit;

namespace CourseAppIntegrationTests.Fixtures.NotificationService;

public class RabbitMqFixture : IAsyncLifetime
{
    private INetwork _network;
    private IContainer RabbitMqContainer { get; set; }

    public RabbitMqFixture(INetwork network)
    {
        _network = network;
        RabbitMqContainer = new ContainerBuilder()
            .WithImage("rabbitmq:3-management")
            .WithName("rabbitmq")
            .WithPortBinding(5672, 5672)
            .WithPortBinding(15672, 15672)
            .WithEnvironment("RABBITMQ_DEFAULT_USER", "admin")
            .WithEnvironment("RABBITMQ_DEFAULT_PASS", "admin")
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilPortIsAvailable(5672))
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilMessageIsLogged("Server startup complete"))
            .WithNetwork(_network)
            .WithNetworkAliases("rabbitmq")
            .Build();
    }
    
    public async Task InitializeAsync()
    {
        await RabbitMqContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await RabbitMqContainer.StopAsync();
        await RabbitMqContainer.DisposeAsync();
    }
}