using Support.Console;

public class ClearHistoryConsoleCommand : IConsoleCommand
{
    public string CommandName => "clear";
    public string Execute()
    {
        Console.Instance.ClearHistory();
        
        return "";
    }
}
