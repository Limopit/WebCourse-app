using MongoDB.Bson;
using MongoDB.Driver;

namespace CourseAppIntegrationTests.DbRepositories
{
    public static class MongoDbRepository
    {
        private static IMongoDatabase _database;

        public static void Initialize(IMongoDatabase database)
        {
            _database = database;
        }

        public static async Task<bool> CourseExistsInMongoDb(string courseId)
        {
            var collection = _database.GetCollection<BsonDocument>("Courses");

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(courseId));

            var result = await collection.Find(filter).FirstOrDefaultAsync();
            return result != null;
        }

        public static async Task<List<BsonDocument>> GetAllCoursesAsync()
        {
            var collection = _database.GetCollection<BsonDocument>("Courses");

            var filter = Builders<BsonDocument>.Filter.Empty;
            var courses = await collection.Find(filter).ToListAsync();

            return courses;
        }
    }
}