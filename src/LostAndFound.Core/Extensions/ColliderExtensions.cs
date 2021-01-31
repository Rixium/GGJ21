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
            var randomX = Random.Next(collider.Bounds.X, collider.Bounds.Right);
            var randomY = Random.Next(collider.Bounds.Y, collider.Bounds.Bottom);

            return new Vector2(randomX, randomY);
        }
    }
}