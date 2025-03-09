using System.Data.SqlClient;

namespace CourseAppIntegrationTests.DbRepositories;

public static class SqlDbRepository
{
    private const string ConnectionString = "Server=localhost,1433;Database=UserServiceDb;User Id=sa;Password=StrongPassword123;Encrypt=False;";
    
    public static async Task<bool> CourseExistsInSqlDb(string courseId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var query = "SELECT COUNT(*) FROM UserCreatedCourses WHERE CourseId = @Id";
        await using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", courseId);

        var count = (int)await command.ExecuteScalarAsync();
        return count > 0;
    }
}