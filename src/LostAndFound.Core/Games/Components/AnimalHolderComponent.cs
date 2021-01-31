using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Questing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class AnimalHolderComponent : IComponent
    {
        public IEntity Entity { get; set; }
        public Quest Quest { get; private set; }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void SetQuest(Quest quest) => Quest = quest;
    }
}