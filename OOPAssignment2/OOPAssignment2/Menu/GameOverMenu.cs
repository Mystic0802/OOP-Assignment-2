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

        public List<string> Options => throw new NotImplementedException();

        public IMenu NextMenu { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void CloseMenu()
        {
            throw new NotImplementedException();
        }

        public void HandleInputs()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public GameOverMenu(Player player)
        {
            winner = player;
        }
    }
}
