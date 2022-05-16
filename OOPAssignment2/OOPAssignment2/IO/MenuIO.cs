﻿using System;
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
            for(int i = 0; i < choicesCount; i++)
            {
                Write($"{i+1}: " + options[i], MenuPos.x-8, MenuPos.y+ i);
            }
            MoveToNextLine();
            Write("Option: ", MenuPos.x - 8, Console.CursorTop);
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
