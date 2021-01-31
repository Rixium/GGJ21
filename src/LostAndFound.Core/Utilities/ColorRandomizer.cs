using System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Utilities
{
    public static class ColorRandomizer
    {
        private static readonly Random Random = new Random();

        public static Color GetRandomColor() =>
            new Color(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
    }
}