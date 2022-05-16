using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    class GameOverMenu : MenuIO, IMenu
    {
        private Player winner;

        private readonly List<string> options = new List<string>() { "Play Game!", "Settings", "Exit" };

        public List<string> Options { get => options; }

        public IMenu NextMenu { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GameOverMenu(Player player)
        {
            winner = player;
        }
        public void Run()
        {
            WriteInMiddleHorizontal($"{winner.Name} won the game!", MenuPos.y - 2);
            WriteOptions(options);
        }

        public void HandleInputs()
        {
            throw new NotImplementedException();
        }

        public void CloseMenu()
        {
            NextMenu = new MainMenu();
        }

    }
}
