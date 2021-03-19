using System.IO;

using Newtonsoft.Json;

using Serilog.Formatting;

namespace GitFargateDemo.Model
{
    public class CustomLogFormatter : ITextFormatter
    {
        #region Methods

        public void Format(Serilog.Events.LogEvent logEvent, TextWriter output)
        {
            var prop = JsonConvert.SerializeObject(logEvent.Properties);

            output.Write("Timestamp - {0} | Level - {1} | Message {2} {3} ", logEvent.Timestamp, logEvent.Level, prop, output.NewLine);
            if (logEvent.Exception != null)
            {
                output.Write("Exception - {0}", logEvent.Exception);
            }
        }

        #endregion
    }
}
