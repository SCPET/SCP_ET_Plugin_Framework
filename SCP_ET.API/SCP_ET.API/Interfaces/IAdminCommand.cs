using SCP_ET.API.Classes;
using SCP_ET.API.Commands;
using System.Collections.Generic;

namespace SCP_ET.API.Interfaces
{
    public interface IAdminCommand
    {
        List<string> RequiredPermission { get; }
        string Description { get; }
        string Name { get; }
        List<string> Aliases { get; }
        bool Hidden { get; }
        void Invoke(Player invoker, Player currentPlayer, string[] args, out CommandResponse response);
    }
}
