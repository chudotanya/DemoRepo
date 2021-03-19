namespace GitFargateDemo.Model
{
    public abstract class LogEventBase : ILogEventBase
    {
        public virtual string Message { get; set; }
    }
}
