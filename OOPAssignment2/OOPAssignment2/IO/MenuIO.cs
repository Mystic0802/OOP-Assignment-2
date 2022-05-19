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

        protected void WriteOptions(List<string> options)
        {
            choicesCount = options.Count;
            for(int i = 0; i < choicesCount; i++)
            {
                Write($"{i+1}: " + options[i], MenuPos.x-12, MenuPos.y+ i);
            }
            MoveToNextLine();
            Write("Option: ", MenuPos.x - 12, Console.CursorTop);
        }

        protected override void Clear() // Dynamic polymorphism
        {
            var vertical = Console.WindowHeight / 2;
            for (int i = 0; i <= choicesCount; i++)
                ClearLine(vertical+i);
        }
    }
}
