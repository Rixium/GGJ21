using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Interfaces;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private readonly TimeManager _timeManager;
        private readonly LightingOverlay _lightingOverlay;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;

        private readonly Camera _camera;

        public GameInstance(IRenderManager renderManager, IZoneLoader zoneLoader,
            IWindowConfiguration windowConfiguration, IContentChest contentChest, GameInterface gameInterface,
            TimeManager timeManager, LightingOverlay lightingOverlay, IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _renderManager = renderManager;
            _zoneLoader = zoneLoader;
            _contentChest = contentChest;
            _gameInterface = gameInterface;
            _timeManager = timeManager;
            _lightingOverlay = lightingOverlay;
            _spriteMapLoader = spriteMapLoader;

            _camera = new Camera(windowConfiguration);
        }

        public void Load()
        {
            _gameInterface.Load();
            _lightingOverlay.Load();

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
            var playerAnimationMap = _spriteMapLoader.GetContent("Assets/Images/Player/PlayerAnimations.json");
            
            var zoneColliders = ActiveZone.Colliders.ToList();
            var playerStartCollider = zoneColliders.First(x => x.Name.Equals("PlayerStart"));

            var player = new Entity(playerStartCollider.Bounds.ToVector2());
            var playerFeetBoxCollider = Program.Resolve<BoxColliderComponent>();
            playerFeetBoxCollider.Width = 14;
            playerFeetBoxCollider.Height = 5;
            playerFeetBoxCollider.Offset = new Vector2(0, 49);
            var playerSoundComponent = Program.Resolve<SoundComponent>();

            var animatorComponent = Program.Resolve<AnimatorComponent>();
            animatorComponent.AddAnimation("Walk_Right", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_1"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_2"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_3"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_4"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Right_5")
            })
            {
                FrameDuration = 0.2f
            });
            
            animatorComponent.AddAnimation("Walk_Left", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_1"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_2"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_3"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_4"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Left_5")
            })
            {
                FrameDuration = 0.2f
            });
            
            animatorComponent.AddAnimation("Walk_Up", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Walk_Up_1"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Up_2")
            })
            {
                FrameDuration = 0.2f
            });
            
            animatorComponent.AddAnimation("Walk_Down", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_1"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_2"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_3"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_4"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_5"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_6"),
                playerAnimationMap.CreateSpriteFromRegion("Walk_Down_7")
            })
            {
                FrameDuration = 0.2f
            });
            
            animatorComponent.AddAnimation("Idle", new Animation(new List<Sprite>
            {
                playerAnimationMap.CreateSpriteFromRegion("Idle_1")
            })
            {
                FrameDuration = 0.2f
            });
            
            player.AddComponent(Program.Resolve<PlayerAnimationComponent>());
            player.AddComponent(Program.Resolve<AnimationDrawComponent>());
            player.AddComponent(playerFeetBoxCollider);
            player.AddComponent(animatorComponent);
            player.AddComponent(Program.Resolve<PlayerControllerComponent>());
            player.AddComponent(Program.Resolve<ZoneInteractionComponent>());
            player.AddComponent(playerSoundComponent);
            player.AddComponent(Program.Resolve<PlayerSoundManagerComponent>());

            player.Position = playerStartCollider.Bounds.ToVector2() - playerFeetBoxCollider.Offset;
            
            _camera.SetEntity(player, false);
            ActiveZone.Entities.Add(player);

            foreach (var zone in _zones)
            {
                zone.Start();
            }
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null,
                _camera.GetMatrix());
            ActiveZone.Draw(_renderManager.SpriteBatch);
            _renderManager.SpriteBatch.End();
            
            _renderManager.SpriteBatch.Begin();
            _lightingOverlay.Draw();
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
            _timeManager.UpdateTime(gameTime);
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