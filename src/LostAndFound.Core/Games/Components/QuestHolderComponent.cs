using System.Collections.Generic;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Questing;
using LostAndFound.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class QuestHolderComponent : IComponent
    {
        private readonly ZoneManager _zoneManager;
        private readonly IInputManager _inputManager;
        public IEntity Entity { get; set; }

        private readonly IList<Quest> _quests = new List<Quest>();

        public QuestHolderComponent(ZoneManager zoneManager, IInputManager inputManager)
        {
            _zoneManager = zoneManager;
            _inputManager = inputManager;
        }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in _zoneManager.ActiveZone.Entities)
            {
                var questGiverComponent = entity.GetComponent<QuestGiverComponent>();
                if (questGiverComponent == null)
                {
                    continue;
                }

                if (Vector2.Distance(Entity.Position, questGiverComponent.Entity.Position) < 20)
                {
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void GiveQuest(Quest quest) => _quests.Add(quest);
    }
}