using System;
using Asepreadr;
using LostAndFound.Core.Config;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Transitions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace LostAndFound.Core.Screens
{
    public class SplashScreen : IScreen
    {
        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;
        private readonly IWindowConfiguration _windowConfiguration;
        private readonly ITransitionManager _transitionManager;
        private Texture2D _image;

        private double _splashTime = 5;

        public SplashScreen(IRenderManager renderManager, IContentChest contentChest,
            IWindowConfiguration windowConfiguration, ITransitionManager transitionManager)
        {
            _renderManager = renderManager;
            _contentChest = contentChest;
            _windowConfiguration = windowConfiguration;
            _transitionManager = transitionManager;
        }

        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.Splash;

        public void Load()
        {
            _image = _contentChest.Get<Texture2D>("images/splash");
            _transitionManager.Load();

            _transitionManager.FadeInEnded = () =>
            {
                _contentChest.Get<SoundEffect>("Audio/SoundEffects/start").Play();
            };
            
            _transitionManager.FadeOutEnded = () => RequestScreenChange(ScreenType.MainMenu);
        }

        public void Update(GameTime gameTime)
        {
            _transitionManager.Update(gameTime);
            
            if (_splashTime <= 0)
            {
                return;
            }
            
            _splashTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_splashTime <= 0)
            {
                _transitionManager.SetState(FadeState.FadingOut);
                var song = _contentChest.Get<Song>("Audio/Music/Floating");
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.5f;
                MediaPlayer.Play(song);
            }
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin();
            _renderManager.SpriteBatch.Draw(_image,
                new Rectangle(0, 0, _windowConfiguration.WindowWidth, _windowConfiguration.WindowHeight), Color.White);
            _transitionManager.Draw();
            _renderManager.SpriteBatch.End();
        }

        public void OnMadeActiveScreen()
        {
            
        }
    }
}