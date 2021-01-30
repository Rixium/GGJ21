using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    public interface IGameInstance
    {
        public GameData GameData { get; }
        void Load();
        void Start();
        void Draw();
        void Update(GameTime gameTime);
    }
}