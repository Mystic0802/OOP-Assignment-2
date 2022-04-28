using System;

namespace OOPAssignment2
{
    internal class Die
    {
        #region Fields & Properties

        private int sides;
        private bool isFair;
        private Random rand;

        public int Sides { get => sides; private set => sides = value; }
        public bool IsFair { get => isFair; private set => isFair = value; }
        public Random Rand { get => rand; private set => rand = value; }

        #endregion

        public Die(int sides = 6, bool fairDie = true)
        {
            Sides = sides;
            IsFair = fairDie;
            Rand = new Random();
        }

        public int Roll()
        {
            if (IsFair)
                return Rand.Next(1, sides);
            else
            {
                int result = Rand.Next(1, sides + (sides / 2) - 1);
                return result > sides ? sides : result;
            }
        }

    }
}
