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
        private Sprite ActiveSprite => _clicked ? _hoverImage : _image;
        
        private bool _hovering;
        private bool _clicked;
        
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

            _clicked = Mouse.GetState().LeftButton == ButtonState.Pressed;
            
            if (_clicked)
            {
                Click?.Invoke();
            }
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            var color = _hovering ? Color.White : Color.White * 0.8f;
            spriteBatch.Draw(ActiveSprite.Texture, Bounds, ActiveSprite.Source, color);
        }
    }
}