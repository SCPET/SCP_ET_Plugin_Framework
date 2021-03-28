using System;

namespace SCP_ET.API.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ChatCommand : Attribute
    {
        public ChatCommand()
        {

        }
    }
}
