using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal class Display
    {
        #region Fields & Properties

        private int currentVerticalPosition;
        private int currentHorizontalPosition;

        public int CurrentVerticalPosition { get => currentVerticalPosition; set => currentVerticalPosition = value; }
        public int CurrentHorizontalPosition { get => currentHorizontalPosition; set => currentHorizontalPosition = value; }

        #endregion


        public Display()
        {
            ClearDisplay();
        }

        public void WriteTitle()
        {
            WriteAt("  _____ _                            __  __", CurrentHorizontalPosition + 20, CurrentVerticalPosition);
            WriteAt(" |_   _| |_  _ _ ___ ___   ___ _ _  |  \\/  |___ _ _ ___", CurrentHorizontalPosition + 20, CurrentVerticalPosition);
            WriteAt("   | | | ' \\| '_/ -_) -_) / _ \\ '_| | |\\/| / _ \\ '_/ -_)", CurrentHorizontalPosition + 20, CurrentVerticalPosition);
            WriteAt("   |_| |_||_|_| \\___\\___| \\___/_|   |_|  |_\\___/_| \\___|", CurrentHorizontalPosition + 20, CurrentVerticalPosition);
            WriteAt("─────────────────────────────────────────────────────────", CurrentHorizontalPosition + 20, CurrentVerticalPosition);
        }

        public void SetupInput(string text, int index)
        {
            WriteAt($"{text} {new string(' ', Console.WindowWidth)}", 0, index);

            Console.CursorTop = index;
            CurrentVerticalPosition = Console.CursorTop;
            Console.CursorLeft = text.Length+1;
            CurrentHorizontalPosition = Console.CursorLeft;
        }

        public void WriteInputError(string text, int index, string errorMessage = "Invalid input!")
        {
            WriteAt($"{errorMessage}", 0, index);
            SetupInput($"{text}", index+1);
        }

        public void WriteMenuChoices(IMenu menu)
        {
            int choicesTop = currentVerticalPosition;

            for(int i = 0; i < menu.Options.Count; i++)
            { 
                WriteAt($"{i + 1} : {menu.Options[i]}", 0, choicesTop+i);
            }

            int choicesBottom = currentVerticalPosition;

            SetupInput("Option:",choicesBottom + 1);
        }

        public void WriteMenuChoicesError()
        {
            int choicesBottom = currentVerticalPosition;
            WriteAt("Invalid choice!", 0, choicesBottom-1);
            SetupInput("Option:",choicesBottom);
        }

        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                ClearDisplay();
                WriteAt(e.Message, CurrentHorizontalPosition, CurrentVerticalPosition);
            }
                currentVerticalPosition = Console.CursorTop;
                currentHorizontalPosition = Console.CursorLeft;
        }

        public void ClearLine(int y)
        {
            WriteAt($"{new string(' ', Console.WindowWidth)}", 0, y);
        }

        public void ClearDisplay()
        {
            Console.Clear();
            CurrentVerticalPosition = Console.CursorTop = 0;
            CurrentHorizontalPosition = Console.CursorLeft = 0;
        }

        public void ShowDice(int roll, int position) // ─│┌┐└┘
        {
            // Draws the Dice face. Each dot has a boolean expression attached to it.
            //  ┌───────┐
            //  │ O   O │
            //  │ O O O │
            //  │ O   O │
            //  └───────┘
            WriteAt("┌───────┐", 10 * position, 0);
            WriteAt($"│ {(roll > 1 ? 'O' : ' ')}   {(roll > 3 ? 'O' : ' ')} │", 10 * position, 1);
            WriteAt($"│ {(roll == 6 ? 'O' : ' ')} {(roll % 2 != 0 ? 'O' : ' ')} {(roll == 6 ? 'O' : ' ')} │", 10 * position, 2);
            WriteAt($"│ {(roll > 3 ? 'O' : ' ')}   {(roll > 1 ? 'O' : ' ')} │", 10 * position, 3);
            WriteAt("└───────┘", 10 * position, 4);
        }
        internal void ShowEmptyDice(int count)
        {
            for (int i = 0; i < count; i++)
            {
                WriteAt("┌───────┐", 10 * i, 0);
                WriteAt("│       │", 10 * i, 1);
                WriteAt("│       │", 10 * i, 2);
                WriteAt("│       │", 10 * i, 3);
                WriteAt("└───────┘", 10 * i, 4);
            }
        }
    }
}
