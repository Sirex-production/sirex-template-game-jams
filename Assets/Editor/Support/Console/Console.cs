using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Support.Console
{
    public class Console : MonoSingleton<Console>
    {
        [SerializeField] private Button buttonThatActivatesConsole;
        [Space]
        [SerializeField] private KeyCode keyToActivateConsole;
        
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

            if (buttonThatActivatesConsole != null)
                buttonThatActivatesConsole.onClick.AddListener(ChangeConsoleActiveness);
        }
        
        private void OnDestroy()
        {
            if (buttonThatActivatesConsole != null)
                buttonThatActivatesConsole.onClick.RemoveListener(ChangeConsoleActiveness);
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
            
            GUI.Box(new Rect(new Vector2(0, 0), new Vector2(Screen.width, 100)), "");
            GUI.Label(new Rect(new Vector2(0, 0), new Vector2(Screen.width, 100)), _history);
            _input = GUI.TextArea(new Rect(new Vector2(0, 100), new Vector2(Screen.width, 50)), _input);
            
            if (_input.Contains('\n'))
            {
                WriteToTheHistory(_input);
                ExecuteCommand(_input);
                ClearInput();
            }
        }
        
        private void ChangeConsoleActiveness() => _isActive = !_isActive;

        private void ExecuteCommand(string consoleInput)
        {
            if (_consoleCommands.Count < 1)
            {
                WriteToTheHistory("There is no command in command list\n");
                return;
            }

            consoleInput = consoleInput.Trim();
            var commandToExecute = _consoleCommands.SafeFirst(command => command.CommandName == consoleInput);

            if (commandToExecute == null)
            {
                WriteToTheHistory($"There is no such command {consoleInput}\n");
                return;
            }

            var commandOutput = commandToExecute.Execute();
            WriteToTheHistory(commandOutput);
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
    }
}