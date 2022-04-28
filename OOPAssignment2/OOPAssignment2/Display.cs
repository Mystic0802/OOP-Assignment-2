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

        int currentVerticalPosition;
        int currentHorizontalPosition;

        public int CurrentVerticalPosition { get => currentVerticalPosition; private set => currentVerticalPosition = value; }
        public int CurrentHorizontalPosition { get => currentHorizontalPosition; private set => currentHorizontalPosition = value; }


        #endregion


        public Display()
        {
            ClearDisplay();
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
        }

    }
}
