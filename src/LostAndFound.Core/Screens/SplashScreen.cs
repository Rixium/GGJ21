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
        private Texture2D _image;

        public SplashScreen(IRenderManager renderManager, IContentChest contentChest)
        {
            _renderManager = renderManager;
            _contentChest = contentChest;
        }

        public void Load()
        {
            _image = _contentChest.Get<Texture2D>("images/splash");
        }

        public void Update()
        {
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Begin();
            _renderManager.SpriteBatch.Draw(_image, Vector2.Zero, Color.Wheat);
            _renderManager.SpriteBatch.End();
        }
    }
}