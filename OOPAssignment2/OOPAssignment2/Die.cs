using System;

namespace OOPAssignment2
{
        public class Die
    {
        public Random Rand { get; set; }
        public int Sides { get; set; }

        public Die(Random r)
        {
            Rand = r;
            Sides = 6; // Defaults sides value to 6
        }

        public Die(Random r, int sides) // Static polymorphism
        {
            Rand = r;
            Sides = sides;
        }

        public int Roll() // Generates a random number based on the number of sides. 1 -> sides
        {
            return Rand.Next(1,Sides+1);
        }
    }
}
