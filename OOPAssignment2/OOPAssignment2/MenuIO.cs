using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public class MenuIO : IO
    {
        private int choicesCount;

        public void WriteOptions(List<string> options)
        {
            choicesCount = options.Count;
            var vertical = Console.WindowHeight / 2;
            for(int i = 0; i < choicesCount; i++)
            {
                var middleWidth = (Console.WindowWidth / 2) - 8;
                Write($"{i+1}: " + options[i],middleWidth,vertical+i);
            }
            MoveToNextLine();
            Write("Option: ", (Console.WindowWidth / 2) - 8, Console.CursorTop);
        }

        public void ClearMenu()
        {
            var vertical = Console.WindowHeight / 2;
            for (int i = 0; i <= choicesCount; i++)
                ClearLine(vertical+i);
            MoveCursor(0, 0);
        }
    }
}
