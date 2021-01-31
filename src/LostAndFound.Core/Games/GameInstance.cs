using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Interfaces;
using LostAndFound.Core.Games.Systems;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class GameInstance : IGameInstance
    {
        private readonly GameInterface _gameInterface;
        private readonly LightingOverlay _lightingOverlay;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private readonly SystemManager _systemManager;
        private readonly ZoneManager _zoneManager;
        private readonly SkyboxManager _skyboxManager;
        private readonly IRenderManager _renderManager;

        private readonly Camera _camera;

        public GameInstance(IRenderManager renderManager,
            IWindowConfiguration windowConfiguration,
            GameInterface gameInterface, 
            LightingOverlay lightingOverlay, IContentLoader<AsepriteSpriteMap> spriteMapLoader,
            SystemManager systemManager, ZoneManager zoneManager, SkyboxManager skyboxManager)
        {
            _renderManager = renderManager;
            _gameInterface = gameInterface;
            _lightingOverlay = lightingOverlay;
            _spriteMapLoader = spriteMapLoader;
            _systemManager = systemManager;
            _zoneManager = zoneManager;
            _skyboxManager = skyboxManager;

            _camera = new Camera(windowConfiguration);
        }

        public void Load()
        {
            _gameInterface.Load();
            _lightingOverlay.Load();
            _skyboxManager.Load();
            _zoneManager.Load();
        }

        public void Start()
        {
            var playerAnimationMap = _spriteMapLoader.GetContent("Assets/Images/Player/PlayerAnimations.json");

            var zoneColliders = _zoneManager.ActiveZone.Colliders.ToList();
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
            player.AddComponent(Program.Resolve<QuestHolderComponent>());

            player.Position = playerStartCollider.Bounds.ToVector2() - playerFeetBoxCollider.Offset;

            _camera.SetEntity(player, false);

            _systemManager.Start();
            _zoneManager.AddToActiveZone(player);
            _zoneManager.Start();
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            _skyboxManager.Draw();
            _renderManager.SpriteBatch.End();
            
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null,
                _camera.GetMatrix());
            _zoneManager.Draw(_renderManager.SpriteBatch);
            _renderManager.SpriteBatch.End();

            _lightingOverlay.Draw(_renderManager.SpriteBatch);
            
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            _gameInterface.Draw();
            _systemManager.Draw(_renderManager.SpriteBatch);
            _renderManager.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            _camera.Update(500, 281);
            _gameInterface.Update(gameTime);
            _zoneManager.Update(gameTime);
            _systemManager.Update(gameTime);
        }
    }
}