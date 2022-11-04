using System.Linq;

namespace Support.Console
{
    /// <summary>
    /// Console command that prints message to the console
    /// </summary>
    public class EchoConsoleCommand : IConsoleCommand
    {
        public string Name => "echo";
        public string Description => "Prints input in the console";

        public string Execute(string[] args = null)
        {
            return args == null ? "" : $"{args.Aggregate((prev, next) => prev + " " + next)}";
        }
    }
}