using System;
using System.IO;
using LostAndFound.Core.Content;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class QuestGiverComponent : IComponent
    {
        private readonly IContentChest _contentChest;
        private StaticDrawComponent _staticDrawComponent;
        private Random _random = new Random();
        public IEntity Entity { get; set; }

        public QuestGiverComponent(IContentChest contentChest)
        {
            _contentChest = contentChest;
        }

        public void Start()
        {
            var possibleImages = Directory.GetFiles("Assets\\Images\\People");
            var randomPerson = _random.Next(0, possibleImages.Length);
            var selected = possibleImages[randomPerson].Replace($"Assets{Path.DirectorySeparatorChar}", "");
            _staticDrawComponent = Entity.GetComponent<StaticDrawComponent>();
            _staticDrawComponent.Image = _contentChest.Get<Texture2D>(selected);

            Entity.Position -= new Vector2(0, _staticDrawComponent.Image.Height) - new Vector2(0, 10);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (HasQuestToGive())
            {
            }
        }

        public bool HasQuestToGive() => true;
    }
}