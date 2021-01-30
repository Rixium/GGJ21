using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Content;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Input;
using LostAndFound.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Games.Interfaces
{
    public class GameInterface
    {
        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;
        private readonly IInputManager _inputManager;

        private readonly IList<IPanel> _panels = new List<IPanel>();
        private string _activePanelName = "Game";
        private IPanel ActivePanel => _panels.First(x => x.Name.Equals(_activePanelName));

        public GameInterface(IRenderManager renderManager, IContentChest contentChest,
            IInputManager inputManager)
        {
            _renderManager = renderManager;
            _contentChest = contentChest;
            _inputManager = inputManager;
        }

        public void Load()
        {
            SetupGameUi();
        }

        private void SetupGameUi()
        {
            var font = _contentChest.Get<SpriteFont>("Fonts/DefaultFont");
            var gamePanel = new Panel(_renderManager, "Game");
            var panelText = new Text(font, "NO MONEY", Color.Black, new Vector2(10, 10), 1f, Origin.TopLeft);
            gamePanel.AddElement(panelText);
            _panels.Add(gamePanel);
        }

        public void Update(GameTime gameTime)
        {
            if (_inputManager.KeyPressed(Keys.Tab))
            {
                _activePanelName = _activePanelName.Equals("Quest") ? "Game" : "Quest";
            }

            ActivePanel.Update(gameTime);
        }

        public void Draw()
        {
            ActivePanel.Draw();
        }
    }
}