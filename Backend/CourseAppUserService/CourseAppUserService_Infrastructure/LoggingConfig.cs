using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;

namespace CourseAppUserService_Persistance;

public static class LoggingConfig
{
    public static void ConfigureLogging(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new [] {new Uri(configuration["ElasticConfiguration:Uri"])}, options =>
            {
                options.DataStream = new DataStreamName("UserService-DataStream");
                options.BootstrapMethod = BootstrapMethod.Failure;
            })
            .CreateLogger();
        
    }
}