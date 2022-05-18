using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public class GameIO : IO
    {
        #region [ Predefined menu/text positions ]
        // Variable coordinates for the different menus/boilerplate text
        public readonly (int x, int y) DicePos = ((Console.WindowWidth / 2) - 25, (Console.WindowHeight / 2) - 9);

        #endregion

        protected void WriteDice(int[] diceList)
        {
            /// ┌───────┐
            /// │ O   O │
            /// │ O O O │
            /// │ O   O │
            /// └───────┘

            for (int i = 0; i < diceList.Length; i++)
            {
                // Specific dots are shown for each number 1-6. This checks and displays the dots at the correct positions without having invididual write sequences for each number.
                Write("┌───────┐", DicePos.x + (10 * i), DicePos.y);
                Write($"│ {(diceList[i] > 1 ? 'O' : ' ')}   {(diceList[i] > 3 ? 'O' : ' ')} │", DicePos.x + (10 * i), DicePos.y + 1);
                Write($"│ {(diceList[i] == 6 ? 'O' : ' ')} {(diceList[i] % 2 != 0 ? 'O' : ' ')} {(diceList[i] == 6 ? 'O' : ' ')} │", DicePos.x + (10 * i), DicePos.y + 2);
                Write($"│ {(diceList[i] > 3 ? 'O' : ' ')}   {(diceList[i] > 1 ? 'O' : ' ')} │", DicePos.x + (10 * i), DicePos.y + 3);
                Write("└───────┘", DicePos.x + (10 * i), DicePos.y + 4);
            }
        }

        /// <summary>
        /// Handles the inputs and outputs to get a player count
        /// </summary>
        /// <param name="playerType">The type of player you are getting a count for. Only affects the player name.</param>
        /// <param name="min">Miniumum allowed number of this player type.</param>
        /// <param name="max">Maximum allowed number of this player type.</param>
        /// <param name="isComputer">Determines if this player type will be controlled by the computer. False by default.</param>
        protected IEnumerable<Player> GetPlayersCount(string playerType, int min, int max, bool isComputer = false)
        {
            int count;
            string QuestionString = $"How many {playerType} players ({min}-{max}): ";
            Write(QuestionString, MenuPos.x - (QuestionString.Length/2), MenuPos.y);

            while (!int.TryParse(GetInput(), out count) && (count < min || count > max))
            {
                Write(QuestionString, MenuPos.x - (QuestionString.Length/2), MenuPos.y);
            }

            ClearLine(MenuPos.y);
            for (int i = 0; i < count; i++)
            {
                yield return new Player($"{playerType}{i + 1}", isComputer);
            }
        }

        public void WriteScoreboard()
        {
            int horizontal = (Console.WindowWidth / 2) - 13;
            int vertical = (Console.WindowHeight / 2) + 5;

            Write("Scoreboard:", horizontal, vertical);
            Write("1: ", horizontal, vertical + 1);
            Write("2:", horizontal, vertical + 2);
            Write("3:", horizontal, vertical + 3);
            Write("4:", horizontal, vertical + 4);
        }

        public void UpdateScoreboard(List<Player> players)
        {
            for(int i = 0; i < players.Count; i++)
            {
                Write(new string(' ', 25), ScoreboardPos.x + 3, ScoreboardPos.y + 1 + i); // Clears the name at the current position
                Write($"{players[i].Name} - {players[i].Score}", ScoreboardPos.x+3, ScoreboardPos.y + i);
            }
        }

        public override void Clear() // Dynamic polymorphism
        {
            ClearAll();
            WriteBoilerplate();
        }
    }
}
