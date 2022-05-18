using System;

namespace OOPAssignment2
{
        public class Die
    {
        public Random Rand { get; set; }
        public bool IsFrozen { get; set; }
        public int Sides { get; set; }

        public Die(Random r)
        {
            Rand = r;
            IsFrozen = false;
            Sides = 6;
        }

        public Die(Random r, int sides) // Static polymorphism
        {
            Rand = r;
            IsFrozen = false;
            Sides = sides;
        }

        public int Roll()
        {
            return Rand.Next(1,Sides+1);
        }
    }
}
