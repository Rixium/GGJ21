using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.UI
{
    public abstract class Element : IElement
    {
        public Action Click { get; set; }
        public float Scale { get; set; }
        public Vector2 Position { get; set; }
        public IPanel Panel { get; set; }
        public abstract Rectangle Bounds { get; }

        protected Element(Vector2 position, float scale)
        {
            Scale = scale;
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