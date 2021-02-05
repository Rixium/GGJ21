using System;
using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Extensions
{
    public static class ColliderExtensions
    {
        private static readonly Random Random = new Random();

        public static Vector2 GetRandomPositionInBounds(this Collider collider)
        {
            var randomX = Random.Next(collider.Bounds.Left, collider.Bounds.Right);
            var randomY = Random.Next(collider.Bounds.Top, collider.Bounds.Bottom);

            return new Vector2(randomX, randomY);
        }

        public static Vector2 GetRandomPositionInBounds(this Rectangle rectangle)
        {
            var randomX = Random.Next(rectangle.Left, rectangle.Right);
            var randomY = Random.Next(rectangle.Top, rectangle.Bottom);

            return new Vector2(randomX, randomY);
        }

        public static Rectangle Expand(this Rectangle rectangle, int size)
        {
            return new Rectangle(rectangle.X - size, rectangle.Y - size, rectangle.Width + size * 2,
                rectangle.Height + size * 2);
        }
    }
}