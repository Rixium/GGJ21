using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 Add(this Vector2 vector2, float x, float y) => vector2 + new Vector2(x, y);
    }
}