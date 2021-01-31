using LostAndFound.Core.Extensions;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.UI.Effects
{
    public class FadeOutEffect : IElementEffect
    {
        public IElement Element { get; set; }
        public void Update(GameTime gameTime)
        {
            Element.Opacity -= gameTime.AsDelta();

            if (Element.Opacity <= 0)
            {
                Element.MarkForDeath();
            }
        }
    }
}