﻿using System.Collections.Generic;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Questing;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class AnimalHolderComponent : IComponent
    {
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private AsepriteSpriteMap _spriteMap;
        private Dictionary<string, Sprite> _animalSprites = new Dictionary<string, Sprite>();

        public IEntity Entity { get; set; }
        public Quest Quest { get; private set; }

        public AnimalHolderComponent(IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _spriteMapLoader = spriteMapLoader;
        }

        public void Start()
        {
            _animalSprites = new Dictionary<string, Sprite>();
            _spriteMap = _spriteMapLoader.GetContent("Assets//Images//Animals//animals.json");
            _animalSprites.Add("Dog_1", _spriteMap.CreateSpriteFromRegion("Dog_1"));
            _animalSprites.Add("Dog_2", _spriteMap.CreateSpriteFromRegion("Dog_2"));
            _animalSprites.Add("Dog_3", _spriteMap.CreateSpriteFromRegion("Dog_3"));
            _animalSprites.Add("Dog_4", _spriteMap.CreateSpriteFromRegion("Dog_4"));
            _animalSprites.Add("Dog_5", _spriteMap.CreateSpriteFromRegion("Dog_5"));
            _animalSprites.Add("Dog_6", _spriteMap.CreateSpriteFromRegion("Dog_6"));
            _animalSprites.Add("Cat_1", _spriteMap.CreateSpriteFromRegion("Cat_1"));
            _animalSprites.Add("Cat_2", _spriteMap.CreateSpriteFromRegion("Cat_2"));
            _animalSprites.Add("Cat_3", _spriteMap.CreateSpriteFromRegion("Cat_3"));
            _animalSprites.Add("Cat_4", _spriteMap.CreateSpriteFromRegion("Cat_4"));
            _animalSprites.Add("Cat_5", _spriteMap.CreateSpriteFromRegion("Cat_5"));
            _animalSprites.Add("Cat_6", _spriteMap.CreateSpriteFromRegion("Cat_6"));
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Quest == null)
            {
                return;
            }

            var animalImage = Quest.AnimalImage;
            var sprite = _animalSprites[animalImage];

            spriteBatch.Draw(sprite.Texture, Entity.Position, sprite.Source, Quest.AnimalColor);
        }

        public void SetQuest(Quest quest) => Quest = quest;

        public void RemoveQuest() => Quest = null;
    }
}