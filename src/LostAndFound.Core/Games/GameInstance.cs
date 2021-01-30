using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Content;
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
        private readonly TimeManager _timeManager;
        private readonly IContentChest _contentChest;

        private GameData _gameData = new GameData();
        private IList<ZoneData> _zoneData;

        private SpriteFont _font;

        public GameInstance(IRenderManager renderManager, IZoneLoader zoneLoader, TimeManager timeManager,
            IContentChest contentChest)
        {
            _renderManager = renderManager;
            _zoneLoader = zoneLoader;
            _timeManager = timeManager;
            _contentChest = contentChest;
        }

        public void Load()
        {
            _zoneData = _zoneLoader.LoadZones();

            _gameData.ActiveZone = ZoneType.Test;

            _font = _contentChest.ContentManager.Load<SpriteFont>("Fonts/DefaultFont");
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin();

            var activeZone = _zoneData.First(x => x.ZoneType == _gameData.ActiveZone);
            _renderManager.SpriteBatch.Draw(activeZone.BackgroundImage, new Vector2(0, 0), Color.White);

            _renderManager.SpriteBatch.DrawString(_font, $"H: {_timeManager.Hour} M: {(int)_timeManager.Minutes}",
                new Vector2(300, 50), Color.White);

            _renderManager.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            _timeManager.UpdateTime(gameTime);
        }
    }
}