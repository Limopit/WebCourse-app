using CourseAppCourseService_Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CourseAppCourseService_Infrastructure;

public static class CollectionConfigurations
{
    public static void ConfigureCollectionMappings()
    {
        ConfigureMap<Course>();
        ConfigureMap<Lesson>();
        ConfigureMap<Quiz>();
    }

    private static void ConfigureMap<T>()
    {
        BsonClassMap.RegisterClassMap<T>(cm =>
        {
            cm.AutoMap();

            var idMember = typeof(T).GetProperties()
                .FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));

            if (idMember != null)
            {
                cm.MapIdMember(idMember)
                    .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
            }
            else
            {
                throw new InvalidOperationException("ID property not found.");
            }

            cm.SetIgnoreExtraElements(true);
        });
    }


}