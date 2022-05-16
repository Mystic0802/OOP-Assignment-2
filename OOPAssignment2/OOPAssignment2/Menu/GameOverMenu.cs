using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    class GameOverMenu : MenuIO, IMenu
    {
        private readonly Player winner;

        private readonly List<string> options = new List<string>() { "Play Again!", "Exit to Main Menu", "Exit" };

        public List<string> Options { get => options; }

        public IMenu NextMenu { get; set; }

        public GameOverMenu(Player player)
        {
            winner = player;
        }
        public void Run()
        {
            WriteInMiddleHorizontal($"{winner.Name} won the game!", MenuPos.y - 2);
            WriteOptions(options);
            HandleInputs();
            CloseMenu();
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
                        NextMenu = new MainMenu();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public void CloseMenu()
        {
            ClearLine(MenuPos.y - 2);
            ClearMenu();
            NextMenu = new MainMenu();
        }

    }
}
