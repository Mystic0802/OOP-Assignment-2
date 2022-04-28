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

        public void StartGame()
        {
            GetNumberOfPlayers();
            PrepareGame();
        }

        private void PrepareGame()
        {
            for (int i = 0; i < 5; i++)
            {
                Dice.Add(new Die());
            }

            for (int i = 0; i < PlayerList.Count; i++)
            {
                if(!PlayerList[i].IsComputer)
                {
                    Console.WriteLine("");
                }
            }
        }

        private void GetNumberOfPlayers()
        {
            while (true)
            {
                Console.WriteLine("Enter number of players(1-4): ");
                if (int.TryParse(Console.ReadLine(), out int playerCount) && playerCount > 0 && playerCount <= 4)
                {
                    for(int i = 1; i <= playerCount; i++)
                    {
                        Console.WriteLine("Enter name for player {0}: ", i);
                        string name = Console.ReadLine().Trim();
                        PlayerList.Add(new Player(name));
                    }
                    if (playerCount == 1)
                    {
                        while (true)
                        {
                            Console.WriteLine("Enter number of computer controlled players(1-4): ");
                            if (int.TryParse(Console.ReadLine(), out int computerCount) && computerCount > 1 && computerCount <= 4)
                            {
                                for (int i = 1; i <= computerCount; i++)
                                {
                                    string name = "Computer " + i;
                                    PlayerList.Add(new Player(name, true));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
