using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    class GameInstance : IGameInstance
    {
        private readonly IZoneLoader _zoneLoader;
        private GameData _gameData = new GameData();
        private ZoneData _zoneData;

        public GameInstance(IZoneLoader zoneLoader)
        {
            _zoneLoader = zoneLoader;
        }

        public void Load()
        {
            _zoneLoader.LoadZones();
        }
        
        public void Draw()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}