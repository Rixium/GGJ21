using System.Collections.Generic;
using LostAndFound.Core.Games.Questing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Systems
{
    public class QuestSystem : ISystem
    {
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

        public IList<Quest> GetQuests() => _quests;

        public void AddQuest(Quest quest) => _quests.Add(quest);
    }
}