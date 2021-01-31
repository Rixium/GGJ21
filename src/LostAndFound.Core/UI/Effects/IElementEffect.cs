using Microsoft.Xna.Framework;

namespace LostAndFound.Core.UI.Effects
{
    public interface IElementEffect
    {
        public IElement Element { get; set; }
        public void Update(GameTime gameTime);
    }
}