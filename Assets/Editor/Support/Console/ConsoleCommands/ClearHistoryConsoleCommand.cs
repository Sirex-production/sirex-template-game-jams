using Support.Console;

public class ClearHistoryConsoleCommand : IConsoleCommand
{
    public string CommandName => "clear";
    
    public string Execute(string[] args = null)
    {
        Console.Instance.ClearHistory();
        
        return "";
    }
}
