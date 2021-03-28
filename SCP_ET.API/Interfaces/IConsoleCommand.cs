namespace SCP_ET.API.Interfaces
{
	public interface IConsoleCommand
	{
		string Name { get; }
		void Invoke(string[] args, out string response);
	}
}