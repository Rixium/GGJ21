using System;
using LostAndFound.Core.Content;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Interfaces
{
    public class GameInterface
    {
        public Action<int> OnMoneyChanged { get; set; }

        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;
        private Panel _panel;
        private float _uiScale = 1f;

        public GameInterface(IRenderManager renderManager, IContentChest contentChest)
        {
            _renderManager = renderManager;
            _contentChest = contentChest;
        }

        public void SetUp()
        {
            var font = _contentChest.Get<SpriteFont>("Fonts/DefaultFont");
            _panel = new Panel(_renderManager, "Game");
            var panelText = new Text(font, string.Empty, Color.Black, new Vector2(10, 10), _uiScale, Origin.TopLeft);
            _panel.AddElement(panelText);

            OnMoneyChanged += (newValue) =>
            {
                panelText.SetText($"${newValue}");
            };
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