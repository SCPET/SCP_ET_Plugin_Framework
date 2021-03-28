using System;

namespace SCP_ET.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AdminCommand : Attribute
    {
        public AdminCommand()
        {
        }
    }
}

