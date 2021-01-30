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

        public Button(Sprite image, Sprite hoverImage, Vector2 position, float scale) : base(position, scale)
        {
            _image = image;
            _hoverImage = hoverImage;
        }

        public override Rectangle Bounds =>
            new Rectangle((int) Position.X, (int) Position.Y, (int) (_image.Width * Scale),
                (int) (_image.Height * Scale));
        
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
            spriteBatch.Draw(activeImage.Texture, Position, activeImage.Source, Color.White, 0f, Vector2.Zero, Scale,
                SpriteEffects.None, 0f);
        }
    }
}