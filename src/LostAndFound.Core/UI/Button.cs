using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.UI
{
    public class Button : Element
    {
        private readonly Sprite _image;
        private readonly Sprite _hoverImage;
        private bool _hovering;
        public override int Width => _image.Width;
        public override int Height => _image.Height;

        public Button(Sprite image, Sprite hoverImage, Vector2 position, float scale,
            Origin origin = Origin.TopLeft) : base(
            position, scale, origin)
        {
            _image = image;
            _hoverImage = hoverImage;
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            _hovering = Bounds.Contains(Mouse.GetState().Position);

            if (!_hovering)
            {
                return;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke();
            }
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            var activeImage = _hovering ? _hoverImage : _image;
            spriteBatch.Draw(activeImage.Texture, Bounds, activeImage.Source, Color.White);
        }
    }
}