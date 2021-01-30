using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.UI
{
    public class Button : Element
    {
        private Texture2D _image;
        private Texture2D _hoverImage;

        public Button(Vector2 position, Texture2D image, Texture2D hoverImage) : base(position)
        {
            _image = image;
            _hoverImage = hoverImage;
        }


        protected override void InternalUpdate(GameTime gameTime)
        {
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
        }
    }
}