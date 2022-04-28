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

        private const int titleHorizontal = 20;
        private const int titleVertical = 0;

        private int currentVerticalPosition;
        private int currentHorizontalPosition;

        private bool titleShown;

        public int CurrentVerticalPosition { get => currentVerticalPosition; private set => currentVerticalPosition = value; }
        public int CurrentHorizontalPosition { get => currentHorizontalPosition; private set => currentHorizontalPosition = value; }

        public bool TitleShown { get => titleShown; set => titleShown = value; }


        #endregion


        public Display()
        {
            ClearDisplay();
        }

        public void WriteTitle()
        {
            titleShown = true;
            currentVerticalPosition = Console.CursorTop + titleVertical;
            currentHorizontalPosition = Console.CursorLeft + titleHorizontal;
            WriteAt("  _____ _                            __  __             ", currentHorizontalPosition, CurrentVerticalPosition);
            Console.WriteLine(currentVerticalPosition);
            WriteAt(" |_   _| |_  _ _ ___ ___   ___ _ _  |  \\/  |___ _ _ ___ ", currentHorizontalPosition, CurrentVerticalPosition++);
            Console.WriteLine(currentVerticalPosition);
            WriteAt("   | | | ' \\| '_/ -_) -_) / _ \\ '_| | |\\/| / _ \\ '_/ -_)", currentHorizontalPosition, CurrentVerticalPosition);
            Console.WriteLine(currentVerticalPosition);
            WriteAt("   |_| |_||_|_| \\___\\___| \\___/_|   |_|  |_\\___/_| \\___|", currentHorizontalPosition, CurrentVerticalPosition++);
            Console.WriteLine(currentVerticalPosition);
            WriteAt("─────────────────────────────────────────────────────────", currentHorizontalPosition, CurrentVerticalPosition);
            Console.WriteLine(currentVerticalPosition);
        }

        public void WriteChoices(IMenu menu)
        {
            currentVerticalPosition = titleShown ? currentVerticalPosition+2 : Console.CursorTop;

            for(int i = 0; i < menu.Choices.Count; i++)
            { 
                WriteAt($"{i + 1} : {menu.Choices[i]}", CurrentHorizontalPosition, CurrentVerticalPosition);
                currentHorizontalPosition = Console.CursorLeft;
                currentVerticalPosition++;
            }
        }

        protected void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(CurrentHorizontalPosition + x, CurrentVerticalPosition + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        protected void ClearDisplay()
        {
            Console.Clear();
            CurrentVerticalPosition = Console.CursorTop;
            CurrentHorizontalPosition = Console.CursorLeft;
            TitleShown = false;
        }

    }
}
