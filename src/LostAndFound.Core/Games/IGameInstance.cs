using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    public interface IGameInstance
    {
        void Load();
        void Start();
        void Draw();
        void Update(GameTime gameTime);
        IEntity GetPlayer();
    }
}