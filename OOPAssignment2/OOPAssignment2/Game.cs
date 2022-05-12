using System;
using System.Collections.Generic;
using System.Threading;

namespace OOPAssignment2
{
    public class Game : GameIO, IMenu
    {
        public List<Die> FrozenDiceList { get; set; }
        public List<Die> DiceList { get; set; }
        public List<Player> PlayerList { get; set; }

        public List<string> Options => throw new NotImplementedException();

        public IMenu NextMenu { get; set; }

        public Game()
        {
            FrozenDiceList = new List<Die>();
            DiceList = new List<Die>();
            PlayerList = new List<Player>();
        }

        #region setup

        public void SetupGame()
        {
            SetupDice();
            SetupPlayers();
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
                PlayerList.AddRange(GetPlayersCount("Computer", 1, 3));
            }
        }

        #endregion

        public void Run()
        {
            SetupGame();
            int round = 0;
            while (true)
            {
                round++;
                Console.WriteLine($"\nRound {round}!");

                for (int i = 0; i < PlayerList.Count; i++)
                {
                    foreach (var die in DiceList)
                    {
                        die.IsFrozen = false;
                    }

                    Console.WriteLine($"\n{PlayerList[i].Name}'s turn!");
                    if (PlayerList[i].IsComputer)
                        ComputerRoll(PlayerList[i]);
                    else
                        PlayerRoll(PlayerList[i]);

                    if (PlayerList[i].Score >= 50)
                    {
                        Console.WriteLine(PlayerList[i].Name + " won the game!");
                        return;
                    }
                }
            }
        }

        #region DiceRolling

        private void PlayerRoll(Player player)
        {
            Console.WriteLine("Press Enter to roll the dice.");

            Console.ReadKey();
            var score = RollDice();
            player.Score += score;
            Console.WriteLine(player.Name + " scored " + score + ". New score: " + player.Score);
            Console.ReadKey();
        }

        private void ComputerRoll(Player player)
        {
            Thread.Sleep(1000); // It's ok to make thread sleep in this case. There is nothing (e.g. gui, async methods) that rely on this thread.
            var score = RollDice();
            player.Score += score;
            Console.WriteLine(player.Name + " scored " + score + ". New score: " + player.Score);
        }

        public int RollDice()
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
                Console.WriteLine("Rerolling...");
                score = RollDice(rolls);
                return score == -1 ? 0 : score; // Check if score is -1 return as 0
            }

            return score;
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

        public void HandleInputs()
        {
            throw new NotImplementedException();
        }

        public void CloseMenu()
        {
            //NextMenu = GameOverMenu();
        }
    }
}
