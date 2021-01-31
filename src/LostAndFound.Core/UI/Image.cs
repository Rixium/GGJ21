using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.UI
{
    public class Image : Element
    {
        private readonly Sprite _image;

        public Image(Sprite image, Vector2 position, float scale, Origin origin) : base(position, scale, origin)
        {
            _image = image;
        }

        public override int Width => _image.Width;
        public override int Height => _image.Height;

        protected override void InternalUpdate(GameTime gameTime)
        {
        }

        protected override void InternalDraw(SpriteBatch spriteBatch) =>
            spriteBatch.Draw(_image.Texture, Bounds, _image.Source, Color.White * Opacity);
    }
}