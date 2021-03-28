using SCP_ET.API.Classes;
using SCP_ET.API.Enums;
using SCP_ET.API.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SCP_ET.API.Commands
{
    public class TextChatCommandHandler
    {
        public static Dictionary<string, IChatCommand> commands { get; set; } = new Dictionary<string, IChatCommand>();
        public static void RegisterCommand(string commandName, IChatCommand handler)
        {
            if (commands.ContainsKey(commandName))
            {
                PluginSystem.Logger.Info("API", $"Command {commandName} already exists in TextChat.");
                return;
            }
            commands.Add(commandName, handler);
            PluginSystem.Logger.Info("API", $"Command {commandName} added to TextChat.");
        }

        public static void ExecuteCommand(Player player, string[] args)
        {
            if (commands.ContainsKey(args[0].ToLower()) || commands.Any(pair => pair.Value.Aliases.Contains(args[0].ToLower())))
            {
                IChatCommand cmd = commands[args[0].ToLower()];
                if (cmd == null)
                    cmd = commands.Where(pair => pair.Value.Aliases.Contains(args[0].ToLower())).GetEnumerator().Current.Value;
                if (cmd.RequiredPermission.Count == 0 || cmd.RequiredPermission.Any(perm => player.CheckPermission(perm)))
                {
                    cmd.Invoke(player, args.Skip(1).ToArray<string>(), out CommandResponse response);
                    if (response.isTranslation)
                    {
                        if (response.translationArgs.Length > 0)
                        {
                            player.SendTranslatedTextChatMessageArgs(response.message, (response.success ? "FAF0FF" : "FFC259"), (int)response.translationType, string.Join("/", response.translationArgs));
                        }
                        else
                        {
                            player.SendTranslatedTextChatMessage(response.message, (response.success ? "FAF0FF" : "FFC259"), (int)response.translationType);
                        }
                    }
                    else
                    {
                        player.SendTextChatMessage(response.message, (response.success ? "FAF0FF" : "FFC259"));
                    }
                    PluginSystem.Logger.Info("API", $"[ChatCommand] {player.PlayerName} used command: {string.Join(" ", args)}");
                }
                else
                {
                    player.SendTranslatedTextChatMessageArgs($"MISSINGPERMISSION", "red", (int)TextType.Commands, $"%PERMISSION%|{string.Join(",", cmd.RequiredPermission)}");
                }
            }
            else
            {
                player.SendTranslatedTextChatMessage("UNKNOWNCOMMAND", "red", (int)TextType.Commands);
            }
        }
    }
}