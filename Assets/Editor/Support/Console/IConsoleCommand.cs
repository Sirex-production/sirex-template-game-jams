namespace Support.Console
{
    /// <summary>
    /// Interface that describes functionality of console command
    /// </summary>
    public interface IConsoleCommand
    {
        public string CommandName { get; }
        public string Execute(string[] args = null);
    }
}