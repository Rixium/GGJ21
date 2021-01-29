using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Graphics
{
    internal class RenderManager : IRenderManager
    {
        private SpriteBatch _spriteBatch;

        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }
    }
}