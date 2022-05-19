using System;

namespace OOPAssignment2
{
    public class Program
    {
        const bool TestingMode = false; // True = Testing mode enabled. Game does not start.


        static void Main(string[] args)
        {
            if (TestingMode)
            {
                Console.WriteLine("-=-=-=-=-=-=- Testing Started -=-=-=-=-=-=-");
                Testing.PerformAllTests();
                Console.WriteLine("\n-=-=-=-=-=-=- Testing Completed -=-=-=-=-=-=-");
                Console.ReadKey();
            }
            else
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
    }

    #region [ Testing Methods ]

    public static class Testing
    {
        public static void PerformAllTests()
        {
            Console.WriteLine("\n-=-=-=-=-=-=- Performing all tests -=-=-=-=-=-=-");
            TestDiceRoll();
            Game game = new Game();
            game.PerformAllTest();
        }

        public static void TestDiceRoll()
        {
            Console.WriteLine("\n-=-=-=-=-=-=- Testing Dice Roll -=-=-=-=-=-=-");
            const int sidesNum = 20;
            Random random = new Random();
            Die die = new Die(random);
            Die die2 = new Die(random, sidesNum);

            for(int i = 0; i < 10; i++)
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
    }

    #endregion
}
