// Copyright(c)  DocuSign,Inc. 3/9/2021.  All Rights reserved. This software is the confidential
// and proprietary information of DocuSign, Inc. You shall not disclose such
// Confidential Information and shall use it only in accordance with the terms
// of the license agreement.

using System.Diagnostics;

using Amazon.CloudWatchLogs;

using GitFargateDemo.Model;

using Serilog;
using Serilog.Sinks.AwsCloudWatch;

namespace GitFargateDemo
{
    public static class LogManager
    {
        #region Fields

        private static bool isInitialized;

        #endregion

        public static ILogger Logger => Log.Logger;

        private static object LockObj => new object();

        #region Methods

        public static void CloseAndFlush()
        {
            Log.CloseAndFlush();
        }

        public static void ConfigureConsoleLoggerSink(LoggerConfiguration loggerConfig, LoggingConfiguration loggingConfiguration)
        {
            if (!loggingConfiguration.WriteToConsole)
            {
                return;
            }

            if (loggingConfiguration.UseCompactFormatter)
            {
                loggerConfig.WriteTo.Console(loggingConfiguration.TextFormatter, loggingConfiguration.LogLevel, loggingConfiguration.LevelSwitch, loggingConfiguration.StandardErrorFromLevel);
            }
            else
            {
                loggerConfig.WriteTo.Console(
                    loggingConfiguration.LogLevel,
                    loggingConfiguration.OutputTemplate,
                    loggingConfiguration.FormatProvider,
                    loggingConfiguration.LevelSwitch,
                    loggingConfiguration.StandardErrorFromLevel,
                    loggingConfiguration.Theme);
            }
        }

        public static void ConfigureFileLoggerSink(LoggerConfiguration loggerConfig, LoggingConfiguration loggingConfiguration)
        {
            if (string.IsNullOrEmpty(loggingConfiguration.LogFile))
            {
                return;
            }

            var fullPath = loggingConfiguration.AbsoluteLogFilePath;

            if (loggingConfiguration.UseCompactFormatter)
            {
                loggerConfig.WriteTo.File(loggingConfiguration.TextFormatter, fullPath,
                    rollingInterval: loggingConfiguration.RollingInterval,
                    retainedFileCountLimit: loggingConfiguration.RetainedFileCountLimit,
                    rollOnFileSizeLimit: loggingConfiguration.RollOnFileSizeLimit);
            }
            else
            {
                loggerConfig.WriteTo.File(fullPath,
                    rollingInterval: loggingConfiguration.RollingInterval,
                    retainedFileCountLimit: loggingConfiguration.RetainedFileCountLimit,
                    rollOnFileSizeLimit: loggingConfiguration.RollOnFileSizeLimit);
            }
        }

        public static CloudWatchSinkOptions GetCloudWatchSinkOptionsFromConfig(LoggingConfiguration loggingConfiguration)
        {
            var options = new CloudWatchSinkOptions
            {
                // the name of the CloudWatch Log group from config  
                LogGroupName = loggingConfiguration.LogGroup,
                TextFormatter = loggingConfiguration.TextFormatter,
                MinimumLogEventLevel = loggingConfiguration.LogLevel,
                BatchSizeLimit = loggingConfiguration.BatchSizeLimit,
                QueueSizeLimit = loggingConfiguration.QueueSizeLimit,
                Period = loggingConfiguration.Period,
                CreateLogGroup = loggingConfiguration.CreateLogGroup,
                LogStreamNameProvider = loggingConfiguration.LogStreamNameProvider,
                RetryAttempts = loggingConfiguration.RetryAttempts,
                LogGroupRetentionPolicy = loggingConfiguration.RetentionPolicy
            };
            return options;
        }

        /// <summary>
        /// Alows for an injectible logger such as with Asp.net core, etc.
        /// </summary>
        /// <param name="logger"></param>
        public static void InitializeLogger(ILogger logger)
        {
            if (isInitialized)
            {
                return;
            }

            lock (LockObj)
            {
                if (isInitialized)
                {
                    Debug.Assert(false, "Logger is already initialized. This request will be ignored!");
                    return;
                }

                Log.Logger = logger;
                isInitialized = true;
            }
        }

        public static void InitializeLoggerFromConfig()
        {
            if (isInitialized)
            {
                return;
            }

            lock (LockObj)
            {
                if (isInitialized)
                {
                    Debug.Assert(false, "Logger is already initialized. This request will be ignored!");
                    return;
                }

                var logConfig = new LoggerConfiguration().ReadFrom.AppSettings();
                Log.Logger = logConfig.CreateLogger();
                isInitialized = true;
            }
        }

        public static void InitializeLoggerFromConfig(IAmazonCloudWatchLogs client,
            LoggingConfiguration loggingConfiguration)
        {
            if (isInitialized)
            {
                return;
            }

            lock (LockObj)
            {
                var options = GetCloudWatchSinkOptionsFromConfig(loggingConfiguration);
                var loggerConfig = new LoggerConfiguration();
                ConfigureFileLoggerSink(loggerConfig, loggingConfiguration);
                ConfigureConsoleLoggerSink(loggerConfig, loggingConfiguration);
                var logger = loggerConfig
                    .WriteTo.Logger(l1 => l1
                        .MinimumLevel.ControlledBy(loggingConfiguration.LevelSwitch)
                        .WriteTo.AmazonCloudWatch(options, client))
                    .CreateLogger();
                isInitialized = false;
                InitializeLogger(logger);
            }

            LogInfo(new Model.LogEvent() { Message = "Initialized Logger successfully." });
        }

        public static void LogDebug<T>(T logEvent)
        {
            Logger.Debug(MessageTemplate<T>(), logEvent);
        }

        public static void LogError<T>(T logEvent) where T : ILogEventBase
        {

            Logger.Error(MessageTemplate<T>(), logEvent);
        }

        public static void LogFatal<T>(T logEvent) where T : ILogEventBase
        {

            Logger.Fatal(MessageTemplate<T>(), logEvent);
        }

        public static void LogInfo<T>(T logEvent) where T : ILogEventBase
        {

            Logger.Information(MessageTemplate<T>(), logEvent);
        }

        public static void LogWarning<T>(T logEvent) where T : ILogEventBase
        {

            Logger.Warning(MessageTemplate<T>(), logEvent);
        }

        private static string MessageTemplate<T>()
        {
            return $"{{@{typeof(T).Name}}}";
        }

        #endregion
    }
}
