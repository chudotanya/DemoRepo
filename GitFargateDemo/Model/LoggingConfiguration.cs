using System;
using System.IO;

using GitFargateDemo.Constant;
using GitFargateDemo.Utils;

using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Sinks.AwsCloudWatch;
using Serilog.Sinks.SystemConsole.Themes;

namespace GitFargateDemo.Model
{
    public class LoggingConfiguration
    {
        #region Constructors

        public LoggingConfiguration()
        {
            this.LogGroup = ConfigurationManagerParser.GetStringProperty(LogSettings.LogGroupName);
            if (string.IsNullOrEmpty(this.LogGroup))
            {
                throw new NullReferenceException("The Cloud watch log group name is not specified!");
            }

            this.LogDir = ConfigurationManagerParser.GetStringProperty(LogSettings.LogDir);
            this.LogFile = ConfigurationManagerParser.GetStringProperty(LogSettings.LogFileName);

            this.UseCompactFormatter = ConfigurationManagerParser.GetBooleanProperty(LogSettings.LogUseCompactFormatter, LogSettingsDefaults.DefaultIsUseCompactFormatter);
            this.LogLevel = ConfigurationManagerParser.GetEnumProperty(LogSettings.LogLevel, LogSettingsDefaults.DefaultLogEventLevel);

            this.RetentionPolicy = ConfigurationManagerParser.GetEnumProperty(LogSettings.RetentionPolicy, LogSettingsDefaults.DefaultLogGroupRetentionPolicy);

            this.LevelSwitch = new LoggingLevelSwitch
            {
                MinimumLevel = this.LogLevel
            };

            this.TextFormatter = this.UseCompactFormatter
                ? new CompactJsonFormatter()
                : (ITextFormatter)new JsonFormatter();

            this.BatchSizeLimit = ConfigurationManagerParser.GetIntProperty(LogSettings.BatchSizeLimit, LogSettingsDefaults.DefaultBatchSizeLimit);
            this.QueueSizeLimit = ConfigurationManagerParser.GetIntProperty(LogSettings.QueueSizeLimit, LogSettingsDefaults.DefaultQueueSizeLimit);
            this.Period = ConfigurationManagerParser.GetTimeSpanProperty(LogSettings.Period, LogSettingsDefaults.DefaultPeriod);
            this.CreateLogGroup = ConfigurationManagerParser.GetBooleanProperty(LogSettings.CreateLogGroup, LogSettingsDefaults.DefaultCreateLogGroup);
            this.LogStreamNameProvider = new DefaultLogStreamProvider();
            this.RetryAttempts = ConfigurationManagerParser.GetByteProperty(LogSettings.RetryAttempts, LogSettingsDefaults.DefaultRetryAttempts);

            this.RetainedFileCountLimit = ConfigurationManagerParser.GetIntProperty(LogSettings.RetainedFileCountLimit, LogSettingsDefaults.DefaultRetainedFileCountLimit);

            this.RollOnFileSizeLimit = ConfigurationManagerParser.GetBooleanProperty(LogSettings.RollOnFileSizeLimit, LogSettingsDefaults.DefaultRollOnFileSizeLimit);

            this.RollingInterval = ConfigurationManagerParser.GetEnumProperty(LogSettings.RollingInterval, LogSettingsDefaults.DefaultRollingInterval);
        }

        #endregion

        public string AbsoluteLogFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(LogFile))
                {
                    return LogFile;
                }

                string fullPath;
                if (string.IsNullOrEmpty(LogDir))
                {
                    fullPath = Path.GetFullPath(LogFile);
                }
                else
                {
                    string[] paths = { LogDir, LogFile };
                    fullPath = Path.Combine(paths);
                }

                return fullPath;
            }
        }

        public int BatchSizeLimit { get; set; }

        public bool CreateLogGroup { get; set; }

        public IFormatProvider FormatProvider { get; set; }

        public LoggingLevelSwitch LevelSwitch { get; set; }

        public string LogDir { get; set; }

        public string LogFile { get; set; }

        public string LogGroup { get; set; }

        public LogEventLevel LogLevel { get; set; }

        public ILogStreamNameProvider LogStreamNameProvider { get; set; }

        public string OutputTemplate { get; set; } = LogSettingsDefaults.DefaultOutputTemplate;

        public TimeSpan Period { get; set; }

        public int QueueSizeLimit { get; set; }

        public int RetainedFileCountLimit { get; set; }

        public LogGroupRetentionPolicy RetentionPolicy { get; set; }

        public byte RetryAttempts { get; set; }

        public RollingInterval RollingInterval { get; set; }

        public bool RollOnFileSizeLimit { get; set; }

        public LoggerSinkConfiguration SinkConfiguration { get; set; }

        public LogEventLevel? StandardErrorFromLevel { get; set; }

        public ITextFormatter TextFormatter { get; set; }

        public ConsoleTheme Theme { get; set; }

        public bool UseCompactFormatter { get; set; }

        public bool WriteToConsole { get; set; }
    }
}
