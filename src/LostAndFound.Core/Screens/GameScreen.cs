using System;
using LostAndFound.Core.Games;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Transitions;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Screens
{
    public class GameScreen : IScreen
    {
        private readonly IRenderManager _renderManager;
        private readonly IGameInstance _gameInstance;
        private readonly ITransitionManager _transitionManager;

        public GameScreen(IRenderManager renderManager, IGameInstance gameInstance,
            ITransitionManager transitionManager)
        {
            _renderManager = renderManager;
            _gameInstance = gameInstance;

            _transitionManager = transitionManager;
            _transitionManager.SetFade(1);
        }

        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.GameScreen;

        public void Load()
        {
            _gameInstance.Load();
            _transitionManager.Load();
            
            _gameInstance.Start();
        }

        public void Update(GameTime gameTime)
        {
            _gameInstance.Update(gameTime);
            _transitionManager.Update(gameTime);
        }

        public void Draw()
        {
            _gameInstance.Draw();

            _renderManager.SpriteBatch.Begin();
            _transitionManager.Draw();
            _renderManager.SpriteBatch.End();
        }
    }
}