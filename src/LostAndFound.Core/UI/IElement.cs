using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.UI
{
    public interface IElement
    {
        public Vector2 Position { get; set; }
        public IPanel Panel { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch renderManagerSpriteBatch);
    }
}