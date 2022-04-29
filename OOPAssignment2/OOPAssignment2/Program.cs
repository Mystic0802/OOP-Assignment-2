using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal class Program
    {
        public static Display display;

        static void Main(string[] args)
        {
            display = new Display();
            display.WriteTitle();
            MainMenu mainMenu = new MainMenu();
            var next = mainMenu.Run();
            while (next != null)
            {
                 next = next.Run();
            }

            Game game = new Game();
            game.StartGame();

            Console.ReadKey();
        }
    }
}
