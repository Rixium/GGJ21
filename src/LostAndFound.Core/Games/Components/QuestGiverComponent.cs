using System;
using System.IO;
using System.Linq;
using LostAndFound.Core.Content;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class QuestGiverComponent : IComponent
    {
        private readonly IContentChest _contentChest;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private StaticDrawComponent _staticDrawComponent;
        private Random _random = new Random();
        private SpriteFont _font;
        private Sprite _questIcon;
        public IEntity Entity { get; set; }
        public string Name { get; set; }
        public bool Highlighted { get; set; }

        public QuestGiverComponent(IContentChest contentChest, IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _contentChest = contentChest;
            _spriteMapLoader = spriteMapLoader;
        }

        public void Start()
        {
            var possibleImages = Directory.GetFiles("Assets\\Images\\People");
            var randomPerson = _random.Next(0, possibleImages.Length);
            var selected = possibleImages[randomPerson].Replace($"Assets{Path.DirectorySeparatorChar}", "");
            _staticDrawComponent = Entity.GetComponent<StaticDrawComponent>();
            _staticDrawComponent.Image = _contentChest.Get<Texture2D>(selected);

            Entity.Position -= new Vector2(0, _staticDrawComponent.Image.Height) - new Vector2(0, 10);

            _font = _contentChest.Get<SpriteFont>("Fonts/DefaultFont");

            Name = selected.Split('\\').Last().Split('.').First();

            _questIcon = _spriteMapLoader.GetContent("Assets\\UI\\ui.json").CreateSpriteFromRegion("Quest_Alert");
            _questIcon.Origin = _questIcon.Center;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!HasQuestToGive()) return;

            var positionToDraw = Entity.Position + new Vector2(
                _staticDrawComponent.Image.Width / 2f - _questIcon.Width / 2f,
                -_questIcon.Height);
            spriteBatch.Draw(_questIcon.Texture, positionToDraw, _questIcon.Source, Color.White, 0,
                _questIcon.Origin, Highlighted ? 0.5f : 0.3f, SpriteEffects.None, 0f);
        }

        public bool HasQuestToGive() => true;
    }
}