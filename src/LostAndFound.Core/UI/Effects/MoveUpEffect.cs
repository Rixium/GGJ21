using LostAndFound.Core.Extensions;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.UI.Effects
{
    public class MoveUpEffect : IElementEffect
    {
        public IElement Element { get; set; }

        public void Update(GameTime gameTime) => Element.Position -= new Vector2(0, gameTime.AsDelta() * 10);
    }
}