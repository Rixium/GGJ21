using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Input;
using LostAndFound.Core.Screens;
using LostAndFound.Core.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core
{
    internal class Game1 : Game
    {
        private const ScreenType StartingScreen = ScreenType.GameScreen;
            
        private static Game1 _instance;
        private readonly IRenderManager _renderManager;
        private readonly IScreenManager _screenManager;
        private readonly IContentChest _contentChest;
        private readonly IWindowConfiguration _windowConfiguration;
        private readonly IApplicationFolder _applicationFolder;
        private readonly IInputManager _inputManager;
        private readonly GraphicsDeviceManager _graphics;

        public Game1(IRenderManager renderManager, IScreenManager screenManager, IContentChest contentChest,
            IWindowConfiguration windowConfiguration, IApplicationFolder applicationFolder, IInputManager inputManager)
        {
            _instance = this;

            _renderManager = renderManager;
            _screenManager = screenManager;
            _contentChest = contentChest;
            _windowConfiguration = windowConfiguration;
            _applicationFolder = applicationFolder;
            _inputManager = inputManager;
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8
            };
            Content.RootDirectory = "Assets";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            _windowConfiguration.WindowHeight = _graphics.PreferredBackBufferHeight;
            _windowConfiguration.WindowWidth = _graphics.PreferredBackBufferWidth;

            var spriteBatch = new SpriteBatch(GraphicsDevice);

            _renderManager.SpriteBatch = spriteBatch;
            _renderManager.GraphicsDeviceManager = _graphics;

            _contentChest.ContentManager = Content;

            _applicationFolder.SetDirectoryName("OffTheLeash");
            _applicationFolder.Create();

            _screenManager.LoadScreens();
            _screenManager.SetActiveScreen(StartingScreen);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _inputManager.Update(gameTime);
            _screenManager.GetActiveScreen().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetRenderTarget(null);
            
            _screenManager.GetActiveScreen().Draw();

            base.Draw(gameTime);
        }

        public static void Quit() => _instance.Exit();
    }
}