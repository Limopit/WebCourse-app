using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Exceptions;

namespace CourseAppCourseService.Logging;

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
                options.DataStream = new DataStreamName("CourseService-DataStream");
                options.BootstrapMethod = BootstrapMethod.Failure;
            })
            .CreateLogger();
    }
}