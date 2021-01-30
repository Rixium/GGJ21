using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    public interface IGameInstance
    {
        public IZone ActiveZone { get; }
        void Load();
        void Start();
        void Draw();
        void Update(GameTime gameTime);
    }
}