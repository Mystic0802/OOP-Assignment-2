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
            dice.AddRange(new List<Die> { new Die(), new Die(), new Die(), new Die(), new Die() });
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
                    string playerName = Console.ReadLine().Trim();
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
            Program.display.ClearDisplay();
            Program.display.ShowDice();
            StartRoll();
        }

        public void StartRoll() // Rolls the dice for players
        {
            int[] rollTotals = new int[PlayerList.Count];
            for(int i = 0; i < PlayerList.Count; i++)
            {
                if (!PlayerList[i].IsComputer)
                {
                    Program.display.WriteAt("Press Enter to roll!", Program.display.CurrentHorizontalPosition, Program.display.CurrentVerticalPosition);
                }
                rollTotals[i] = RollTotal();
                Program.display.WriteAt("Press Enter to roll!", Program.display.CurrentHorizontalPosition, Program.display.CurrentVerticalPosition);
            }

            int counter = PlayerList.Count;
            bool sorted = false;
            while(counter > 1 && !sorted)
            {
                sorted = true;
                for(int i = 1; i < counter; i++)
                {
                    if(rollTotals[i-1] > rollTotals[i])
                    {
                        (rollTotals[i-1], rollTotals[i]) = (rollTotals[i], rollTotals[i-1]);
                        (PlayerList[i-1], PlayerList[i]) = (PlayerList[i], PlayerList[i-1]);
                        sorted = false;
                    }
                }
                counter--;
            }

        }


        public int RollTotal()
        {
            int[] rolls = new int[5];
            int total = 0;
            for(int i= 0; i < 5; i ++)
            {
                var roll = Dice[i].Roll();
                rolls[i] = roll;
                total += roll;
            }
            Program.display.ShowDice(rolls);
            return total;
        }
    }
}
