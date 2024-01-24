namespace Core.Logging;

public static class Log
{
    private static readonly ILogger Logger = new SerilogLogger();

    public static void Debug(string msg)
    {
        Logger.Debug(msg);
    }

    public static void Debug(string msg, Exception ex)
    {
        Logger.Debug(msg, ex);
    }

    public static void Debug(string msg, params object[] objs)
    {
        Logger.DebugFormat(msg, objs);
    }

    public static void Info(string msg)
    {
        Logger.Info(msg);
    }

    public static void Info(string msg, Exception ex)
    {
        Logger.Info(msg, ex);
    }

    public static void Info(string msg, params object[] objs)
    {
        Logger.InfoFormat(msg, objs);
    }

    public static void Warn(string msg)
    {
        Logger.Warn(msg);
    }

    public static void Warn(string msg, Exception ex)
    {
        Logger.Warn(msg, ex);
    }

    public static void WarnFormat(string msg, params object[] objs)
    {
        Logger.WarnFormat(msg, objs);
    }

    public static void Error(string msg, Exception ex)
    {
        Logger.Error(msg, ex);
    }

    public static void ErrorFormat(string msg, Exception ex, params object[] objs)
    {
        Logger.ErrorFormat(msg, ex, objs);
    }

    public static void Fatal(string msg, Exception ex)
    {
        Logger.Fatal(msg, ex);
    }
}