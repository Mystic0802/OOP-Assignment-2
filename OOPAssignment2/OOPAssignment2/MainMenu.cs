using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal class MainMenu : IMenu
    {
        public readonly List<string> choices = new List<string>() { "Play Game!", "Settings", "Exit" };
        
        public List<string> Choices { get => choices; }

        private IMenu nextMenu;

        public IMenu Run()
        {
            Program.display.WriteMenuChoices(this);
            while (!HandleChoiceInputs(GetChoiceInputs()));
            CloseMenu();
            return nextMenu;
        }

        public void CloseMenu()
        {
            Program.display.ClearDisplay();
        }

        public int GetChoiceInputs()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && (choice > 0 && choice <= choices.Count))
                return choice;
            else return -1;
        }

        public bool HandleChoiceInputs(int inputCode)
        {
            switch (inputCode)
            {
                case 1:
                    CloseMenu();
                    nextMenu = null;
                    return true;
                case 2:
                    CloseMenu();
                    //nextMenu = new SettingsMenu();
                    return true;
                case 3:
                    Environment.Exit(0);
                    return true;
                default:
                    Program.display.WriteMenuChoicesError();
                    return false;
            }
        }

    }
}
