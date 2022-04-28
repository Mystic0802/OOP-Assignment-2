using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal class MainMenu : IMenu
    {
        public readonly List<string> text = new List<string>() { };
        public readonly List<string> choices = new List<string>() { "Play Game!", "Settings", "Exit" };
        private readonly Display displayInstance;
        public List<string> Choices { get => choices; }


        public MainMenu(Display display)
        {
            displayInstance = display;
        }

        public void Run()
        {
            ShowMenu();
            while(HandleChoiceInputs(GetChoiceInputs()))
            {

            }
            CloseMenu();
        }
        public void ShowMenu()
        {
            
        }

        public void CloseMenu()
        {
            throw new NotImplementedException();
        }

        public int GetChoiceInputs()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= choices.Count)
                return choice;
            else return -1;
        }

        public bool HandleChoiceInputs(int inputCode)
        {
            switch (inputCode)
            {
                case -1:

                    return false;
                case 1:

                    return true;
                case 2:

                    return true;
                case 3:

                    return true;
                default:
                    return false;
            }
        }

    }
}
