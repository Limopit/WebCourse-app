using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Xunit;

namespace CourseAppIntegrationTests.Fixtures.NotificationService;

public class RedisFixture : IAsyncLifetime
{
    private INetwork _network;
    private IContainer RedisContainer { get; set; }
    public string ConnectionString { get; private set; }

    public RedisFixture(INetwork network)
    {
        _network = network;
        RedisContainer = new ContainerBuilder()
            .WithImage("redis:latest")
            .WithName($"redis-{Guid.NewGuid()}")
            .WithPortBinding(6379, 6379)
            .WithCommand("redis-server", "--appendonly", "no")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6379))
            .WithNetwork(_network)
            .WithNetworkAliases("redis")
            .Build();
        
    }

    public async Task InitializeAsync()
    {
        await RedisContainer.StartAsync();
        ConnectionString = $"{RedisContainer.Hostname}:{RedisContainer.GetMappedPublicPort(6379)}";
    }

    public async Task DisposeAsync()
    {
        await RedisContainer.StopAsync();
        await RedisContainer.DisposeAsync();
    }
}