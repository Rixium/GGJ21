using LostAndFound.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.UI
{
    public class Text : Element
    {
        private SpriteFont _font;
        private readonly Color _color;
        private string _text { get; set; }

        public Text(SpriteFont font, string text, Color color, Vector2 position, float scale, Origin origin) : base(
            position, scale,
            origin)
        {
            _font = font;
            _color = color;
            _text = text;
        }

        public void SetText(string text) => _text = text;

        public override int Width => (int) _font.MeasureString(_text).X;
        public override int Height => (int) _font.MeasureString(_text).Y;

        protected override void InternalUpdate(GameTime gameTime)
        {
        }

        protected override void InternalDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                _font,
                _text,
                Bounds.ToVector2(),
                _color);
        }
    }
}