using System;
using System.Collections.Generic;

namespace OOPAssignment2
{
    internal class Game
    {
        #region Fields & Properties

        private List<Player> playerList = new List<Player>();
        private List<Die> dice = new List<Die>();

        protected List<Player> PlayerList { get => playerList; set => playerList = value; }
        protected List<Die> Dice { get => dice; set => dice = value; }

        #endregion

        public Game()
        {
            Program.display.WriteTitle();
            SetupPlayers();
        }

        public void SetupPlayers()
        {
            Program.display.SetupInput("Enter number of human players(1-4):", Program.display.CurrentVerticalPosition + 1);
            int playerCount = 0;
            while ((!int.TryParse(Console.ReadLine(), out playerCount)) || (playerCount < 1 || playerCount > 4))
            {
                Program.display.WriteInputError("Enter number of human players(1-4):", Program.display.CurrentVerticalPosition-1);
            }
            if(playerCount == 1)
            {
                Program.display.SetupInput("Enter number of computer players(1-3):", Program.display.CurrentVerticalPosition);
                int computerCount = 0;
                while ((!int.TryParse(Console.ReadLine(), out computerCount)) || (computerCount < 1 || computerCount > 3))
                {
                    Program.display.WriteInputError("Enter number of computer players(1-3):", Program.display.CurrentVerticalPosition - 1);
                }
                for(int i = 0; i < 3; i++)
                {
                    PlayerList.Add(new Player($"Computer{i + 1}", true));
                }
            }
            else
            {
                for(int i = 0; i < playerCount; i++)
                {
                    Program.display.SetupInput($"Enter name of player {i+1}:", Program.display.CurrentVerticalPosition);
                    string playerName = "";
                    playerName = Console.ReadLine().Trim();
                    while (playerName.Length == 0 || playerName.Length > 25)
                    {
                        Program.display.WriteInputError($"Enter name of player {i + 1}:", Program.display.CurrentVerticalPosition - 1);
                        playerName = Console.ReadLine().Trim();
                    }
                    PlayerList.Add(new Player(playerName));
                }
            }
        }

        public void StartGame()
        {

        }
    }
}
