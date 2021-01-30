using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.UI
{
    public class Button : Element
    {
        private readonly Sprite _image;
        private readonly Sprite _clickedImage;
        private Sprite ActiveSprite => _clicked ? _clickedImage : _image;
        
        private bool _hovering;
        private bool _clicked;
        
        public override int Width => _image.Width;
        public override int Height => _image.Height;

        public Button(Sprite image, Sprite clickedImage, Vector2 position, float scale,
            Origin origin = Origin.TopLeft) : base(
            position, scale, origin)
        {
            _image = image;
            _clickedImage = clickedImage;
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            var oldHover = _hovering;
            
            _hovering = Bounds.Contains(Mouse.GetState().Position);

            if (oldHover)
            {
                if (!_hovering)
                {
                    HoverOff?.Invoke();
                }
            }
            else
            {
                if (_hovering)
                {
                    HoverOn?.Invoke();
                }
            }
            
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