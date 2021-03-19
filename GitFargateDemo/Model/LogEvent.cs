using System;

using Newtonsoft.Json;

namespace GitFargateDemo.Model
{
    public class LogEvent : LogEventBase, ILogEvent
    {
        /// <summary>
        /// Describes the 3rd Party destination system
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string DestinationSystem { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual Exception Exception { get; set; }

        /// <summary>
        /// Describes the top level process
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string ProcessName { get; set; }

        /// <summary>
        /// Describes the 3rd party source system
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string SourceSystem { get; set; }

        /// <summary>
        /// Describes a specific step within the Process
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Step { get; set; }

        /// <summary>
        /// Pulls the AWS X-Ray trace ID from the current environment. 
        /// <see cref="https://aws.amazon.com/xray/"/>
        /// </summary>
        public virtual string XRayTraceId => Environment.GetEnvironmentVariable("_X_AMZN_TRACE_ID");
    }
}
