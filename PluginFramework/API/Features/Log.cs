using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using VirtualBrightPlayz.SCP_ET.ServerConsole;

namespace PluginFramework.API.Features
{
    public class Log
    {
	    /// <summary>
        /// Shorthand for info log. Includes assembly name for tag.
        /// </summary>
        /// <param name="message">The message/object to be logged.</param>
        public static void Info(object message) => Logger.Info(Assembly.GetCallingAssembly().GetName().Name, message.ToString());
        
        /// <summary>
        /// Shorthand for warn log. Includes assembly name for tag.
        /// </summary>
        /// <param name="message">The message/object to be logged.</param>
        public static void Warn(object message) => Logger.Warn(Assembly.GetCallingAssembly().GetName().Name, message.ToString());
        
        /// <summary>
        /// Shorthand for error log. Includes assembly name for tag.
        /// </summary>
        /// <param name="message">The message/object to be logged.</param>
        public static void Error(object message) => Logger.Error(Assembly.GetCallingAssembly().GetName().Name, message.ToString());
        
        /// <summary>
        /// Shorthand for debug log. Includes assembly name for tag.
        /// </summary>
        /// <param name="message">The message/object to be logged.</param>
        public static void Debug(object message) => Logger.Debug(Assembly.GetCallingAssembly().GetName().Name, message.ToString());
    }

    internal static class Logger
    {
        public static void Debug(string tag, string message)
		{
			var stackTrace = new StackTrace(true);
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"[",
				stackTrace.GetFrames()[1].GetMethod().DeclaringType.FullName,
				"] [",
				tag,
				"] ",
				message
			}));
			TcpConsole.Addlog(string.Concat(new string[]
			{
				"[",
				stackTrace.GetFrames()[1].GetMethod().DeclaringType.FullName,
				"] [DEBUG] [",
				tag,
				"] ",
				message
			}), Color.gray);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000329A0 File Offset: 0x00030BA0
		public static void Error(string tag, string message)
		{
			var stackTrace = new StackTrace(true);
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"[",
				stackTrace.GetFrames()[1].GetMethod().DeclaringType.FullName,
				"] [",
				tag,
				"] ",
				message
			}));
			TcpConsole.Addlog(string.Concat(new string[]
			{
				"[",
				stackTrace.GetFrames()[1].GetMethod().DeclaringType.FullName,
				"] [ERROR] [",
				tag,
				"] ",
				message
			}), Color.red);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00032A54 File Offset: 0x00030C54
		public static void Info(string tag, string message)
		{
			var stackTrace = new StackTrace(true);
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"[",
				stackTrace.GetFrames()[1].GetMethod().DeclaringType.FullName,
				"] [",
				tag,
				"] ",
				message
			}));
			TcpConsole.Addlog(string.Concat(new string[]
			{
				"[",
				stackTrace.GetFrames()[1].GetMethod().DeclaringType.FullName,
				"] [INFO] [",
				tag,
				"] ",
				message
			}), Color.cyan);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00032B08 File Offset: 0x00030D08
		public static void Warn(string tag, string message)
		{
			var stackTrace = new StackTrace(true);
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"[",
				stackTrace.GetFrames()[1].GetMethod().DeclaringType.FullName,
				"] [",
				tag,
				"] ",
				message
			}));
			TcpConsole.Addlog(string.Concat(new string[]
			{
				"[",
				stackTrace.GetFrames()[1].GetMethod().DeclaringType.FullName,
				"] [WARN] [",
				tag,
				"] ",
				message
			}), Color.yellow);
		}
    }
}