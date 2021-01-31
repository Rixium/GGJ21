using System;
using System.Collections.Generic;
using LostAndFound.Core.UI.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.UI
{
    public enum Origin
    {
        TopLeft,
        Center,
        TopRight
    }

    public interface IElement
    {
        public bool MarkedForDeath { get; set; }
        public IList<IElementEffect> ElementEffects { get; }
        public Origin Origin { get; }
        public Action Click { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; }
        public float Scale { get; set; }
        public IPanel Panel { get; set; }
        float Opacity { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch renderManagerSpriteBatch);
        void AddEffect(IElementEffect elementEffect);
        void MarkForDeath();
    }
}