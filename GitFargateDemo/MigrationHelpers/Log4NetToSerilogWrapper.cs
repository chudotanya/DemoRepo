using System;

using Serilog;
using Serilog.Events;

namespace GitFargateDemo.MigrationHelpers
{
    /// <summary>
    /// Serilog logging wrapped in Log4net interface. To use, simply configure your serilogger, then pass it into a new instance of this class. Then point existing log4net calls that currently use ILog to point to
    ///  the appropriate Log4NetToSerilogWrapper.Instance calls, such as Log4NetToSerilogWrapper.Instance.Debug("my log message")
    /// </summary>
    public class Log4NetToSerilogWrapper : ILog
    {
        protected ILogger serilogger;
        public ILogger Serilogger
        {
            get
            {
                if (serilogger == null) throw new NullReferenceException("No valid Serilog logger was found (Did you initialize a Serilog logger into the wrapper properly, or recently clear it?)");
                return serilogger;
            }
            set { serilogger = value; }
        }

        protected static Log4NetToSerilogWrapper instance;
        public static Log4NetToSerilogWrapper Instance
        {
            get
            {
                if (instance == null) throw new NullReferenceException("No valid Serilog logger was found (Did you initialize a Serilog logger into the wrapper properly, or recently clear it?)");
                return instance;
            }
            set { instance = value; }
        }

        public static bool IsInitialized { get { return (instance != null && Instance.serilogger != null); } }

        public Log4NetToSerilogWrapper(ILogger serilogLogger)
        {
            serilogger = serilogLogger;
            Instance = this;
        }

        bool ILog.IsDebugEnabled => Serilogger.IsEnabled(LogEventLevel.Debug);

        bool ILog.IsInfoEnabled => Serilogger.IsEnabled(LogEventLevel.Information);

        bool ILog.IsWarnEnabled => Serilogger.IsEnabled(LogEventLevel.Warning);

        bool ILog.IsErrorEnabled => Serilogger.IsEnabled(LogEventLevel.Error);

        bool ILog.IsFatalEnabled => Serilogger.IsEnabled(LogEventLevel.Fatal);

        public void Debug(string message)
        {
            Serilogger.Debug(message);
        }

        public void Debug(object message)
        {
            Serilogger.Debug("{message}", message);
        }

        public void Debug(object message, Exception exception)
        {
            Serilogger.Debug(exception, "{message}", message);
        }

        public void Debug(int eventId, object message)
        {
            Serilogger.Debug("{EventID} - {Message}", eventId, message);
        }

        public void Debug(int eventId, object message, Exception exception)
        {
            Serilogger.Debug(exception, "{EventID} - {Message}", eventId, message);
        }

        public void DebugFormat(string format, params object[] args)
        {
            Serilogger.Debug("{message}", String.Format(format, args));
        }

        public void DebugFormat(int eventId, string format, params object[] args)
        {
            Serilogger.Debug("{EventID} - {Message}", eventId, String.Format(format, args));
        }

        public void Error(string message)
        {
            Serilogger.Error(message);
        }

        public void Error(object message)
        {
            Serilogger.Error("{message}", message);
        }

        public void Error(object message, Exception exception)
        {
            Serilogger.Error(exception, "{message}", message);
        }

        public void Error(int eventId, object message)
        {
            Serilogger.Error("{EventID} - {Message}", eventId, message);
        }

        public void Error(int eventId, object message, Exception exception)
        {
            Serilogger.Error(exception, "{EventID} - {Message}", eventId, message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            Serilogger.Error("{message}", String.Format(format, args));
        }

        public void ErrorFormat(int eventId, string format, params object[] args)
        {
            Serilogger.Error("{EventID} - {Message}", eventId, String.Format(format, args));
        }

        public void Fatal(string message)
        {
            Serilogger.Fatal(message);
        }

        public void Fatal(object message)
        {
            Serilogger.Fatal("{message}", message);
        }

        public void Fatal(object message, Exception exception)
        {
            Serilogger.Fatal(exception, "{message}", message);
        }

        public void Fatal(int eventId, object message)
        {
            Serilogger.Fatal("{EventID} - {Message}", eventId, message);
        }

        public void Fatal(int eventId, object message, Exception exception)
        {
            Serilogger.Fatal(exception, "{EventID} - {Message}", eventId, message);
        }

        public void FatalFormat(string format, params object[] args)
        {
            Serilogger.Fatal("{message}", String.Format(format, args));
        }

        public void FatalFormat(int eventId, string format, params object[] args)
        {
            Serilogger.Fatal("{EventID} - {Message}", eventId, String.Format(format, args));
        }

        public void Info(string message)
        {
            Serilogger.Information(message);
        }

        public void Info(object message)
        {
            Serilogger.Information("{message}", message);
        }

        public void Info(object message, Exception exception)
        {
            Serilogger.Information(exception, message.ToString(), message);
        }

        public void Info(int eventId, object message)
        {
            Serilogger.Information("{EventID} - {Message}", eventId, message);
        }

        public void Info(int eventId, object message, Exception exception)
        {
            Serilogger.Information(exception, "{EventID} - {Message}", eventId, message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Serilogger.Information("{message}", String.Format(format, args));
        }

        public void InfoFormat(int eventId, string format, params object[] args)
        {
            Serilogger.Information("{EventID} - {Message}", eventId, String.Format(format, args));
        }

        public void Warn(string message)
        {
            Serilogger.Warning(message);
        }

        public void Warn(object message)
        {
            Serilogger.Warning("{message}", message);
        }

        public void Warn(object message, Exception exception)
        {
            Serilogger.Warning(exception, "{message}", message);
        }

        public void Warn(int eventId, object message)
        {
            Serilogger.Warning("{EventID} - {Message}", eventId, message);
        }

        public void Warn(int eventId, object message, Exception exception)
        {
            Serilogger.Warning(exception, "{EventID} - {Message}", eventId, message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            Serilogger.Warning("{message}", String.Format(format, args));
        }

        public void WarnFormat(int eventId, string format, params object[] args)
        {
            Serilogger.Warning("{EventID} - {Message}", eventId, String.Format(format, args));
        }
    }
}