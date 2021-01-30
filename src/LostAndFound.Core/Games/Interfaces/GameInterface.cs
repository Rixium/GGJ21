using System;
using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Interfaces
{
    public class GameInterface
    {
        public Action<int> OnMoneyChanged { get; set; }
        public Action<QuestData> OnQuestAdded { get; set; }

        private IList<Text> _questTexts = new List<Text>();

        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;
        private readonly IWindowConfiguration _windowConfiguration;
        private Panel _panel;
        private float _uiScale = 1f;
        private GameData _gameData;

        public GameInterface(IRenderManager renderManager, IContentChest contentChest, IWindowConfiguration windowConfiguration)
        {
            _renderManager = renderManager;
            _contentChest = contentChest;
            _windowConfiguration = windowConfiguration;
        }

        public void SetUp(GameData gameData)
        {
            var font = _contentChest.Get<SpriteFont>("Fonts/DefaultFont");
            _panel = new Panel(_renderManager, "Game");
            var panelText = new Text(font, string.Empty, Color.Black, new Vector2(10, 10), _uiScale, Origin.TopRight);
            _panel.AddElement(panelText);

            OnMoneyChanged += (newValue) => { panelText.SetText($"${newValue}"); };
            OnQuestAdded += AddNewQuestToPanel;

            _gameData = gameData;

            AddQuestUi();
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
            var element = new Text(font, $"FIND {quest.AnimalData.Name}", Color.White, lastQuestText?.Bottom ?? new Vector2(_windowConfiguration.WindowWidth - 10, 10),
                _uiScale, Origin.TopRight);
            
            _questTexts.Add(element);
            
            _panel.AddElement(element);
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