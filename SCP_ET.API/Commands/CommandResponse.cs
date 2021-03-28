using SCP_ET.API.Enums;

namespace SCP_ET.API.Commands
{
    public class CommandResponse
    {
        /// <summary>
        /// A pre-set command response which can be used when a command was successful. The message will appear as "Success" and is translated. Synonymous with Done.
        /// </summary>
        public static CommandResponse Success = new CommandResponse() { isTranslation = true, translationType = TextType.Commands, success = true, message = "SUCCESS" };

        /// <summary>
        /// A pre-set command response which can be used when a command was successful. The message will appear as "Done" and is translated. Synonymous with Success.
        /// </summary>
        public static CommandResponse Done = new CommandResponse() { isTranslation = true, translationType = TextType.Commands, success = true, message = "DONE" };

        /// <summary>
        /// A pre-set command response which can be used when a command was not successful. The message will appear as "Unable to run command." and is translated.
        /// </summary>
        public static CommandResponse Fail = new CommandResponse() { isTranslation = true, translationType = TextType.Commands, success = false, message = "FAILUNKNOWN" };

        /// <summary>
        /// Creates a command response without translating to clients.
        /// </summary>
        /// <param name="success">Whether or not the command execution should be considered successful.</param>
        /// <param name="str">The message displayed to the user when the command is used.</param>
        /// <returns>A <see cref="CommandResponse" /> object.</returns>
        public static CommandResponse Create(bool success, string str)
        {
            return new CommandResponse() { message = str, success = success, isTranslation = false };
        }

        /// <summary>
        /// Creates a command response and forces it to be translated upon sending to clients.
        /// </summary>
        /// <param name="success">Whether or not the command execution should be considered successful.</param>
        /// <param name="str">The translation key to be used.</param>
        /// <returns>A <see cref="CommandResponse" /> object.</returns>
        public static CommandResponse CreateTranslated(bool success, string str)
        {
            return new CommandResponse() { message = str, success = success, isTranslation = true, translationType = TextType.Commands };
        }

        /// <summary>
        /// Creates a command response and forces it to be translated upon sending to clients, as well as providing a list of translation arguments.
        /// </summary>
        /// <param name="success">Whether or not the command execution should be considered successful.</param>
        /// <param name="str">The translation key to be used.</param>
        /// <param name="args">A list of string arguments to replace, formatted as "%KEY%|value".</param>
        /// <returns>A <see cref="CommandResponse" /> object.</returns>
        public static CommandResponse CreateTranslated(bool success, string str, string[] args)
        {
            return new CommandResponse() { message = str, success = success, isTranslation = true, translationType = TextType.Commands, translationArgs = args };
        }

        /// <summary>
        /// Defines whether or not this response will act as a translation.
        /// </summary>
        public bool isTranslation = false;

        /// <summary>
        /// Defines the message to display to the user.
        /// </summary>
        public string message = string.Empty;

        /// <summary>
        /// Defines whether or not the command should be considered successful.
        /// </summary>
        public bool success = false;

        /// <summary>
        /// Defines translation arguments.
        /// </summary>
        public string[] translationArgs = new string[] { };

        /// <summary>
        /// Defines the <see cref="TextType" /> to use for translations. Only applicable if <see cref="isTranslation" /> is true.
        /// </summary>
        public TextType translationType = TextType.Commands;
        private CommandResponse() { }
    }
}
