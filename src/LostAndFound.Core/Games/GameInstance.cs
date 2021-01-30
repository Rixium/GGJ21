using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    class GameInstance : IGameInstance
    {
        private readonly IRenderManager _renderManager;
        private readonly IZoneLoader _zoneLoader;
        private readonly TimeManager _timeManager;

        private GameData _gameData = new GameData();
        private IList<ZoneData> _zoneData;

        public GameInstance(IRenderManager renderManager, IZoneLoader zoneLoader, TimeManager timeManager)
        {
            _renderManager = renderManager;
            _zoneLoader = zoneLoader;
            _timeManager = timeManager;
        }

        public void Load()
        {
            _zoneData = _zoneLoader.LoadZones();
            
            _gameData.ActiveZone = ZoneType.Test;
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin();

            var activeZone = _zoneData.First(x => x.ZoneType == _gameData.ActiveZone);
            _renderManager.SpriteBatch.Draw(activeZone.BackgroundImage, new Vector2(0, 0), Color.White);
            
            
            
            _renderManager.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            _timeManager.UpdateTime(gameTime);
        }
    }
}