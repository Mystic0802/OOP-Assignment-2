using System;
using System.Collections.Generic;

namespace OOPAssignment2
{
    public class Game
    {
        List<Player> playerList = new List<Player>();

        enum GameState
        {
            None = 0,
            SettingUpGame,
            PlayingGame,
            EndGame
        }

        public void PrepareGame()
        {
            playerList.Clear();
            GetNumberOfPlayers();

        }

        public void GetNumberOfPlayers()
        {
            while (true)
            {
                Console.WriteLine("Enter number of players(1-4): ");
                if (int.TryParse(Console.ReadLine(), out int playerCount) && playerCount > 1 && playerCount <= 4)
                {
                    for(int i = 1; i <= playerCount; i++)
                    {
                        Console.WriteLine("Enter name for player {0}: ", i);
                        var name = Console.ReadLine().Trim();
                        playerList.Add(new Player(name));
                    }
                }
            }
        }
    }
}
