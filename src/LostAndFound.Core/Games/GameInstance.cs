using System;
using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Interfaces;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    class GameInstance : IGameInstance
    {
        private GameInterface _gameInterface;

        private readonly IRenderManager _renderManager;
        private readonly IZoneLoader _zoneLoader;
        private readonly IContentChest _contentChest;
        private readonly Camera _camera;

        private GameData _gameData = new GameData();
        private IList<ZoneData> _zoneData;

        private ZoneData ActiveZone => _zoneData.First(x => x.ZoneType == _gameData.ActiveZone);
        private Player _player;

        public GameInstance(IRenderManager renderManager, IZoneLoader zoneLoader,
            IWindowConfiguration windowConfiguration, IContentChest contentChest)
        {
            _renderManager = renderManager;
            _zoneLoader = zoneLoader;
            _contentChest = contentChest;
            _camera = new Camera(windowConfiguration);
        }

        public void Load()
        {
            _zoneData = _zoneLoader.LoadZones();

            _gameData.ActiveZone = ZoneType.Test;
            _camera.Position = new Vector2(500, 500);

            _gameInterface = new GameInterface(_renderManager, _contentChest);
            _gameInterface.SetUp();
        }

        public void Start()
        {
            SetupGameData();

            var zoneColliders = ActiveZone.Colliders.ToList();
            var playerStartCollider = zoneColliders.First(x => x.Name.Equals("PlayerStart"));
            _gameData.PlayerData.Position = playerStartCollider.Bounds.ToVector2();

            _player = new Player(_contentChest.Get<Texture2D>("Images/Player/Idle_1"), _gameData);
            _camera.SetEntity(_gameData.PlayerData, true);

            _player.PlayerMove += CanPlayerMove;
        }

        public bool CanPlayerMove(Movement movement, Rectangle bounds)
        {
            var newBounds = new Rectangle(bounds.X + movement.X, bounds.Y + movement.Y, bounds.Width, bounds.Height);
            return ActiveZone.Colliders.Any(x => x.Bounds.Intersects(newBounds));
        }

        private void SetupGameData()
        {
            _gameData = new GameData
            {
                ActiveZone = ZoneType.Test,
                PersonData = Array.Empty<PersonData>(),
                PlayerData = new PlayerData()
            };
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null,
                _camera.GetMatrix());

            _renderManager.SpriteBatch.Draw(ActiveZone.BackgroundImage, new Vector2(0, 0), Color.White);
            _renderManager.SpriteBatch.Draw(_player.Image, _gameData.PlayerData.Position, Color.White);

            _renderManager.SpriteBatch.End();
            
            _renderManager.SpriteBatch.Begin();
            _gameInterface.Draw();
            _renderManager.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            _camera.Update(20000, 20000);
            _player.Update(_gameData.PlayerData, gameTime);
            _camera.ToGo = _gameData.PlayerData.Position;
            _gameInterface.Update(gameTime);
        }
    }
}