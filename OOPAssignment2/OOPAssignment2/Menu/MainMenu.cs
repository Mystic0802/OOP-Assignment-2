﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal class MainMenu : MenuIO, IMenu
    {
        public readonly List<string> options = new List<string>() { "Play Game!", "Settings", "Exit" };
        
        public List<string> Options { get => options; }

        public IMenu NextMenu { get; set; }

        public void Run()
        {
            WriteOptions(Options);
            
            HandleInputs();
            CloseMenu();
        }

        public void CloseMenu()
        {
            if (NextMenu == null)
            {
                // throw custom exception
            }
            ClearMenu();
        }

        public void HandleInputs()
        {
            var userInput = GetInput();
            if (userInput != null && int.TryParse(userInput, out int result) && result > 0 && result <= Options.Count)
            {
                switch (result)
                {
                    case 1:
                        NextMenu = new Game();
                        break;
                    case 2:
                        NextMenu = new SettingsMenu();
                        break;
                    default:
                        NextMenu = new MainMenu();
                        break;
                }
            }
        }
    }
}