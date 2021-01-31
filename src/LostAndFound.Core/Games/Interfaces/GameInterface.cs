using System.Collections.Generic;
using System.Linq;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Questing;
using LostAndFound.Core.Games.Systems;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Input;
using LostAndFound.Core.UI;
using LostAndFound.Core.UI.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Games.Interfaces
{
    public class GameInterface
    {
        private QuestHolderComponent _questHolderComponent;

        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;
        private readonly IInputManager _inputManager;
        private readonly IWindowConfiguration _windowConfiguration;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private readonly SystemManager _systemManager;

        private readonly IList<IPanel> _panels = new List<IPanel>();
        private string _activePanelName = "Game";
        private SpriteFont _font;
        private AsepriteSpriteMap _uiSpriteMap;

        private Sprite _questPopupImage;
        private AudioSystem _audioSystem;


        private IPanel ActivePanel => _panels.First(x => x.Name.Equals(_activePanelName));

        public GameInterface(IRenderManager renderManager, IContentChest contentChest,
            IInputManager inputManager, IWindowConfiguration windowConfiguration,
            IContentLoader<AsepriteSpriteMap> spriteMapLoader, SystemManager systemManager)
        {
            _renderManager = renderManager;
            _contentChest = contentChest;
            _inputManager = inputManager;
            _windowConfiguration = windowConfiguration;
            _spriteMapLoader = spriteMapLoader;
            _systemManager = systemManager;
        }

        public void Load()
        {
            _uiSpriteMap = _spriteMapLoader.GetContent("Assets\\UI\\UI.json");
            _questPopupImage = _uiSpriteMap.CreateSpriteFromRegion("QuestAccepted_Popup");

            SetupGameUi();

            _audioSystem = _systemManager.GetSystem<AudioSystem>();
        }

        private void SetupGameUi()
        {
            _font = _contentChest.Get<SpriteFont>("Fonts/DefaultFont");
            var gamePanel = new Panel(_renderManager, "Game");
            var panelText = new Text(_font, "NO MONEY", Color.Black, new Vector2(10, 10), 1f, Origin.TopLeft);
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

        public void RegisterToEntity(Entity entity)
        {
            // Register to the quest holder component action, so we can show some UI when taking quest
            _questHolderComponent = entity.GetComponent<QuestHolderComponent>();
            _questHolderComponent.QuestTaken += ShowQuestTaken;
        }

        private void ShowQuestTaken(Quest quest)
        {
            var gamePanel = _panels.First(x => x.Name.Equals("Game"));

            var element = new Image(_questPopupImage, _windowConfiguration.Center, 1f, Origin.Center);
            element.AddEffect(new FadeOutEffect());

            _audioSystem.Play("Audio\\SoundEffects\\quest_taken");
            
            gamePanel.AddElement(element);
        }
    }
}