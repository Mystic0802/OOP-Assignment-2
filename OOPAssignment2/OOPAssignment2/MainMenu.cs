using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal class MainMenu : IMenu
    {
        public readonly List<string> options = new List<string>() { "Play Game!", "Settings", "Exit" };
        
        public List<string> Options { get => options; }

        private IMenu nextMenu;

        public void Run()
        {
            //Show options
            //Get input
            //exit
        }

        public void CloseMenu()
        {
            // Check nextMenu is not null. Throw exception if so.
            // Clear menu display
        }

        public void HandleInputs()
        {
            // Get input from user.
            // Based on input set nextMenu. 
        }

    }
}
