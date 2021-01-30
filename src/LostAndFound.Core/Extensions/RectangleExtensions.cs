using System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Extensions
{
    public static class RectangleExtensions
    {
        public static Vector2 ToVector2(this Rectangle rectangle) => new Vector2(rectangle.X, rectangle.Y);

        public static Rectangle Add(this Rectangle rectangle, Rectangle rectangleToAdd) => new Rectangle(
            rectangle.X + rectangleToAdd.X, rectangle.Y + rectangleToAdd.Y, rectangle.Width, rectangle.Height);

        public static Vector2 GetIntersectionDepth(this Rectangle rectA, Rectangle rectB)
        {
            // Calculate half sizes.
            var halfWidthA = rectA.Width / 2.0f;
            var halfHeightA = rectA.Height / 2.0f;
            var halfWidthB = rectB.Width / 2.0f;
            var halfHeightB = rectB.Height / 2.0f;

            // Calculate centers.
            var centerA = new Vector2(rectA.Left + halfWidthA, rectA.Top + halfHeightA);
            var centerB = new Vector2(rectB.Left + halfWidthB, rectB.Top + halfHeightB);

            // Calculate current and minimum-non-intersecting distances between centers.
            var distanceX = centerA.X - centerB.X;
            var distanceY = centerA.Y - centerB.Y;
            var minDistanceX = halfWidthA + halfWidthB;
            var minDistanceY = halfHeightA + halfHeightB;

            // If we are not intersecting at all, return (0, 0).
            if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
                return Vector2.Zero;

            // Calculate and return intersection depths.
            var depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
            var depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
            return new Vector2(depthX, depthY);
        }
    }
}