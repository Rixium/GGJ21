using System;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Transitions;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Screens
{
    public class MainMenuScreen : IScreen
    {
        private readonly IRenderManager _renderManager;
        private readonly ITransitionManager _transitionManager;
        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.MainMenu;

        public MainMenuScreen(IRenderManager renderManager, ITransitionManager transitionManager)
        {
            _renderManager = renderManager;
            _transitionManager = transitionManager;
        }
        
        public void Load()
        {
            _transitionManager.Load();
        }

        public void Update(GameTime gameTime)
        {
            _transitionManager.Update(gameTime);
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin();
            _transitionManager.Draw();
            _renderManager.SpriteBatch.End();
        }
    }
}