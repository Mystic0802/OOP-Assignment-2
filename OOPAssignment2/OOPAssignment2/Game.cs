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
            Program.display.SetupInput("Enter number of players(1-4):", Program.display.CurrentVerticalPosition + 1);
            int playerCount = 0;
            while ((!int.TryParse(Console.ReadLine(), out playerCount)) || (playerCount < 1 || playerCount > 4))
            {
                Program.display.WriteInputError("Enter number of players(1-4):", Program.display.CurrentVerticalPosition-1);
            }
        }

        public void StartGame()
        {

        }


    }
}
