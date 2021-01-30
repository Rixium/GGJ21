using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.UI
{
    public abstract class Element : IElement
    {
        public Vector2 Position { get; set; }
        public IPanel Panel { get; set; }

        protected Element(Vector2 position)
        {
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            InternalUpdate(gameTime);
        }

        protected abstract void InternalUpdate(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            InternalDraw(spriteBatch);
        }

        protected abstract void InternalDraw(SpriteBatch spriteBatch);
    }
}