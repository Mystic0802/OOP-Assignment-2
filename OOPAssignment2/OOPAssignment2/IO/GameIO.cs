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
        protected readonly (int x, int y) DicePos = ((Console.WindowWidth / 2) - 25, (Console.WindowHeight / 2) - 9);

        #endregion

        /// <summary>
        /// Prints all integer values from an array into dice ASCII art.
        /// </summary>
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
                Write($"│ {(diceList[i] == 6 ? 'O' : ' ')} {(diceList[i] % 2 == 1 ? 'O' : ' ')} {(diceList[i] == 6 ? 'O' : ' ')} │", DicePos.x + (10 * i), DicePos.y + 2);
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
            WriteInMiddleHorizontal(QuestionString, MenuPos.y);

            while (!int.TryParse(GetInput(), out count) || (count < min || count > max)) // Loop while the input is not valid
            {
                ClearLine(MenuPos.y);
                WriteInMiddleHorizontal(QuestionString, MenuPos.y);
            }
            ClearLine(MenuPos.y);

            for (int i = 0; i < count; i++) // Creates players based on the count.
            {
                string name = $"{playerType}{i + 1}";
                if(!isComputer) // Asks for a name if player is not a computer.
                {
                    WriteInMiddleHorizontal($"Enter a name for {playerType}{i + 1}:",MenuPos.y);
                    Write($"Name: ", MenuPos.x-12, MenuPos.y+1);
                    name = GetInput() ?? $"{playerType}{i + 1}"; // Sets default value if null is returned from GetInput()
                    if(name.Length > SettingsMenu.nameCharLimit)
                        name = name.Substring(0, SettingsMenu.nameCharLimit);
                    ClearLine(MenuPos.y);
                    ClearLine(MenuPos.y+1);
                }
                yield return new Player($"{name}", isComputer); // Returns IEnumerable containing Players
            }
        }

        /// <summary>
        /// Writes the scoreboard outline.
        /// Does not write any player names.
        /// </summary>
        protected void WriteScoreboard()
        {
            Write("Scoreboard:", ScoreboardPos.x, ScoreboardPos.y);
            Write("1: ", ScoreboardPos.x, ScoreboardPos.y + 1);
            Write("2: ", ScoreboardPos.x, ScoreboardPos.y + 2);
            Write("3: ", ScoreboardPos.x, ScoreboardPos.y + 3);
            Write("4: ", ScoreboardPos.x, ScoreboardPos.y + 4);
        }

        /// <summary>
        /// Adds a list of player's names to the scoreboard.
        /// </summary>
        /// <param name="players">A sorted list based on score.</param>
        protected void UpdateScoreboard(List<Player> players)
        {
            for(int i = 0; i < players.Count; i++)
            {
                Write(new string(' ', SettingsMenu.nameCharLimit + 6), ScoreboardPos.x + 3, ScoreboardPos.y + 1 + i); // Clears the name at the current position
                Write($"{players[i].Name} - {players[i].Score}", ScoreboardPos.x + 3, ScoreboardPos.y + 1 + i);
            }
        }

        protected override void Clear() // Dynamic polymorphism
        {
            ClearAll();
            WriteBoilerplate();
        }
    }
}
