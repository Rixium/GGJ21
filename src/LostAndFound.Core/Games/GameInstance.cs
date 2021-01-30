using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Interfaces;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class GameInstance : IGameInstance
    {
        private const ZoneType StartingZone = ZoneType.Street;

        private readonly IList<IZone> _zones = new List<IZone>();
        public IZone ActiveZone => _zones.First(x => x.ZoneType == _currentZone);
        private ZoneType _currentZone = StartingZone;

        private readonly IZoneLoader _zoneLoader;

        private readonly GameInterface _gameInterface;
        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;

        private readonly Camera _camera;

        public GameInstance(IRenderManager renderManager, IZoneLoader zoneLoader,
            IWindowConfiguration windowConfiguration, IContentChest contentChest, GameInterface gameInterface)
        {
            _renderManager = renderManager;
            _zoneLoader = zoneLoader;
            _contentChest = contentChest;
            _gameInterface = gameInterface;

            _camera = new Camera(windowConfiguration);
        }

        public void Load()
        {
            _gameInterface.Load();

            var zoneData = _zoneLoader.LoadZones();

            foreach (var zone in zoneData)
            {
                _zones.Add(new Zone
                {
                    ZoneType = zone.ZoneType,
                    Entities = new List<IEntity>(),
                    Colliders = zone.Colliders,
                    Image = zone.BackgroundImage
                });
            }

            _currentZone = StartingZone;
        }

        public void Start()
        {
            var zoneColliders = ActiveZone.Colliders.ToList();
            var playerStartCollider = zoneColliders.First(x => x.Name.Equals("PlayerStart"));

            var player = new Entity(playerStartCollider.Bounds.ToVector2());
            var playerImage = _contentChest.Get<Texture2D>("Images/Player/Idle_1");
            var playerFeetBoxCollider = Program.Resolve<BoxColliderComponent>();
            playerFeetBoxCollider.Width = 14;
            playerFeetBoxCollider.Height = 5;
            playerFeetBoxCollider.Offset = new Vector2(0, 49);

            var staticDrawComponent = Program.Resolve<StaticDrawComponent>();
            staticDrawComponent.Image = playerImage;

            player.AddComponent(staticDrawComponent);
            player.AddComponent(playerFeetBoxCollider);
            player.AddComponent(Program.Resolve<PlayerControllerComponent>());
            player.AddComponent(Program.Resolve<ZoneInteractionComponent>());

            _camera.SetEntity(player, false);
            ActiveZone.Entities.Add(player);
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null,
                _camera.GetMatrix());
            ActiveZone.Draw(_renderManager.SpriteBatch);
            _renderManager.SpriteBatch.End();

            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            _gameInterface.Draw();
            _renderManager.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            _camera.Update(500, 281);
            _gameInterface.Update(gameTime);
            ActiveZone.Update(gameTime);
        }

        public IZone GetZone(ZoneType zoneType) => _zones.FirstOrDefault(x => x.ZoneType == zoneType);
        public void SetActiveZone(ZoneType zoneType) => _currentZone = zoneType;

        public void MoveEntityToZone(IZone oldZone, IZone zoneToGoTo, IEntity entity)
        {
            oldZone.RemoveEntity(entity);
            zoneToGoTo.AddEntity(entity);
        }
    }
}