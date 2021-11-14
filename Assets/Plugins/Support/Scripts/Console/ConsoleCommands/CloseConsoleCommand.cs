namespace Support.Console
{
    public class CloseConsoleCommand : IConsoleCommand
    {
        public string Name => "close";
        public string Description => "Closes console";

        public string Execute(string[] args = null)
        {
            if (args != null && args.Length > 0)
                return "\"close\" does not accept arguments";
            
            Console.Instance.ClearInput();
            Console.Instance.TurnOff();

            return "";
        }
    }
}