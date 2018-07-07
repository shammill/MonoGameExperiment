using System;
using System.Diagnostics.Contracts;

namespace Engine.Utility
{
    public static class RandomHelper
    {
        private static readonly Random globalRandom = new Random();

        [ThreadStatic]
        private static Random _random;

        private static Random Random
        {
            get { return _random ?? (_random = new Random(globalRandom.Next())); }
        }

        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        public static bool NextBool()
        {
            return (Random.Next(0, 2) > 0);
        }

        public static float NextFloat()
        {
            return (float)Random.NextDouble();
        }

        public static double NextDouble()
        {
            return Random.NextDouble();
        }

    }
}
