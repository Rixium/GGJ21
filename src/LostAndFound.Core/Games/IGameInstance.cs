using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    public interface IGameInstance
    {
        void Draw();
        void Update(GameTime gameTime);
    }
}