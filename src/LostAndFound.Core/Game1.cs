using LostAndFound.Core.Content;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core
{
    internal class Game1 : Game
    {
        private readonly IRenderManager _renderManager;
        private readonly IScreenManager _screenManager;
        private readonly IContentChest _contentChest;
        private GraphicsDeviceManager _graphics;

        public Game1(IRenderManager renderManager, IScreenManager screenManager, IContentChest contentChest)
        {
            _renderManager = renderManager;
            _screenManager = screenManager;
            _contentChest = contentChest;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Assets";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            var spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // We're going to store the spritebatch we make inside this,
            // so we can make use of it elsewhere, rather than pass it in.
            _renderManager.SpriteBatch = spriteBatch;

            _contentChest.ContentManager = Content;
            
            _screenManager.LoadScreens();
            _screenManager.SetActiveScreen<SplashScreen>();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _screenManager.GetActiveScreen().Draw();

            base.Draw(gameTime);
        }
    }
}
