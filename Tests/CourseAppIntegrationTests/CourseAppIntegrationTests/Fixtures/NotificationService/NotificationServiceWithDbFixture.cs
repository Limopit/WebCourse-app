using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Xunit;

namespace CourseAppIntegrationTests.Fixtures.NotificationService;

public class NotificationServiceWithDbFixture: IAsyncLifetime
{
    private readonly INetwork _network = new NetworkBuilder()
        .WithName(Guid.NewGuid().ToString("D"))
        .Build();
    public RedisFixture RedisFixture { get; private set; } = null!;
    public NotificationServiceFixture NotificationServiceFixture { get; private set; } = null!;
    public RabbitMqFixture RabbitMqFixture { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        RedisFixture = new RedisFixture(_network);
        await RedisFixture.InitializeAsync();
        
        RabbitMqFixture = new RabbitMqFixture(_network);
        await RabbitMqFixture.InitializeAsync();
        
        NotificationServiceFixture = new NotificationServiceFixture(_network);
        await NotificationServiceFixture.InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        await _network.DeleteAsync();
        await RedisFixture.DisposeAsync();
        await RabbitMqFixture.DisposeAsync();
        await NotificationServiceFixture.DisposeAsync();
    }
}