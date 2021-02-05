using System.Collections.Generic;
using Asepreadr.Aseprite;
using Asepreadr.Graphics;
using Asepreadr.Loaders;
using LostAndFound.Core.Games.Questing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components.PlayerComponents
{
    public class AnimalHolderComponent : Component
    {
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private AsepriteSpriteMap _spriteMap;
        private Dictionary<string, Sprite> _animalSprites = new Dictionary<string, Sprite>();
        public Quest Quest { get; private set; }

        public AnimalHolderComponent(IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _spriteMapLoader = spriteMapLoader;
        }

        public override void Start()
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

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Quest == null)
            {
                return;
            }

            var animalImage = Quest.AnimalImage;
            var sprite = _animalSprites[animalImage];

            spriteBatch.Draw(sprite.Texture, Entity.Position + new Vector2(Entity.Width / 2f - sprite.Width / 2f, 0), sprite.Source, Quest.AnimalColor);
        }

        public void SetQuest(Quest quest) => Quest = quest;

        public void RemoveQuest() => Quest = null;
    }
}