using System.Net.Http.Json;
using CourseAppIntegrationTests.Fixtures.UserService;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CourseAppIntegrationTests.ContainerTests;

public class UserServiceContainerTests(UserServiceWithDbFixture fixture, ITestOutputHelper testOutputHelper) : IClassFixture<UserServiceWithDbFixture>
{
    [Fact]
    public async Task TestLogin()
    {
        using var client = new HttpClient();
        HttpResponseMessage? response = null;
        var retryCount = 0;
        while (retryCount < 5)
        {
            var login = new
            {
                email = "admin@gmail.com",
                password = "Admin123#"
            };
            response = await client.PostAsJsonAsync($"https://localhost:{fixture.UserServiceFixture.port}/api/Auth/login", login);
            if (response.IsSuccessStatusCode)
            {
                break;
            }
            await Task.Delay(1000);
            retryCount++;
        }

        testOutputHelper.WriteLine($"Status Code: {response.StatusCode}");
        var responseBody = await response.Content.ReadAsStringAsync();
        testOutputHelper.WriteLine($"Response Body: {responseBody}");
        
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task TestRegistration()
    {
        using var client = new HttpClient();
        HttpResponseMessage response = null;
        var retryCount = 0;
        while (retryCount < 5)
        {
            var command = new
            {
                firstname = "admin",
                lastname = "admin",
                email = "admin123@gmail.com",
                password = "Admin123#",
                role = "Admin"
            };
            response = await client.PostAsJsonAsync($"https://localhost:{fixture.UserServiceFixture.port}/api/Auth/register", command);
            if (response.IsSuccessStatusCode)
            {
                break;
            }
            await Task.Delay(1000);
            retryCount++;
        }

        testOutputHelper.WriteLine($"Status Code: {response.StatusCode}");
        var responseBody = await response.Content.ReadAsStringAsync();
        testOutputHelper.WriteLine($"Response Body: {responseBody}");
        
        response.EnsureSuccessStatusCode();
        
        var isRegistered = await UserExistsByEmailAsync("admin123@gmail.com");
        
        isRegistered.Should().BeTrue();
    }

    private async Task<bool> UserExistsByEmailAsync(string email)
    {
        var user = await fixture.SqlDbFixture.UserManager.FindByEmailAsync(email);

        return user != null;
    }
}