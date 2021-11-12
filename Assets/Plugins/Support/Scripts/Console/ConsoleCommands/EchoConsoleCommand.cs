using System.Linq;

namespace Support.Console
{
    /// <summary>
    /// Console command that prints message to the console
    /// </summary>
    public class EchoConsoleCommand : IConsoleCommand
    {
        public string CommandName => "echo";

        public string Execute(string[] args = null)
        {
            return args == null ? "" : $"{args.Aggregate((prev, next) => prev + " " + next)}";
        }
    }
}