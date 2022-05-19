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
            List<Die> diceList = new List<Die>();
            for (int i = 0; i < 5; i++)
            {
                diceList.Add(new Die(rand));
            }
            DiceList = diceList;
        }

        private void SetupDice(int diceCount)
        {
            if(diceCount < 1)
                diceCount = 1;
            Random rand = new Random();
            List<Die> diceList = new List<Die>();
            for (int i = 0; i < diceCount; i++)
            {
                diceList.Add(new Die(rand));
            }
            DiceList = diceList;
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
            WriteDice(new int[] {0,0,0,0,0}); // Draws empty dice
            if (!player.IsComputer)
            {
                WriteInMiddleHorizontal("Press Enter to roll the dice.", MenuPos.y);
                Console.ReadKey();
                ClearLine(MenuPos.y);
            }
            else
                Thread.Sleep(500);

            int[] firstRoll = RollDice();
            WriteDice(firstRoll);

            var frozenNumberAndFreq = GetRollMostOccuring(firstRoll);
            WriteInMiddleHorizontal(String.Join(",", firstRoll) + "   " + frozenNumberAndFreq.num, ScoreboardPos.y + 6);

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
                    Thread.Sleep(500);
                }
                ClearLine(MenuPos.y);

                var secondRoll = RollDice();
                WriteInMiddleHorizontal(String.Join(",", secondRoll), ScoreboardPos.y+7);
                var temp = RerollNonMatching(firstRoll, secondRoll, frozenNumberAndFreq.num); // Reroll
                WriteDice(temp);
                frozenNumberAndFreq = GetRollMostOccuring(temp); // Reset highest frequency
            }

            if(frozenNumberAndFreq.freq > 2) // If frequency after first or both rolls is > 2. Add score.
            {
                WriteInMiddleHorizontal($"{frozenNumberAndFreq.freq} of a kind has been rolled!", MenuPos.y);
                int score = GetScore(frozenNumberAndFreq.freq);
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
                Thread.Sleep(500);

            ClearLine(MenuPos.y);     // Clears "# of a kind has been rolled!" message
            ClearLine(MenuPos.y + 1); // Clears "*player* scored #" message
            ClearLine(MenuPos.y+2);   // Clears "Press Enter to continue..." message
        }

        /// <summary>
        /// Calculates the score.
        /// </summary>
        /// <param name="frequency">Count of the same number in the dice roll.</param>
        /// <returns></returns>
        private int GetScore(int frequency)
        {
            return (int)(3 * Math.Pow(2, frequency - 3));
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

            (int num, int freq) mostOccuring = (1,frequency[0]); // Create tuple with the first value.

            for (int c = 1; c < frequency.Length; c++) // Loop to find highest frequency.
            {
                if(frequency[c] > mostOccuring.freq)
                    mostOccuring = (c+1,frequency[c]);
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

        #region [ Testing Methods ]

        public void PerformAllTest()
        {
            Console.WriteLine("\n-=-=-=-=-=-=- Performing all game tests -=-=-=-=-=-=-");
            TestRollDice();
            TestMostOccuring();
            TestReroll();
            TestDiceScore();
        }

        public void TestRollDice()
        {
            Console.WriteLine("\n-=-=-=-=-=-=- Testing dice roll count -=-=-=-=-=-=-");
            
            SetupDice(1);

            int[] testRolls = RollDice();
            if (testRolls.Length == DiceList.Count)
                Console.WriteLine("Rolls count passed!");
            else
                Console.WriteLine("Rolls count failed!");
        }

        public void TestMostOccuring()
        {
            Console.WriteLine("\n-=-=-=-=-=-=- Testing most occuring -=-=-=-=-=-=-");
            int[] testArray = new int[] { 6, 2, 4, 1, 1 };

            SetupDice(1);

            var testResult = GetRollMostOccuring(testArray);
            if (testResult.num == 1)
                Console.WriteLine("Most occuring number passed!");
            else
                Console.WriteLine("Most occuring number failed!");

            if (testResult.freq == 2)
                Console.WriteLine("Most occuring number frequency passed!");
            else
                Console.WriteLine("Most occuring number frequency failed!");
        }

        public void TestReroll()
        {
            Console.WriteLine("\n-=-=-=-=-=-=- Testing reroll -=-=-=-=-=-=-");
            int[] testArray = new int[] { 1, 5, 5, 6, 1 };
            int[] testArray2 = new int[] { 4, 6, 3, 3, 2 };
            int correctMostOccuringNum = 5;

            var testResult = RerollNonMatching(testArray, testArray2, correctMostOccuringNum);
            int[] temp = new int[] { 4, 6, 3, 1, 1 };
            if (testResult.SequenceEqual(temp))
                Console.WriteLine("Reroll passed! Result: " + String.Join(", " ,testResult));
            else
                Console.WriteLine("Reroll failed! Result: " + String.Join(", ", testResult) + "   Expected: 4,6,3,1,1");
        }

        public void TestDiceScore()
        {
            Console.WriteLine("\n-=-=-=-=-=-=- Testing dice score -=-=-=-=-=-=-");

            int result = GetScore(3);
            if (result == 3)
                Console.WriteLine("3 Score passed! Returned value: " + result);
            else
                Console.WriteLine("3 Score failed! Returned value: " + result);

            result = GetScore(4);
            if (result == 6)
                Console.WriteLine("6 Score passed! Returned value: " + result);
            else
                Console.WriteLine("6 Score failed! Returned value: " + result);

            result = GetScore(5);
            if (result == 12)
                Console.WriteLine("12 Score passed! Returned value: " + result);
            else
                Console.WriteLine("12 Score failed! Returned value: " + result);
        }

        #endregion
    }
}
