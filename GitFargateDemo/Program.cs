using System;
using System.Collections.Generic;
using System.IO;

using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

using GitFargateDemo.Model;

namespace GitFargateDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            var region = RegionEndpoint.USWest2;
            var credMgr = new CredentialProfileStoreChain();
            credMgr.TryGetAWSCredentials("sandbox", out var credentials);
            var loggingConfiguration = new LoggingConfiguration { LogGroup = "HangFire", WriteToConsole = true };
            var client = new AmazonCloudWatchLogsClient(credentials, region);
            LogManager.InitializeLoggerFromConfig(client, loggingConfiguration);
            LogManager.LogInfo(new LogEvent() { Message = "GitFargateDemo: Initialized Logger successfully." });
            LogMessages();
            LogManager.CloseAndFlush();
        }

        private static void LogMessages()
        {
            LogManager.LogError(new LogEvent()
            {
                DestinationSystem = "GitFargateDemo application",
                Exception = new Exception("GitFargateDemo exception raised"),
                Message = "this is GitFargateDemo application's message",
                ProcessName = "GitFargateDemo process",
                SourceSystem = "GitFargateDemo source",
                Step = "GitFargateDemo step"
            });
            LogManager.LogWarning(new LogEvent { Message = "GitFargateDemo warning" });
            LogManager.LogDebug(new LogEvent { Message = "GitFargateDemo debug" });
        }
    }
}
