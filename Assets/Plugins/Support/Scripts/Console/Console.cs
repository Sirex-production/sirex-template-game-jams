using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Support.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Support.Console
{
    /// <summary>
    /// Class that responsible for managing console input and output
    /// </summary>
    public sealed class Console : MonoSingleton<Console>
    {
        [Tooltip("UI button that will activate console (Can be not assigned)")]
        [SerializeField] private Button uiButtonThatActivatesConsole;
        [Space]
        [SerializeField] private KeyCode keyToActivateConsole;

        private readonly (Vector2 position, Vector2 size) INPUT_AREA_SCREEN_PROPERTIES = (new Vector2(0, 0), new Vector2(Screen.width, 50));
        private readonly (Vector2 position, Vector2 size) OUTPUT_AREA_SCREEN_PROPERTIES = (new Vector2(0, 50), new Vector2(Screen.width, 300));

        private LinkedList<IConsoleCommand> _consoleCommands = new LinkedList<IConsoleCommand>();
        private string _history = "";
        private string _input = "";

        private bool _isActive = false;

        protected override void Awake()
        {
            base.Awake();
            
            foreach (var classType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if(classType.GetInterfaces().Contains(typeof(IConsoleCommand)))
                    _consoleCommands.AddLast((IConsoleCommand)Activator.CreateInstance(classType));
            }

            if (uiButtonThatActivatesConsole != null)
                uiButtonThatActivatesConsole.onClick.AddListener(ChangeConsoleActiveness);
        }
        
        private void OnDestroy()
        {
            if (uiButtonThatActivatesConsole != null)
                uiButtonThatActivatesConsole.onClick.RemoveListener(ChangeConsoleActiveness);
        }

        private void Update()
        {
            if (Input.GetKeyUp(keyToActivateConsole))
                ChangeConsoleActiveness();
        }

        private void OnGUI()
        {
            if(!_isActive)
                return;
            
            GUI.Box(new Rect(OUTPUT_AREA_SCREEN_PROPERTIES.position, OUTPUT_AREA_SCREEN_PROPERTIES.size), "");
            GUI.Label(new Rect(OUTPUT_AREA_SCREEN_PROPERTIES.position, OUTPUT_AREA_SCREEN_PROPERTIES.size), _history);
            _input = GUI.TextArea(new Rect(INPUT_AREA_SCREEN_PROPERTIES.position, INPUT_AREA_SCREEN_PROPERTIES.size), _input);
            
            if (_input.Contains('\n'))
            {
                _input = _input.Trim();

                var arguments = _input.Split(' ').ToList();
                var command = arguments[0];
                arguments.RemoveAt(0);
                
                ExecuteCommand(command, arguments.ToArray());
                ClearInput();
            }
        }
        
        private void ChangeConsoleActiveness()
        {
            _isActive = !_isActive;
        }

        public void TurnOn()
        {
            _isActive = true;
        }

        public void TurnOff()
        {
            _isActive = false;
        }

        /// <summary>
        /// Executes given command
        /// </summary>
        /// <param name="commandName">Command to execute</param>
        /// <param name="arguments">Command arguments</param>
        /// <returns>Returns true if command was found. Otherwise returns false</returns>
        public bool ExecuteCommand(string commandName, string[] arguments)
        {
            if (_consoleCommands.Count < 1)
            {
                WriteToTheHistory("There is no command in command list\n");
                return false;
            }
            
            var commandToExecute = _consoleCommands.SafeFirst(command => command.Name == commandName);

            if (commandToExecute == null)
            {
                WriteToTheHistory($"There is no such command {commandName}\n");
                return true;
            }

            var commandOutput = commandToExecute.Execute(arguments);
            WriteToTheHistory($"{_input}\n");
            WriteToTheHistory(String.IsNullOrEmpty(commandOutput) ? "" : $"{commandOutput}\n");

            return true;
        }

        /// <summary>
        /// Writes given content to the history
        /// </summary>
        /// <param name="content"></param>
        public void WriteToTheHistory(object content)
        {
            if(!_isActive)
                return;
            
            if(content != null)
                _history += content.ToString();
        }

        /// <summary>
        /// Clears history area of the console
        /// </summary>
        public void ClearHistory()
        {
            if(!_isActive)
                return;
            
            _history = "";
        }

        /// <summary>
        /// Clears input area of the console
        /// </summary>
        public void ClearInput()
        {
            if(!_isActive)
                return;
            
            _input = "";
        }

        /// <summary>
        /// Finds instance of IConsoleCommand by command name
        /// </summary>
        /// <param name="commandName">Name of the command that IConsoleCommand instance should have</param>
        /// <returns>Instance of IConsoleCommand that was found. If there is no such commands, returns null</returns>
        public IConsoleCommand GetCommandByCommandName(string commandName)
        {
            return _consoleCommands.SafeFirst(command => command.Name == commandName);
        }
    }
}