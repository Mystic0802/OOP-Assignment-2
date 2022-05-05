using System;

namespace OOPAssignment2
{
        public class Die
    {
        public Random Rand { get; set; }
        public bool IsFrozen { get; set; }

        public Die(Random r)
        {
            Rand = r;
            IsFrozen = false;
        }

        public int Roll()
        {
            return Rand.Next(1,7);
        }
    }
}
