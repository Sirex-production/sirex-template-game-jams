using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extensions;
using UnityEngine;

namespace Support.Console
{
    public class Console : MonoSingleton<Console>
    {
        private LinkedList<IConsoleCommand> _consoleCommands = new LinkedList<IConsoleCommand>();
        private string _history = "";
        private string _input = "";

        protected override void Awake()
        {
            base.Awake();
            
            foreach (var classType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if(classType.GetInterfaces().Contains(typeof(IConsoleCommand)))
                    _consoleCommands.AddLast((IConsoleCommand)Activator.CreateInstance(classType));
            }
        }

        private void OnGUI()
        {
            GUI.TextArea(new Rect(new Vector2(0, 0), new Vector2(Screen.width, 100)), _history);
            _input = GUI.TextArea(new Rect(new Vector2(0, 100), new Vector2(Screen.width, 50)), _input);

            if (_input.Contains('\n'))
            {
                WriteToTheHistory(_input);
                ExecuteCommand(_input);
                ClearInput();
            }
        }

        private void ExecuteCommand(string consoleInput)
        {
            if (_consoleCommands.Count < 1)
            {
                WriteToTheHistory("There is no command in command list");
                return;
            }

            consoleInput = consoleInput.Trim();
            var commandToExecute = _consoleCommands.SafeFirst(command => command.CommandName == consoleInput);

            if (commandToExecute == null)
            {
                WriteToTheHistory($"There is no such command {consoleInput}");
                return;
            }

            var commandOutput = commandToExecute.Execute();
            WriteToTheHistory(commandOutput);
        }

        public void WriteToTheHistory(object content)
        {
            if(content != null)
                _history += content.ToString();
        }

        public void ClearHistory() => _history = "";
        public void ClearInput() => _input = "";
    }
}