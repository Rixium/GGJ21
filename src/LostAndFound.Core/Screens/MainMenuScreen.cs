using System;
using System.Collections.Generic;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Transitions;
using LostAndFound.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Screens
{
    public class MainMenuScreen : IScreen
    {
        private readonly IRenderManager _renderManager;
        private readonly IWindowConfiguration _windowConfiguration;
        private readonly ITransitionManager _transitionManager;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.MainMenu;

        private readonly IList<IPanel> _mainMenuPanels = new List<IPanel>();
        private readonly float _uiScale = 4f;

        public MainMenuScreen(IRenderManager renderManager, IWindowConfiguration windowConfiguration,
            ITransitionManager transitionManager, IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _renderManager = renderManager;
            _windowConfiguration = windowConfiguration;
            _transitionManager = transitionManager;
            _spriteMapLoader = spriteMapLoader;
        }

        public void Load()
        {
            _transitionManager.Load();

            var spriteMap = _spriteMapLoader.GetContent("Assets/UI/ui.json");
            var upButton = spriteMap.CreateSpriteFromRegion("play up");
            var downButton = spriteMap.CreateSpriteFromRegion("play down");
            var quitUp = spriteMap.CreateSpriteFromRegion("quit up");
            var quitDown = spriteMap.CreateSpriteFromRegion("quit down");

            var mainPanel = new Panel(_renderManager);
            var playButton = new Button(upButton, downButton, _windowConfiguration.Center, _uiScale, Origin.Center);
            var quitButton = new Button(quitUp, quitDown, _windowConfiguration.Center + new Vector2(0, 100), _uiScale,Origin.Center);

            playButton.Click = () =>
            {
                playButton.Click = null;
                _transitionManager.SetState(FadeState.FadingOut);
                _transitionManager.FadeOutEnded = () => { RequestScreenChange?.Invoke(ScreenType.GameScreen); };
            };

            quitButton.Click = () =>
            {
                Game1.Quit();
            };

            mainPanel.AddElement(playButton);
            mainPanel.AddElement(quitButton);

            _mainMenuPanels.Add(mainPanel);
        }

        public void Update(GameTime gameTime)
        {
            _transitionManager.Update(gameTime);

            foreach (var panel in _mainMenuPanels)
            {
                panel.Update(gameTime);
            }
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);


            foreach (var panel in _mainMenuPanels)
            {
                panel.Draw();
            }
            
            _transitionManager.Draw();
            _renderManager.SpriteBatch.End();
        }
    }
}