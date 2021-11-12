using Support.Console;

/// <summary>
/// Console command that clears console output
/// </summary>
public class ClearHistoryConsoleCommand : IConsoleCommand
{
    public string CommandName => "clear";
    
    public string Execute(string[] args = null)
    {
        Console.Instance.ClearHistory();
        
        return "";
    }
}
