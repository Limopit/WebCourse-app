using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Xunit;

namespace CourseAppIntegrationTests.Fixtures;

public class ElasticFixture: IAsyncLifetime
{
    private IContainer ElasticsearchContainer { get; set; }
    private INetwork _network;

    public ElasticFixture(INetwork network)
    {
        _network = network;
        
        ElasticsearchContainer = new ContainerBuilder()
            .WithImage("elasticsearch:8.17.1")
            .WithName("elasticsearch")
            .WithEnvironment("discovery.type", "single-node")
            .WithEnvironment("xpack.security.enabled", "false")
            .WithPortBinding(9200, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(9200))
            .WithNetwork(_network)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await ElasticsearchContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await ElasticsearchContainer.DisposeAsync();
    }
}