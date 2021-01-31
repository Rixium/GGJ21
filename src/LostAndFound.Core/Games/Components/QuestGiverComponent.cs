using System;
using System.Collections.Generic;
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
        Dog,
        END
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

        private Dictionary<string, Sprite> _animalSprites = new Dictionary<string, Sprite>();
        private Sprite _animalScroll;

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

            var asepriteSpriteMap = _spriteMapLoader.GetContent("Assets\\UI\\ui.json");
            _questIcon = asepriteSpriteMap.CreateSpriteFromRegion("Quest_Alert");
            _questIcon.Origin = _questIcon.Center;

            _animalScroll = asepriteSpriteMap.CreateSpriteFromRegion("Animal_Scroll");
            _animalScroll.Origin = _animalScroll.Center;

            var animalSpriteMap = _spriteMapLoader.GetContent("Assets\\Images\\Animals\\animals.json");

            _animalSprites.Add("Dog_1", animalSpriteMap.CreateSpriteFromRegion("Dog_1"));
            _animalSprites.Add("Dog_2", animalSpriteMap.CreateSpriteFromRegion("Dog_2"));
            _animalSprites.Add("Dog_3", animalSpriteMap.CreateSpriteFromRegion("Dog_3"));
            _animalSprites.Add("Dog_4", animalSpriteMap.CreateSpriteFromRegion("Dog_4"));
            _animalSprites.Add("Dog_5", animalSpriteMap.CreateSpriteFromRegion("Dog_5"));
            _animalSprites.Add("Dog_6", animalSpriteMap.CreateSpriteFromRegion("Dog_6"));
            _animalSprites.Add("Cat_1", animalSpriteMap.CreateSpriteFromRegion("Cat_1"));
            _animalSprites.Add("Cat_2", animalSpriteMap.CreateSpriteFromRegion("Cat_2"));
            _animalSprites.Add("Cat_3", animalSpriteMap.CreateSpriteFromRegion("Cat_3"));
            _animalSprites.Add("Cat_4", animalSpriteMap.CreateSpriteFromRegion("Cat_4"));
            _animalSprites.Add("Cat_5", animalSpriteMap.CreateSpriteFromRegion("Cat_5"));
            _animalSprites.Add("Cat_6", animalSpriteMap.CreateSpriteFromRegion("Cat_6"));
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GivenQuest())
            {
                DrawAnimalAboveHead(spriteBatch);
                return;
            }

            if (!HasQuestToGive()) return;

            DrawQuestIconAboveHead(spriteBatch);
        }

        private void DrawQuestIconAboveHead(SpriteBatch spriteBatch)
        {
            var positionToDraw = Entity.Position + new Vector2(
                _staticDrawComponent.Image.Width / 2f,
                -_questIcon.Height);
            var scale = Highlighted ? 1.5f : 1f;
            spriteBatch.Draw(_questIcon.Texture, positionToDraw, _questIcon.Source, Color.White, 0f,
                _questIcon.Origin, scale, SpriteEffects.None, 0f);
        }

        private void DrawAnimalAboveHead(SpriteBatch spriteBatch)
        {
            var animal = _animalSprites[_givenQuest.AnimalImage];

            var animalScrollPosition = Entity.Position + new Vector2(_staticDrawComponent.Image.Width / 2f, 0);
            animalScrollPosition -= new Vector2(0, _animalScroll.Height) - new Vector2(0, 10);

            animal.Origin = animal.Center;

            spriteBatch.Draw(_animalScroll.Texture, animalScrollPosition, _animalScroll.Source, Color.White, 0f,
                _animalScroll.Origin, 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(animal.Texture,
                animalScrollPosition, animal.Source, _givenQuest.AnimalColor, 0f,
                animal.Origin, 1f, SpriteEffects.None, 0f);
        }

        private bool GivenQuest() => _givenQuest != null;

        public bool HasQuestToGive() => _hasQuest;

        public Quest TakeQuest()
        {
            _hasQuest = false;
            var randomAnimalType = (AnimalType) _random.Next(0, (int) AnimalType.END);

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