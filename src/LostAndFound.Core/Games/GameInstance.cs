using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
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
        private Entity _player;

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
            var zoneColliders = _zoneManager.ActiveZone.Colliders.ToList();
            var playerStartCollider = zoneColliders.First(x => x.Name.Equals("PlayerStart"));

            var player = new Entity(playerStartCollider.Bounds.ToVector2());
            var playerFeetBoxCollider = Program.Resolve<BoxColliderComponent>();
            playerFeetBoxCollider.Width = 14;
            playerFeetBoxCollider.Height = 5;
            playerFeetBoxCollider.Offset = new Vector2(0, 49);
            var playerSoundComponent = Program.Resolve<SoundComponent>();
            var animatorComponent = Program.Resolve<AnimatorComponent>();
            animatorComponent.SetUp(Program.Resolve<PlayerAnimationSet>());
            
            player.AddComponent(Program.Resolve<LightComponent>());
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

            _player = player;
            
            _gameInterface.RegisterToEntity(player);
            _systemManager.Start();
            _zoneManager.AddToActiveZone(player);
            _zoneManager.Start();
            _lightingOverlay.Start();
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
            
            _lightingOverlay.Draw(_camera);
            
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