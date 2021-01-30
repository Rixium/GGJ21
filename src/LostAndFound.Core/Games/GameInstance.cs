using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    class GameInstance : IGameInstance
    {
        private readonly IRenderManager _renderManager;
        private readonly IZoneLoader _zoneLoader;
        private readonly Camera _camera;

        private GameData _gameData = new GameData();
        private IList<ZoneData> _zoneData;

        public GameInstance(IRenderManager renderManager, IZoneLoader zoneLoader,
            IWindowConfiguration windowConfiguration)
        {
            _renderManager = renderManager;
            _zoneLoader = zoneLoader;
            _camera = new Camera(windowConfiguration);
        }

        public void Load()
        {
            _zoneData = _zoneLoader.LoadZones();

            _gameData.ActiveZone = ZoneType.Test;
            _camera.ToGo = new Vector2(500, 500);
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null,
                _camera.GetMatrix());

            var activeZone = _zoneData.First(x => x.ZoneType == _gameData.ActiveZone);
            _renderManager.SpriteBatch.Draw(activeZone.BackgroundImage, new Vector2(0, 0), Color.White);

            _renderManager.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            _camera.Update(20000, 20000);
        }
    }
}