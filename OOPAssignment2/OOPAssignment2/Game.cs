using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OOPAssignment2
{
    public class Game : GameIO, IMenu
    {
        private Player winner;

        public List<Die> DiceList { get; private set; }
        public List<Player> PlayerList { get; private set; }
        public List<Player> SortedPlayersByScore { get; private set; }

        public List<string> Options => throw new NotImplementedException();

        public IMenu NextMenu { get; set; }

        public Game()
        {
            DiceList = new List<Die>();
            PlayerList = new List<Player>();
            SortedPlayersByScore = new List<Player>();
        }

        #region [ setup ]

        public void SetupGame()
        {
            SetupDice();
            SetupPlayers();
            WriteScoreboard();
            UpdateScoreboard(SortedPlayersByScore);
        }

        public void SetupDice()
        {
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                DiceList.Add(new Die(rand));
            }
        }

        private void SetupPlayers()
        {
            PlayerList.AddRange(GetPlayersCount("Human", 1, 4));

            if (PlayerList.Count == 1)
            {
                PlayerList.AddRange(GetPlayersCount("Computer", 1, 3, true));
            }
            SortedPlayersByScore = PlayerList.ToList(); // Adds players to SortedPlayersByScore without using reference.
        }

        #endregion

        public void Run()
        {
            CloseMenu();
            SetupGame();
            int round = 0;
            while (true)
            {
                round++;
                WriteInMiddleHorizontal($"Round {round}!", (Console.WindowHeight / 2) - 2);

                for (int i = 0; i < PlayerList.Count; i++)
                {
                    foreach (var die in DiceList)
                    {
                        die.IsFrozen = false;
                    }
                    ClearLine((Console.WindowHeight / 2) - 1);
                    WriteInMiddleHorizontal($"{PlayerList[i].Name}'s turn!", (Console.WindowHeight / 2) - 1);
                    ClearLine((Console.WindowHeight / 2) + 1); // Clears previous "*player* scored #" message

                    if (PlayerList[i].IsComputer)
                        ComputerRoll(PlayerList[i]);
                    else
                        PlayerRoll(PlayerList[i]);

                    UpdateScoreboard(SortedPlayersByScore.OrderByDescending(p => p.Score).ToList()); // Sorts the scores and then updates leaderboard

                    if (PlayerList[i].Score >= 50)
                    {
                        winner = PlayerList[i];
                        CloseMenu();
                        return;
                    }
                    Thread.Sleep(2000); // It's ok to make thread sleep in this case. There is nothing (e.g. gui, async methods) that rely on this thread.
                }
            }
        }




        #region [ Dice Rolling ]

        //could implement delegate here
        private void PlayerRoll(Player player)
        {
            WriteInMiddleHorizontal("Press Enter to roll the dice.", Console.WindowHeight / 2);
            Console.ReadKey();
            ClearLine(Console.WindowHeight / 2);

            RollDice(player);
        }

        private void ComputerRoll(Player player)
        {
            RollDice(player);
        }

        public void RollDice(Player player)
        {
            int[] rolls = new int[DiceList.Count];
            for (int i = 0; i < DiceList.Count; i++)
            {
                rolls[i] = DiceList[i].Roll();
            }

            WriteDice(rolls);
            
            var score = GetRollScore(rolls);
            if (score == -1)
            {
                if (!player.IsComputer)
                { 
                    WriteInMiddleHorizontal($"2 of a kind has been rolled! Press Enter to reroll!", Console.WindowHeight / 2);
                    Console.ReadKey();
                    ClearLine(Console.WindowHeight / 2);
                }

                score = RollDice(rolls);
            }
            ClearLine((Console.WindowHeight / 2) + 1);
            WriteInMiddleHorizontal($"{player.Name} scored {score}", (Console.WindowHeight / 2) + 1);
            player.Score += score;
        }

        public int RollDice(int[] previousRolls)
        {
            for (int i = 0; i < DiceList.Count; i++)
            {
                if (!DiceList[i].IsFrozen)
                    previousRolls[i] = DiceList[i].Roll();
            }
            WriteDice(previousRolls);

            var score = GetRollScore(previousRolls);
            return score == -1 ? 0 : score; // Check if score is -1 return as 0
        }


        public int GetRollScore(int[] rolls)
        {
            List<Die> mostDuplicates = new List<Die>();
            for (int i = 0; i < rolls.Length - 1; i++)
            {
                foreach (var dice in DiceList)
                {
                    dice.IsFrozen = true;
                }
                for (int c = 0; c < rolls.Length; c++)
                {
                    if (rolls[i] != rolls[c])
                    {
                        DiceList[c].IsFrozen = false;
                    }
                }
                var temp = DiceList.FindAll(s => s.IsFrozen); // Returns a new list without null values
                if (temp.Count > mostDuplicates.Count)
                    mostDuplicates = temp;
            }
            foreach (var dice in DiceList)
            {
                dice.IsFrozen = mostDuplicates.Contains(dice);
            }
            if (mostDuplicates.Count == 2)
                return -1;
            else if (mostDuplicates.Count > 2)
                return (int)(3 * Math.Pow(2, mostDuplicates.Count - 3));
            else
                return 0;
        }

        #endregion

        public void CloseMenu()
        {
            ClearGame();
            NextMenu = new GameOverMenu(winner);
        }
        public void HandleInputs()
        {
            throw new NotImplementedException();
        }
    }
}
