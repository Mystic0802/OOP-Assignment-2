using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public class GameIO : IO
    {
        protected void WriteDice(int[] diceList)
        {
            int vertical = (Console.WindowHeight/2) - 10;
            int horizontalConst = (Console.WindowWidth / 2) - 25;
            for (int i = 0; i < diceList.Length; i++)
            {
                Write("┌───────┐", horizontalConst + (10 * i), vertical);
                Write($"│ {(diceList[i] > 1 ? 'O' : ' ')}   {(diceList[i] > 3 ? 'O' : ' ')} │", horizontalConst + (10 * i), vertical + 1);
                Write($"│ {(diceList[i] == 6 ? 'O' : ' ')} {(diceList[i] % 2 != 0 ? 'O' : ' ')} {(diceList[i] == 6 ? 'O' : ' ')} │", horizontalConst + (10 * i), vertical + 2);
                Write($"│ {(diceList[i] > 3 ? 'O' : ' ')}   {(diceList[i] > 1 ? 'O' : ' ')} │", horizontalConst + (10 * i), vertical + 3);
                Write("└───────┘", horizontalConst + (10 * i), vertical + 4);
            }
        }

        protected IEnumerable<Player> GetPlayersCount(string playerType, int min, int max)
        {
            int vertical = Console.WindowHeight / 2;
            int count;
            string humanAskString = $"How many {playerType} players ({min}-{max}): ";
            Write(humanAskString, (Console.WindowWidth / 2) - humanAskString.Length/2, vertical);

            while (!int.TryParse(GetInput(), out count) && (count < min || count > max))
            {
                Write(humanAskString, (Console.WindowWidth / 2) - humanAskString.Length/2, vertical);
            }

            ClearLine(vertical);
            for (int i = 0; i < count; i++)
            {
                yield return new Player($"{playerType}{i + 1}");
            }
        }
    }
}
