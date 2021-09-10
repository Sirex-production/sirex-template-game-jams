namespace Support.Console
{
    public interface IConsoleCommand
    {
        public string CommandName { get; }
        public string Execute(string[] args = null);
    }
}