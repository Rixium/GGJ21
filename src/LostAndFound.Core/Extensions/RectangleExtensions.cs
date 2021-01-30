using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Extensions
{
    public static class RectangleExtensions
    {
        public static Vector2 ToVector2(this Rectangle rectangle) => new Vector2(rectangle.X, rectangle.Y);
    }
}