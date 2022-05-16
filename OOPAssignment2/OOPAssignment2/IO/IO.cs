using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public class IO
    {
        #region [ Predefined menu/text positions ]
        // Variable coordinates for the different menus/boilerplate text
        public static readonly (int x, int y) TitlePos = ((Console.WindowWidth / 2) - (57 / 2), 0);
        public static readonly (int x, int y) ScoreboardPos = ((Console.WindowWidth / 2) - 13, (Console.WindowHeight / 2) + 5);

        #endregion

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
            catch(Exception ex)
            {
                throw ex;
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

        public void WriteInMiddleHorizontal(string s, int y)
        {
            Write(s, (Console.WindowWidth / 2) - (s.Length / 2), y);
        }

        public static void WriteBoilerplate()
        {
            WriteTitle();
        }


        private static void WriteTitle()
        {
            Write("  _____ _                            __  __", TitlePos.x, TitlePos.y);
            Write(" |_   _| |_  _ _ ___ ___   ___ _ _  |  \\/  |___ _ _ ___", TitlePos.x, TitlePos.y + 1);
            Write("   | | | ' \\| '_/ -_) -_) / _ \\ '_| | |\\/| / _ \\ '_/ -_)", TitlePos.x, TitlePos.y + 2);
            Write("   |_| |_||_|_| \\___\\___| \\___/_|   |_|  |_\\___/_| \\___|", TitlePos.x, TitlePos.y + 3);
            Write("─────────────────────────────────────────────────────────", TitlePos.x, TitlePos.y + 4);

        }



        public static void WriteScoreboard()
        {
            int horizontal = (Console.WindowWidth / 2) - 13;
            int vertical = (Console.WindowHeight/2) + 5;

            Write("Scoreboard:", horizontal, vertical);
            Write("1: ", horizontal, vertical + 1);
            Write("2:", horizontal, vertical + 2);
            Write("3:", horizontal, vertical + 3);
            Write("4:", horizontal, vertical + 4);
        }
    }
}
