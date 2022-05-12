using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public class IO
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

        public static void MoveToNextLine()
        {
            MoveCursor(0, Console.CursorTop+1);
        }

        public static void ClearLine(int y)
        {
            Write(new string(' ', Console.WindowWidth), 0, y);
        }

        public static void ClearAll()
        {
            Console.Clear();
            MoveCursor(0, 0);
        }

        public static string GetInput()
        {
            return Console.ReadLine();
        }

        public void WriteInMiddle(string s, int y)
        {
            Write(s, (Console.WindowWidth / 2) - (s.Length / 2), y);
        }

        public static void WriteBoilerplate()
        {
            WriteTitle();
        }

        private static void WriteTitle()
        {
            int horizontal = (Console.WindowWidth / 2)-57;
            int vertical = 0;

            Write("  _____ _                            __  __", horizontal, vertical);
            Write(" |_   _| |_  _ _ ___ ___   ___ _ _  |  \\/  |___ _ _ ___", horizontal, vertical+1);
            Write("   | | | ' \\| '_/ -_) -_) / _ \\ '_| | |\\/| / _ \\ '_/ -_)", horizontal, vertical + 1);
            Write("   |_| |_||_|_| \\___\\___| \\___/_|   |_|  |_\\___/_| \\___|", horizontal, vertical + 1);
            Write("─────────────────────────────────────────────────────────", horizontal, vertical + 1);

        }
    }
}
