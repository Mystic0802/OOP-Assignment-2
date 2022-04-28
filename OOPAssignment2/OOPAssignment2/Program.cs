using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display();
            Game gameInstance = new Game();
            MainMenu mainMenu = new MainMenu(display);

            display.WriteTitle();
            display.WriteChoices(mainMenu);

            Console.ReadKey();
        }
    }
}
