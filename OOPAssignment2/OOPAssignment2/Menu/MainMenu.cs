using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal class MainMenu : MenuIO, IMenu
    {
        private readonly List<string> options = new List<string>() { "Play Game!", "Settings(Not implemented)", "Exit" };
        
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
                throw new NextMenuNotSetException(); // User-defined exception
            }
            Clear();
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
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        NextMenu = new MainMenu();
                        break;
                }
            }
        }
    }
}
