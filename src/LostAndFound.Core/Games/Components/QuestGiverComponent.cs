using System;
using System.IO;
using System.Linq;
using LostAndFound.Core.Content;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Questing;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public enum AnimalType
    {
        Cat,
        Dog
    }

    public class QuestGiverComponent : IComponent
    {
        private readonly IContentChest _contentChest;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private StaticDrawComponent _staticDrawComponent;
        private Random _random = new Random();
        private Sprite _questIcon;
        private bool _hasQuest = true;
        private Quest _givenQuest;
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
            _staticDrawComponent.Image = new Sprite(_contentChest.Get<Texture2D>(selected));

            Entity.Position -= new Vector2(0, _staticDrawComponent.Image.Height) - new Vector2(0, 10);

            Name = selected.Split('\\').Last().Split('.').First();

            _questIcon = _spriteMapLoader.GetContent("Assets\\UI\\ui.json").CreateSpriteFromRegion("Quest_Alert");
            _questIcon.Origin = _questIcon.Center;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GivenQuest())
            {
                return;
            }

            if (!HasQuestToGive()) return;

            var positionToDraw = Entity.Position + new Vector2(
                _staticDrawComponent.Image.Width / 2f,
                -_questIcon.Height);
            var scale = Highlighted ? 1.5f : 1f;
            spriteBatch.Draw(_questIcon.Texture, positionToDraw, _questIcon.Source, Color.White, 0f,
                _questIcon.Origin, scale, SpriteEffects.None, 0f);
        }

        private bool GivenQuest() => _givenQuest != null;

        public bool HasQuestToGive() => _hasQuest;

        public Quest TakeQuest()
        {
            _hasQuest = false;
            var randomAnimalType = (AnimalType) _random.Next(0, (int) AnimalType.Dog);

            _givenQuest = new Quest
            {
                HandIn = Entity,
                AnimalZone = ZoneType.Forest,
                AnimalImage = randomAnimalType + "_" + _random.Next(1, 6),
                AnimalColor = ColorRandomizer.GetRandomColor(),
                AnimalName = "Joey",
                Completed = false
            };
            return _givenQuest;
        }
    }
}