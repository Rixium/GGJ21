using System;
using System.Collections.Generic;
using LostAndFound.Core.UI.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.UI
{
    public abstract class Element : IElement
    {
        public bool MarkedForDeath { get; set; } = false;
        public IList<IElementEffect> ElementEffects { get; } = new List<IElementEffect>();
        public Origin Origin { get; }
        public Action Click { get; set; }
        public Action HoverOn { get; set; }
        public Action HoverOff { get; set; }

        public Rectangle Bounds
        {
            get
            {
                var (xOffset, yOffset) = GetOriginOffset();
                return new Rectangle((int) (Position.X - xOffset), (int) (Position.Y - yOffset),
                    (int) (Width * Scale),
                    (int) (Height * Scale));
            }
        }

        public Vector2 Bottom
        {
            get
            {
                var (xOffset, yOffset) = GetOriginOffset();
                return new Vector2(Bounds.Left + xOffset, Bounds.Bottom + yOffset);
            }
        }

        public float Scale { get; set; }
        public Vector2 Position { get; set; }
        public IPanel Panel { get; set; }
        public float Opacity { get; set; } = 1f;
        public abstract int Width { get; }
        public abstract int Height { get; }

        protected Element(Vector2 position, float scale, Origin origin)
        {
            Origin = origin;
            Scale = scale;
            Position = position;
        }

        protected Vector2 GetOriginOffset()
        {
            switch (Origin)
            {
                case Origin.TopLeft:
                    return new Vector2(0, 0);
                case Origin.Center:
                    return new Vector2(Width / 2f * Scale, Height / 2f * Scale);
                case Origin.TopRight:
                    return new Vector2(Width * Scale, 0);
                default:
                    return new Vector2(0, 0);
            }
        }

        public void Update(GameTime gameTime)
        {
            InternalUpdate(gameTime);

            foreach (var elementEffect in ElementEffects)
            {
                elementEffect.Update(gameTime);
            }
        }

        protected abstract void InternalUpdate(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            InternalDraw(spriteBatch);
        }

        public void AddEffect(IElementEffect elementEffect)
        {
            elementEffect.Element = this;
            ElementEffects.Add(elementEffect);
        }

        public void MarkForDeath() => MarkedForDeath = true;

        protected abstract void InternalDraw(SpriteBatch spriteBatch);
    }
}