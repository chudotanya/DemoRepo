// Copyright(c)  DocuSign,Inc. 3/9/2021.  All Rights reserved. This software is the confidential
// and proprietary information of DocuSign, Inc. You shall not disclose such
// Confidential Information and shall use it only in accordance with the terms
// of the license agreement.

using System;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.AwsCloudWatch;

namespace GitFargateDemo.Constant
{
    public static class LogSettingsDefaults
    {
        #region Constants

        public const int DefaultBatchSizeLimit = 100;

        public const bool DefaultCreateLogGroup = true;

        public const bool DefaultIsUseCompactFormatter = true;

        public const LogEventLevel DefaultLogEventLevel = LogEventLevel.Information;

        public const LogGroupRetentionPolicy DefaultLogGroupRetentionPolicy = LogGroupRetentionPolicy.OneWeek;

        public const string DefaultOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        public const int DefaultQueueSizeLimit = 10000;

        public const int DefaultRetryAttempts = 5;

        internal const int DefaultRetainedFileCountLimit = 7;

        internal const RollingInterval DefaultRollingInterval = RollingInterval.Day;

        internal const bool DefaultRollOnFileSizeLimit = true;

        #endregion

        #region Fields

        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(10);

        #endregion
    }
}
