using System.Collections.Generic;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Questing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class QuestHolderComponent : IComponent
    {
        public IEntity Entity { get; set; }

        private readonly IList<Quest> _quests = new List<Quest>();

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void GiveQuest(Quest quest) => _quests.Add(quest);
    }
}