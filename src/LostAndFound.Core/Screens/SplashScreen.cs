using System;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Screens
{
    public class SplashScreen : IScreen
    {
        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;
        private readonly IWindowConfiguration _windowConfiguration;
        private Texture2D _image;

        private double _splashTime = 5;

        public SplashScreen(IRenderManager renderManager, IContentChest contentChest,
            IWindowConfiguration windowConfiguration)
        {
            _renderManager = renderManager;
            _contentChest = contentChest;
            _windowConfiguration = windowConfiguration;
        }

        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.Splash;

        public void Load()
        {
            _image = _contentChest.Get<Texture2D>("images/splash");
        }

        public void Update(GameTime gameTime)
        {
            _splashTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_splashTime <= 0)
            {
                RequestScreenChange(ScreenType.MainMenu);
            }
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin();
            _renderManager.SpriteBatch.Draw(_image,
                new Rectangle(0, 0, _windowConfiguration.WindowWidth, _windowConfiguration.WindowHeight), Color.White);
            _renderManager.SpriteBatch.End();
        }
    }
}