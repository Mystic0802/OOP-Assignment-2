using System;

namespace OOPAssignment2
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Intansiate a MainMenu object. To be used as the first menu.
            MainMenu mainMenu = new MainMenu();
            IMenu currentMenu = mainMenu;
            // Infitite loop. There is no way to "break" out of this loop except selecting an exit option.
            while (true)
            {
                IO.WriteBoilerplate();
                currentMenu.Run();
                if (currentMenu.NextMenu == null)
                    throw new NextMenuNotSetException(); // User-defined exception
                currentMenu = currentMenu.NextMenu; // Sets up for loop around.
            }
        }
    }

    #region [ Testing Methods ]

    public class Testing
    {
        public static void TestDiceRoll()
        {
            const int sidesNum = 20;
            Random random = new Random();
            Die die = new Die(random);
            Die die2 = new Die(random, sidesNum);

            for(int i = 0; i < 100; i++)
            {
                int result = die.Roll();
                if(result > 6 || result < 1)
                    Console.WriteLine("Default dice failed! Rolled value: " + result);
                else
                    Console.WriteLine("Default dice passed! Rolled value: " + result);
                result = die2.Roll();
                if (result > 6)
                {
                    Console.WriteLine("Variable Side dice passed! Rolled value: " + result + "  Side Count: "+ sidesNum);
                    if (result > sidesNum)
                        Console.WriteLine("Default dice failed! Rolled value: " + result);
                }
            }
        }

        //public static void TestDiceScores(Game game)
        //{
        //    int[] noScoreRoll = new int[] { 1, 2, 3, 4, 5 };
        //    int[] reRoll = new int[] { 2, 2, 1, 5, 5 };
        //    int[] threeKindRoll = new int[] { 5, 5, 2, 2, 2 };
        //    int[] fourKindRoll = new int[] { 2, 2, 4, 2, 2 };
        //    int[] fiveKindRoll = new int[] { 2, 2, 2, 2, 2 };


        //    int roll1 = game.GetRollScore(noScoreRoll);
        //    if (roll1.Equals(0))
        //        Console.WriteLine("Zero Score passed! Returned value: " + roll1);
        //    else
        //        Console.WriteLine("Zero Score failed! Returned value: " + roll1);

        //    int roll2 = game.GetRollScore(reRoll);
        //    if (roll2.Equals(-1))
        //        Console.WriteLine("Reroll passed! Returned value: " + roll2);
        //    else
        //        Console.WriteLine("Reroll failed! Returned value: " + roll2);

        //    int roll3 = game.GetRollScore(threeKindRoll);
        //    if (roll3.Equals(3))
        //        Console.WriteLine("Three of a kind passed! Returned value: " + roll3);
        //    else
        //        Console.WriteLine("Three of a kind failed! Returned value: " + roll3);

        //    int roll4 = game.GetRollScore(fourKindRoll);
        //    if (roll4.Equals(6))
        //        Console.WriteLine("Four of a kind passed! Returned value: " + roll4);
        //    else
        //        Console.WriteLine("Four of a kind failed! Returned value: " + roll4);

        //    int roll5 = game.GetRollScore(fiveKindRoll);
        //    if (roll5.Equals(12))
        //        Console.WriteLine("Five of a kind passed! Returned value: " + roll5);
        //    else
        //        Console.WriteLine("Five of a kind failed! Returned value: " + roll5);
        //}
    }

    #endregion
}
