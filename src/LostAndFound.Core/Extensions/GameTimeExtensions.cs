using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Extensions
{
    public static class GameTimeExtensions
    {
        public static float AsDelta(this GameTime gameTime) =>
            (float) gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
    }
}