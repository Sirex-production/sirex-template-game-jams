namespace Support.Console
{
    /// <summary>
    /// Interface that describes functionality of console command
    /// </summary>
    public interface IConsoleCommand
    {
        /// <summary>
        /// Name of the command iin the console
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Description of the command
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Method which invokes when the command with given Name property is requested in console
        /// </summary>
        /// <param name="args">Arguments of th command. If there was no arguments, passes empty array</param>
        /// <returns></returns>
        public string Execute(string[] args = null);
    }
}