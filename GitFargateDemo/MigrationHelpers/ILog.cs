namespace GitFargateDemo.MigrationHelpers
{
     /// <summary>
     /// The ILog interface is from Log4net / EAIUtility Framework, and is the basic interface for all legacy log4net logging implementations.
     /// </summary>
    public interface ILog
    {
        bool IsFatalEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }

        void Debug(object message);
        void Debug(object message, System.Exception exception);
        void Debug(int eventId, object message);
        void Debug(int eventId, object message, System.Exception exception);
        void DebugFormat(string format, params object[] args);
        void DebugFormat(int eventId, string format, params object[] args);
        void Error(int eventId, object message, System.Exception exception);
        void Error(object message, System.Exception exception);
        void Error(object message);
        void Error(int eventId, object message);
        void ErrorFormat(int eventId, string format, params object[] args);
        void ErrorFormat(string format, params object[] args);
        void Fatal(int eventId, object message);
        void Fatal(int eventId, object message, System.Exception exception);
        void Fatal(object message);
        void Fatal(object message, System.Exception exception);
        void FatalFormat(string format, params object[] args);
        void FatalFormat(int eventId, string format, params object[] args);
        void Info(object message);
        void Info(int eventId, object message, System.Exception exception);
        void Info(int eventId, object message);
        void Info(object message, System.Exception exception);
        void InfoFormat(int eventId, string format, params object[] args);
        void InfoFormat(string format, params object[] args);
        void Warn(int eventId, object message);
        void Warn(object message, System.Exception exception);
        void Warn(object message);
        void Warn(int eventId, object message, System.Exception exception);
        void WarnFormat(string format, params object[] args);
        void WarnFormat(int eventId, string format, params object[] args);
    }
}