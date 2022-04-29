﻿using System;
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

            for(int i = 0; i < menu.Choices.Count; i++)
            { 
                WriteAt($"{i + 1} : {menu.Choices[i]}", 0, choicesTop+i);
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

        public void ClearDisplay()
        {
            Console.Clear();
            CurrentVerticalPosition = Console.CursorTop = 0;
            CurrentHorizontalPosition = Console.CursorLeft = 0;
        }
    }
}
