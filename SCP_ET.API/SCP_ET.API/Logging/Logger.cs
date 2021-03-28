namespace SCP_ET.API.Logging
{
    public abstract class Logger
    {
        public abstract void Debug(string tag, string message);
        public abstract void Info(string tag, string message);
        public abstract void Warn(string tag, string message);
        public abstract void Error(string tag, string message);
    }
}
