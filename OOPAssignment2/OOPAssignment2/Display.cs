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

        public int CurrentVerticalPosition { get => currentVerticalPosition; set => currentVerticalPosition = value; }
        public int CurrentHorizontalPosition { get => currentHorizontalPosition; set => currentHorizontalPosition = value; }

        public bool TitleShown { get => titleShown; set => titleShown = value; }

        #endregion


        public Display()
        {
            ClearDisplay();
        }

        public void WriteTitle()
        {
            titleShown = true;
            WriteAt("  _____ _                            __  __             ", titleHorizontal, titleVertical);
            WriteAt(" |_   _| |_  _ _ ___ ___   ___ _ _  |  \\/  |___ _ _ ___ ", titleHorizontal, titleVertical+1);
            WriteAt("   | | | ' \\| '_/ -_) -_) / _ \\ '_| | |\\/| / _ \\ '_/ -_)", titleHorizontal, titleVertical+2);
            WriteAt("   |_| |_||_|_| \\___\\___| \\___/_|   |_|  |_\\___/_| \\___|", titleHorizontal, titleVertical+3);
            WriteAt("─────────────────────────────────────────────────────────", titleHorizontal, titleVertical+4);
        }

        public void WriteChoices(IMenu menu)
        {
            for(int i = 0; i < menu.Choices.Count; i++)
            { 
                WriteAt($"{i + 1} : {menu.Choices[i]}", 0, titleShown ? (titleVertical + 4) + 2 : 0);
            }
        }

        protected void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
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
