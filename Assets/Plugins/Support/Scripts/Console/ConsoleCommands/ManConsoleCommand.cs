using System.Linq;

namespace Support.Console
{
    public class ManConsoleCommand : IConsoleCommand
    {
        public string Name => "man";
        public string Description => "Prints command description";

        public string Execute(string[] args = null)
        {
            if (args == null || args.Length < 1)
                return "No argument was specified";
            
            var consoleCommand = args.Length > 1 ? args.Aggregate((prev, next) => prev + " " + next) : args[0];
            var command = Console.Instance.GetCommandByCommandName(consoleCommand);

            if (command == null)
                return $"There is no command name as {consoleCommand}";

            return command.Description;
        }
    }
}