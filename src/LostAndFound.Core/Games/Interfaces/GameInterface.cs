using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Games.Models;
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
        private IList<Text> _questTexts = new List<Text>();

        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;
        private readonly IWindowConfiguration _windowConfiguration;
        private readonly IContentLoader<AsepriteSpriteMap> _asepriteSpriteMapLoader;
        private readonly IInputManager _inputManager;

        private readonly IList<IPanel> _panels = new List<IPanel>();
        private string _activePanelName = "Game";
        private IPanel ActivePanel => _panels.First(x => x.Name.Equals(_activePanelName));

        private float _uiScale = 3f;
        private GameData _gameData;

        private Image _questNotebook;

        public GameInterface(IRenderManager renderManager, IContentChest contentChest,
            IWindowConfiguration windowConfiguration, IContentLoader<AsepriteSpriteMap> asepriteSpriteMapLoader,
            IInputManager inputManager)
        {
            _renderManager = renderManager;
            _contentChest = contentChest;
            _windowConfiguration = windowConfiguration;
            _asepriteSpriteMapLoader = asepriteSpriteMapLoader;
            _inputManager = inputManager;
        }

        public void SetUp(GameData gameData)
        {
            SetupGameUi();
            SetupQuestUi();

            _gameData = gameData;

            AddQuestUi();
        }

        private void SetupQuestUi()
        {
            var asepriteSpriteMap = _asepriteSpriteMapLoader.GetContent("Assets/UI/ui.json");
            var notepadSprite = asepriteSpriteMap.CreateSpriteFromRegion("notepad");

            var questPanel = new Panel(_renderManager, "Quest");
            _questNotebook = new Image(notepadSprite, _windowConfiguration.Center,
                _uiScale, Origin.Center);
            questPanel.AddElement(_questNotebook);

            _panels.Add(questPanel);
        }

        private void SetupGameUi()
        {
            var font = _contentChest.Get<SpriteFont>("Fonts/DefaultFont");
            var gamePanel = new Panel(_renderManager, "Game");
            var panelText = new Text(font, string.Empty, Color.Black, new Vector2(10, 10), 1f, Origin.TopLeft);
            gamePanel.AddElement(panelText);
            _panels.Add(gamePanel);
        }

        private void AddQuestUi()
        {
            foreach (var quest in _gameData.QuestData)
            {
                AddNewQuestToPanel(quest);
            }
        }

        private void AddNewQuestToPanel(QuestData quest)
        {
            var lastQuestText = _questTexts.LastOrDefault();

            var font = _contentChest.Get<SpriteFont>("Fonts/DefaultFont");
            var questTextPosition = lastQuestText?.Bottom ?? new Vector2(_questNotebook.Bounds.Left + 10 * _uiScale,
                _questNotebook.Bounds.Top + 10 * _uiScale);

            var element = new Text(font, $"FIND {quest.AnimalId}", Color.Black,
                questTextPosition,
                1f, Origin.TopLeft);

            _questTexts.Add(element);

            _panels.First(x => x.Name.Equals("Quest")).AddElement(element);
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