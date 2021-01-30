using System;
using System.Collections.Generic;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Transitions;
using LostAndFound.Core.UI;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Screens
{
    public class MainMenuScreen : IScreen
    {
        private readonly IRenderManager _renderManager;
        private readonly ITransitionManager _transitionManager;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.MainMenu;

        private IList<IPanel> _mainMenuPanels = new List<IPanel>();

        public MainMenuScreen(IRenderManager renderManager, ITransitionManager transitionManager, IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _renderManager = renderManager;
            _transitionManager = transitionManager;
            _spriteMapLoader = spriteMapLoader;
        }

        public void Load()
        {
            _transitionManager.Load();
            _spriteMapLoader.GetContent("");
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