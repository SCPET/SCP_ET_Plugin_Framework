using SCP_ET.API.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SCP_ET.API.Commands
{
    public class ConsoleCommandHandler
    {
        public static Dictionary<string, IConsoleCommand> commands { get; set; } = new Dictionary<string, IConsoleCommand>();
        public static void RegisterCommand(string commandName, IConsoleCommand handler)
        {
            if (commands.ContainsKey(commandName))
            {
                PluginSystem.Logger.Info("API", $"Command {commandName} already exists in Console.");
                return;
            }
            commands.Add(commandName, handler);
            PluginSystem.Logger.Info("API", $"Command {commandName} added to Console.");
        }

        public static void ExecuteCommand(string[] args)
        {
			if (!ConsoleCommandHandler.commands.ContainsKey(args[0].ToLower()))
			{
                PluginSystem.Logger.Info("API", $"[ConsoleCommand] Command {args[0].ToLower()} does not exist");
				return;
			}
			IConsoleCommand consoleCommand = ConsoleCommandHandler.commands[args[0].ToLower()];
			string str = "";
			consoleCommand.Invoke(args, out str);
		    PluginSystem.Logger.Info("API", $"[ConsoleCommand] {str}");
		}
    }
}