namespace WebApi;

public static class LoggingExtension
{
    public static ILoggingBuilder AddLoggingConfig(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();

        return loggingBuilder;
    }
}
