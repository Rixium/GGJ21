using Microsoft.Xna.Framework;

namespace LostAndFound.Core.UI
{
    public interface IPanel
    {
        void AddElement<T>(T element) where T : IElement;
        void Update(GameTime gameTime);
        void Draw();
    }
}