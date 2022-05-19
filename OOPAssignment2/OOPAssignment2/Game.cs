using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OOPAssignment2
{
    public class Game : GameIO, IMenu
    {
        private Player winner;

        private List<Die> DiceList { get; set; }
        private List<Player> PlayerList { get; set; }
        private List<Player> SortedPlayersByScore { get; set; }

        public List<string> Options => throw new NotImplementedException(); // Interface property; This is not needed and does not need to be implemented

        public IMenu NextMenu { get; set; }

        #region [ Constructors ]

        // Constructs new game with brand new DiceList, PlayerList and SortedPlayersByScore.
        public Game()
        {
            DiceList = new List<Die>();
            PlayerList = new List<Player>();
            SortedPlayersByScore = new List<Player>();
        }

        // Constructs new game with brand new DiceList and SortedPlayersByScore but uses an existing list of players.
        public Game(List<Player> players) // Static polymorphism
        {
            DiceList = new List<Die>();
            PlayerList = players;
            SortedPlayersByScore = new List<Player>();
        }

        #endregion

        #region [ setup ]

        public void SetupGame()
        {
            SetupDice();
            if (PlayerList.Count == 0)
                SetupPlayers();
            else
                PlayerList.ForEach(player => player.Score = 0); // Reset score to 0;
            WriteScoreboard();
            UpdateScoreboard(SortedPlayersByScore);
        }

        private void SetupDice()
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
            SortedPlayersByScore = PlayerList.ToList(); // Adds players to SortedPlayersByScore without keeping reference.
        }

        #endregion

        #region [ Interface Methods ]

        public void Run()
        {
            SetupGame();
            int round = 0;
            while (true)
            {
                round++;
                WriteInMiddleHorizontal($"Round {round}!", MenuPos.y - 2);

                for (int i = 0; i < PlayerList.Count; i++)
                {
                    foreach (var die in DiceList)
                    {
                        die.IsFrozen = false;
                    }
                    ClearLine(MenuPos.y - 1);
                    WriteInMiddleHorizontal($"{PlayerList[i].Name}'s turn!", MenuPos.y - 1);

                    PlayerRoll(PlayerList[i]);

                    UpdateScoreboard(SortedPlayersByScore.OrderByDescending(p => p.Score).ToList()); // Sorts the scores and then updates leaderboard

                    if (PlayerList[i].Score >= 50)
                    {
                        winner = PlayerList[i];
                        CloseMenu();
                        return;
                    }
                    Thread.Sleep(1000); // It's ok to make thread sleep in this case. There is nothing (e.g. gui, async methods) that rely on this thread.
                }
            }
        }

        public void CloseMenu()
        {
            Clear();
            NextMenu = new GameOverMenu(winner);
        }
        public void HandleInputs()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region [ Dice Rolling ]

        private void PlayerRoll(Player player)
        {
            if (!player.IsComputer)
            {
                WriteInMiddleHorizontal("Press Enter to roll the dice.", MenuPos.y);
                Console.ReadKey();
                ClearLine(MenuPos.y);
            }

            int[] firstRoll = RollDice();

            var frozenNumberAndFreq = GetRollMostOccuring(firstRoll);

            if(frozenNumberAndFreq.freq == 2) // If frequency after first roll is 2. Reroll.
            {
                if (!player.IsComputer)
                {
                    WriteInMiddleHorizontal($"2 of a kind has been rolled! Press Enter to reroll!", MenuPos.y);
                    Console.ReadKey();
                }
                else
                {
                    WriteInMiddleHorizontal($"2 of a kind has been rolled!", MenuPos.y);
                    Thread.Sleep(1000);
                }
                ClearLine(MenuPos.y);

                var temp = RerollNonMatching(firstRoll, RollDice(), frozenNumberAndFreq.num); // Reroll
                frozenNumberAndFreq = GetRollMostOccuring(temp); // Reset highest frequency
            }

            if(frozenNumberAndFreq.freq > 2) // If frequency after first or both rolls is > 2. Add score.
            {
                WriteInMiddleHorizontal($"{frozenNumberAndFreq.freq} of a kind has been rolled!", MenuPos.y);
                int score = (int)(3 * Math.Pow(2, frozenNumberAndFreq.freq - 3));
                WriteInMiddleHorizontal($"{player.Name} scored {score}", MenuPos.y + 1);
                player.Score += score;
            }
            else
            {
                WriteInMiddleHorizontal($"{player.Name} scored 0", MenuPos.y + 1);
            }

            if(!player.IsComputer) // If player is human, let them press Enter before moving to the next player.
            {
                WriteInMiddleHorizontal($"Press Enter to continue...", MenuPos.y+2);
                Console.ReadKey();
            }
            else
                Thread.Sleep(1000);

            ClearLine(MenuPos.y);     // Clears "# of a kind has been rolled!" message
            ClearLine(MenuPos.y + 1); // Clears "*player* scored #" message
            ClearLine(MenuPos.y+2);   // Clears "Press Enter to continue..." message
        }



        /// <summary>
        /// Rolls all the dice in DiceList.
        /// </summary>
        /// <returns>
        /// An integer array containing random numbers.
        /// It's size depends on the DiceList
        /// </returns>
        private int[] RollDice()
        {
            int[] rolls = new int[DiceList.Count]; // Create new array to store the dice rolls.

            for (int i = 0; i < DiceList.Count; i++)
            {
                rolls[i] = DiceList[i].Roll();
                Thread.Sleep(750);
                WriteDice(rolls);
            }

            return rolls;
        }

        /// <summary>
        /// Counts the frequency of each number in a roll.
        /// </summary>
        /// <returns>
        /// Tuple containing the number with the highest frequency.
        /// </returns>
        private (int num, int freq) GetRollMostOccuring(int[] rolls)
        {
            int[] frequency = new int[DiceList[0].Sides]; // Array that will store the frequency for each value. Similar to counting sort. All dice will have the same number of sides so taking the first will work in this case.

            for (int i = 0; i < rolls.Length; i++) // Loop to count frequency.
            {
                frequency[rolls[i] - 1]++;
            }

            (int num, int freq) mostOccuring = (rolls[0],frequency[0]); // Create tuple with the first value.

            for (int c = 1; c < frequency.Length; c++) // Loop to find highest frequency.
            {
                if(frequency[c] > mostOccuring.freq)
                    mostOccuring = (rolls[c],frequency[c]);
            }
            return mostOccuring;
        }

        /// <summary>
        /// Reroll the dice values depending on the mostOccuringNumber.
        /// </summary>
        /// <param name="firstRolls">The array that was generated first.</param>
        /// <param name="secondRolls">The array that was generated second.</param>
        /// <param name="mostOccuringNumber">The number that you want to keep the same from the first roll.</param>
        /// <returns>Array containing the new rerolled values</returns>
        private int[] RerollNonMatching(int[] firstRolls, int[] secondRolls, int mostOccuringNumber)
        {
            if (firstRolls.Length != secondRolls.Length) // Checks both arrays have equal length
                return null;

            int[] resultingRolls = new int[firstRolls.Length];
            // Compare each number in the firstRoll with the mostOccuringNumber
            // If the number is not equal. Replace with the second rolls value.
            for (int i = 0; i < firstRolls.Length; i++)
            {
                if(firstRolls[i] != mostOccuringNumber)
                    resultingRolls[i] = secondRolls[i];
                else
                    resultingRolls[i] = firstRolls[i];
            }
            return resultingRolls;
        }
        #endregion
    }
}
