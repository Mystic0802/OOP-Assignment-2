using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();
            IMenu currentMenu = mainMenu;
            while (true)
            {
                currentMenu.Run();
                currentMenu = currentMenu.NextMenu;
            }

            Console.ReadKey();
        }

        public static void TestDiceRoll(Game game)
        {
            int[] noScoreRoll = new int[] { 1, 2, 3, 4, 5 };
            int[] reRoll = new int[] { 2, 2, 1, 5, 5 };
            int[] threeKindRoll = new int[] { 5, 5, 2, 2, 2 };
            int[] fourKindRoll = new int[] { 2, 2, 4, 2, 2 };
            int[] fiveKindRoll = new int[] { 2, 2, 2, 2, 2 };


            int roll1 = game.GetRollScore(noScoreRoll);
            if (roll1.Equals(0))
                Console.WriteLine("Zero Score passed! Returned value: " + roll1);
            else
                Console.WriteLine("Zero Score failed! Returned value: " + roll1);

            int roll2 = game.GetRollScore(reRoll);
            if (roll2.Equals(-1))
                Console.WriteLine("Reroll passed! Returned value: " + roll2);
            else
                Console.WriteLine("Reroll failed! Returned value: " + roll2);

            int roll3 = game.GetRollScore(threeKindRoll);
            if (roll3.Equals(3))
                Console.WriteLine("Three of a kind passed! Returned value: " + roll3);
            else
                Console.WriteLine("Three of a kind failed! Returned value: " + roll3);

            int roll4 = game.GetRollScore(fourKindRoll);
            if (roll4.Equals(6))
                Console.WriteLine("Four of a kind passed! Returned value: " + roll4);
            else
                Console.WriteLine("Four of a kind failed! Returned value: " + roll4);

            int roll5 = game.GetRollScore(fiveKindRoll);
            if (roll5.Equals(12))
                Console.WriteLine("Five of a kind passed! Returned value: " + roll5);
            else
                Console.WriteLine("Five of a kind failed! Returned value: " + roll5);
        }
    }
}
