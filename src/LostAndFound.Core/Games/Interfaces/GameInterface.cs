using LostAndFound.Core.Content;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Interfaces
{
    public class GameInterface
    {
        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;
        private Panel _panel;

        public GameInterface(IRenderManager renderManager, IContentChest contentChest)
        {
            _renderManager = renderManager;
            _contentChest = contentChest;
        }

        public void SetUp()
        {
            var font = _contentChest.Get<SpriteFont>("Fonts/DefaultFont");
            _panel = new Panel(_renderManager, "Game");
            var panelText = new Text(font, "Hello, World!", Color.Black, new Vector2(10, 10), 3f, Origin.TopLeft);
            _panel.AddElement(panelText);
        }

        public void Update(GameTime gameTime)
        {
            _panel.Update(gameTime);
        }

        public void Draw()
        {
            _panel.Draw();
        }
    }
}