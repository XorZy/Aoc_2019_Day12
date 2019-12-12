using System;
using System.IO;
using System.Linq;

namespace Day__12
{
    internal class Moon
    {
        public int X;
        public int Y;
        public int Z;

        public int vX;
        public int vY;
        public int vZ;

        public Moon(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Moon Parse(string input)
        {
            var split = input.Split(',').Select(x => new string(x.Where(y => char.IsNumber(y) || y == '-').ToArray())).Select(int.Parse).ToArray();
            return new Moon(split[0], split[1], split[2]);
        }
    }

    internal class Program
    {
        private static void Main()
        {
            var moons = File.ReadAllLines("input3").Select(Moon.Parse).ToArray();

            long steps = 0;

            long xCycle = -1;
            long yCycle = -1;
            long zCycle = -1;

            while (true)
            {
                foreach (var moon in moons)
                {
                    foreach (var otherMoon in moons.Where(x => x != moon))
                    {
                        var deltaX = Math.Sign(otherMoon.X - moon.X);
                        var deltaY = Math.Sign(otherMoon.Y - moon.Y);
                        var deltaZ = Math.Sign(otherMoon.Z - moon.Z);

                        moon.vX += deltaX;
                        moon.vY += deltaY;
                        moon.vZ += deltaZ;
                    }
                }

                foreach (var moon in moons)
                {
                    moon.X += moon.vX;
                    moon.Y += moon.vY;
                    moon.Z += moon.vZ;
                }

                steps++;

                if (xCycle == -1 && moons.All(x => x.vX == 0))
                    xCycle = steps;

                if (yCycle == -1 && moons.All(x => x.vY == 0))
                    yCycle = steps;

                if (zCycle == -1 && moons.All(x => x.vZ == 0))
                    zCycle = steps;

                if (xCycle > -1 && yCycle > -1 && zCycle > -1)
                {
                    Console.WriteLine(MathNet.Numerics.Euclid.LeastCommonMultiple(new[] { xCycle, yCycle, zCycle }) * 2);
                    break;
                }
            }
        }
    }
}
