using System;
using System.IO;
using System.Linq;
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
        private SpriteFont _font;
        public IEntity Entity { get; set; }
        public string Name { get; set; }

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

            _font = _contentChest.Get<SpriteFont>("Fonts/DefaultFont");

            Name = selected.Split('\\').Last().Split('.').First();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (HasQuestToGive())
            {
                var fontSize = _font.MeasureString("!");
                spriteBatch.DrawString(_font, "!", Entity.Position + new Vector2(_staticDrawComponent.Image.Width / 2f -(fontSize.X / 2f), -(fontSize.Y)), Color.Yellow);
            }
        }

        public bool HasQuestToGive() => true;
    }
}