using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public static class IO
    {
        public static void Write(string text, int x, int y)
        {
            MoveCursor(x, y);
            Console.Write(text);
        }
        
        public static void MoveCursor(int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
            }
            catch
            {
                throw;
            }
        }

        public static void ClearLine(int y)
        {
            Write(new string(' ', Console.WindowWidth), 0, y);
        }

        public static void ClearAll()
        {
            Console.Clear();
        }
    }
}
