using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal class MainMenu : Display, IMenu
    {
        private readonly List<string> choices = new List<string>() { "Play Game!", "Settings", "Exit" };

        public void Run()
        {
            ShowMenu();
            while(HandleChoiceInputs(GetChoiceInputs()))
            { }
            CloseMenu();
        }
        public void ShowMenu()
        {
            throw new NotImplementedException();
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
