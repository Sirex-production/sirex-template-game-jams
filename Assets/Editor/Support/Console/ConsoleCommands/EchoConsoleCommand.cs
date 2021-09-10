using System.Linq;

namespace Support.Console
{
    public class EchoConsoleCommand : IConsoleCommand
    {
        public string CommandName => "echo";

        public string Execute(string[] args = null)
        {
            return args == null ? "" : $"{args.Aggregate((prev, next) => prev + " " + next)}";
        }
    }
}