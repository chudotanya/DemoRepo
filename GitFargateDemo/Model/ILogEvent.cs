using System;

namespace GitFargateDemo.Model
{
    public interface ILogEvent : ILogEventBase
    {
        string DestinationSystem { get; set; }
        Exception Exception { get; set; }
        string ProcessName { get; set; }
        string SourceSystem { get; set; }
        string Step { get; set; }

        /// <summary>
        /// Pulls the AWS X-Ray trace ID from the current environment. 
        /// <see cref="https://aws.amazon.com/xray/"/>
        /// </summary>
        string XRayTraceId { get; }
    }
}