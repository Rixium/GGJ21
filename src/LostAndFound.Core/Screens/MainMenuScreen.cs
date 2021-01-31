using System;
using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Transitions;
using LostAndFound.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace LostAndFound.Core.Screens
{
    public class MainMenuScreen : IScreen
    {
        private readonly IRenderManager _renderManager;
        private readonly IWindowConfiguration _windowConfiguration;
        private readonly ITransitionManager _transitionManager;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private readonly IContentChest _contentChest;
        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.MainMenu;

        private readonly IList<IPanel> _mainMenuPanels = new List<IPanel>();
        private readonly float _uiScale = 3f;
        private IPanel ActivePanel => _mainMenuPanels.FirstOrDefault(x => x.Name.Equals(_activePanelName));
        private string _activePanelName = "Main";


        public MainMenuScreen(IRenderManager renderManager, IWindowConfiguration windowConfiguration,
            ITransitionManager transitionManager, IContentLoader<AsepriteSpriteMap> spriteMapLoader,
            IContentChest contentChest)
        {
            _renderManager = renderManager;
            _windowConfiguration = windowConfiguration;
            _transitionManager = transitionManager;
            _spriteMapLoader = spriteMapLoader;
            _contentChest = contentChest;
        }

        public void Load()
        {
            _transitionManager.Load();

            var spriteMap = _spriteMapLoader.GetContent("Assets/UI/ui.json");

            var titleSprite = spriteMap.CreateSpriteFromRegion("tittle");
            var upButton = spriteMap.CreateSpriteFromRegion("play up");
            var downButton = spriteMap.CreateSpriteFromRegion("play down");
            var quitUp = spriteMap.CreateSpriteFromRegion("quit up");
            var quitDown = spriteMap.CreateSpriteFromRegion("quit down");
            // var optionsUp = spriteMap.CreateSpriteFromRegion("options up");
            // var optionsDown = spriteMap.CreateSpriteFromRegion("options down");
            // var petItUp = spriteMap.CreateSpriteFromRegion("pet it up");
            // var petItDown = spriteMap.CreateSpriteFromRegion("pet it down");

            var mainPanel = new Panel(_renderManager, "Main");
            var optionsPanel = new Panel(_renderManager, "Options");

            var titleImage = new Image(titleSprite, _windowConfiguration.Center - new Vector2(0, 100), _uiScale,
                Origin.Center);
            var playButton = new Button(upButton, downButton, titleImage.Bottom + new Vector2(0, 30),
                _uiScale, Origin.Center);
            // var optionsButton = new Button(optionsUp, optionsDown, playButton.Bottom, _uiScale, Origin.Center);
            var quitButton = new Button(quitUp, quitDown, playButton.Bottom, _uiScale, Origin.Center);
            // var petItButton = new Button(petItUp, petItDown, optionsButton.Bottom, _uiScale, Origin.Center);

            playButton.HoverOn = () => { _contentChest.Get<SoundEffect>("Audio/SoundEffects/buttonHover").Play(); };
            // optionsButton.HoverOn = () => { _contentChest.Get<SoundEffect>("Audio/SoundEffects/buttonHover").Play(); };
            quitButton.HoverOn = () => { _contentChest.Get<SoundEffect>("Audio/SoundEffects/buttonHover").Play(); };

            playButton.Click = () =>
            {
                playButton.Click = null;
                _contentChest.Get<SoundEffect>("Audio/SoundEffects/buttonClick").Play();
                _transitionManager.SetState(FadeState.FadingOut);
                _transitionManager.FadeOutEnded = () =>
                {
                    RequestScreenChange?.Invoke(ScreenType.GameScreen);
                    MediaPlayer.Play(_contentChest.Get<Song>("Audio/Music/game"));
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.2f;
                };
            };
            //
            // optionsButton.Click = () =>
            // {
            //     _activePanelName = "Options";
            //     _contentChest.Get<SoundEffect>("Audio/SoundEffects/buttonClick").Play();
            // };

            quitButton.Click = Game1.Quit;

            mainPanel.AddElement(titleImage);
            mainPanel.AddElement(playButton);
            // mainPanel.AddElement(optionsButton);
            mainPanel.AddElement(quitButton);

            optionsPanel.AddElement(titleImage);

            _mainMenuPanels.Add(mainPanel);
            _mainMenuPanels.Add(optionsPanel);
        }

        public void Update(GameTime gameTime)
        {
            _transitionManager.Update(gameTime);

            ActivePanel?.Update(gameTime);
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            ActivePanel?.Draw();

            _transitionManager.Draw();
            _renderManager.SpriteBatch.End();
        }

        public void OnMadeActiveScreen()
        {
            
        }
    }
}