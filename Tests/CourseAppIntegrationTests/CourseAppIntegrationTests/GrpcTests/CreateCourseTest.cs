using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CourseAppIntegrationTests.Configuration;
using CourseAppIntegrationTests.DbRepositories;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CourseAppIntegrationTests.GrpcTests;

public class CreateCourseTest(CourseAppClientFactory factory, ITestOutputHelper testOutputHelper) : IClassFixture<CourseAppClientFactory>
{
    private readonly HttpClient _client = factory.GetCourseServiceClient();

    [Fact]
    public async Task CreateCourse_ShouldReturnOk_WithValidRequest()
    {
        // Arrange
        var token = TestJwtGenerator.GenerateToken(
            email: "admin@gmail.com",
            roles: new[] { "Admin" },
            issuer: "UserService",
            audience: "OtherServices",
            key: "ThisKeyWillMakeMyJWTTokenTheBest"
        );

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var command = new
        {
            title = "Test Course",
            description = "A test course",
            logo = "https://example.com/logo.png",
            level = "Beginner",
            category = "Programming",
            language = "English",
            requirements = "Basic knowledge of programming",
            lessons = new[] { Guid.NewGuid().ToString() }
        };


        // Act
        var response = await _client.PostAsJsonAsync("/api/Courses", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        testOutputHelper.WriteLine(content);
        var result = JsonSerializer.Deserialize<CreateCourseResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CreateCourse_ShouldBeSavedInDbs_WithValidRequest()
    {
        // Arrange
        var token = TestJwtGenerator.GenerateToken(
            email: "admin@gmail.com",
            roles: new[] { "Admin" },
            issuer: "UserService",
            audience: "OtherServices",
            key: "ThisKeyWillMakeMyJWTTokenTheBest"
        );

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var command = new
        {
            title = "Test Course",
            description = "A test course",
            logo = "https://example.com/logo.png",
            level = "Beginner",
            category = "Programming",
            language = "English",
            requirements = "Basic knowledge of programming",
            lessons = new[] { Guid.NewGuid().ToString() }
        };


        // Act
        var response = await _client.PostAsJsonAsync("/api/Courses", command);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        testOutputHelper.WriteLine(content);
        var result = JsonSerializer.Deserialize<CreateCourseResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        var isSavedinSql = await SqlDbRepository.CourseExistsInSqlDb(result.RecordId);
        var isSavedInMongo = await MongoDbRepository.CourseExistsInMongoDb(result.RecordId);
        testOutputHelper.WriteLine(Guid.Parse(result.RecordId).ToString());
        
        isSavedInMongo.Should().BeTrue();
        isSavedinSql.Should().BeTrue();
    }

    private class CreateCourseResponse
    {
        public string RecordId { get; set; }
    }
}