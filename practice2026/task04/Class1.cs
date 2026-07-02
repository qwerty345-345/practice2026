using System;

namespace task04
{
    public interface ISpaceship
    {
        void MoveForward();
        void Rotate(int angle);
        void Fire();
        int Speed { get; }
        int FirePower { get; }
    }

    public class Cruiser : ISpaceship
    {
        public int Speed => 50;
        public int FirePower => 100;

        public void MoveForward()
        {
            Console.WriteLine("cruiser moves forward slowly");
        }

        public void Rotate(int angle)
        {
            Console.WriteLine($"cruiser rotates by {angle} degrees");
        }

        public void Fire()
        {
            Console.WriteLine($"cruiser fires photon missile with power {FirePower}");
        }
    }

    public class Fighter : ISpaceship
    {
        public int Speed => 100;
        public int FirePower => 50;

        public void MoveForward()
        {
            Console.WriteLine("fighter moves forward fast");
        }

        public void Rotate(int angle)
        {
            Console.WriteLine($"fighter rotates by {angle} degrees");
        }

        public void Fire()
        {
            Console.WriteLine($"fighter fires photon missile with power {FirePower}");
        }
    }
}
