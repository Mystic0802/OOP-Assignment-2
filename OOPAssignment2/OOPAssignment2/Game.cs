using System;
using System.Collections.Generic;

namespace OOPAssignment2
{
    public class Game
    {
        public List<Die> FrozenDiceList { get; set; }
        public List<Die> DiceList { get; set; }
        public List<Player> PlayerList { get; set; }

        public Game()
        {
            FrozenDiceList = new List<Die>();
            DiceList = new List<Die>();
            PlayerList = new List<Player>();
        }

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
            int humanCount;
            Console.Write("How many human players (1-4): ");
            while (!int.TryParse(Console.ReadLine(), out humanCount) && (humanCount < 1 || humanCount > 4))
            {
                Console.Write("How many human players (1-4): ");
            }

            for (int i = 0; i < humanCount; i++)
            {
                PlayerList.Add(new Player($"Player{i + 1}"));
            }

            if (humanCount == 1)
            {
                int computerCount;
                Console.Write($"How many computer players (1-3): ");
                while (!int.TryParse(Console.ReadLine(), out computerCount) && (computerCount < 1 || computerCount > 3))
                {
                    Console.Write("How many computer players (1-3): ");
                }
                for (int i = 0; i < computerCount; i++)
                {
                    PlayerList.Add(new Player($"Computer{i + 1}", true));
                }
            }
        }

        public void StartGame()
        {
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
            var score = RollDice();
            player.Score += score;
            Console.WriteLine(player.Name + " scored " + score + ". New score: " + player.Score);




            Console.ReadKey();
        }

        public int RollDice()
        {
            int[] rolls = new int[DiceList.Count];
            for (int i = 0; i < DiceList.Count; i++)
            {
                rolls[i] = DiceList[i].Roll();
            }

            //move to output
            for (int i = 0; i < DiceList.Count; i++)
            {
                Console.Write(rolls[i] + ", ");
            }
            Console.WriteLine();

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

            //move to output
            for (int i = 0; i < DiceList.Count; i++)
            {
                Console.Write(previousRolls[i] + ", ");
            }
            Console.WriteLine();


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
    }
}
