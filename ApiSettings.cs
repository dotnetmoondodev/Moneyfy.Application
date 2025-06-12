using Microsoft.Extensions.Logging;

namespace Application;

public static class LoggerSettings
{
    public static string Default => "Logging:LogLevel:Default";

    public static LogLevel GetLogLevel( string? logLevel )
    {
        return logLevel switch
        {
            "Trace" => LogLevel.Trace,
            "Debug" => LogLevel.Debug,
            "Information" => LogLevel.Information,
            "Warning" => LogLevel.Warning,
            "Error" => LogLevel.Error,
            "Critical" => LogLevel.Critical,
            _ => LogLevel.Information
        };
    }
}

public static class ApiSettings
{
    public static string Key => $"{nameof( ApiSettings )}:Key";
    public static string Issuer => $"{nameof( ApiSettings )}:Issuer";
    public static string Audience => $"{nameof( ApiSettings )}:Audience";
    public static string DBConnection => $"{nameof( ApiSettings )}:DBConnection";

    public static string BuildJwtKey( string? secret )
    {
        return $"Moon|{secret}|DoDev";
    }
}
