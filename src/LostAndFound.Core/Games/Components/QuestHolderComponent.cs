using System;
using System.Collections.Generic;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Questing;
using LostAndFound.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Games.Components
{
    public class QuestHolderComponent : Component
    {
        public Action<Quest, IEntity> QuestTaken { get; set; }
        public Action<Quest> QuestComplete { get; set; }

        private const int InteractionRange = 20;

        private readonly ZoneManager _zoneManager;
        private readonly IInputManager _inputManager;

        private QuestGiverComponent _firstActiveQuestGiver;

        private readonly IList<Quest> _quests = new List<Quest>();

        private bool _isInsideInteractionRange;

        public QuestHolderComponent(ZoneManager zoneManager, IInputManager inputManager)
        {
            _zoneManager = zoneManager;
            _inputManager = inputManager;
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in _zoneManager.ActiveZone.Entities)
            {
                _isInsideInteractionRange = (Vector2.Distance(Entity.Position, entity.Position) < InteractionRange);

                _firstActiveQuestGiver ??= entity.GetComponent<QuestGiverComponent>();

                if (_firstActiveQuestGiver == null) continue;

                CheckForQuests(entity);
            }
        }

        private void CheckForQuests(IEntity entity)
        {
            if (!_inputManager.KeyPressed(Keys.E))
            {
                return;
            }

            var questGiverComponent = entity.GetComponent<QuestGiverComponent>();

            if (questGiverComponent == null)
            {
                return;
            }

            if (!_isInsideInteractionRange) return;
            if (!questGiverComponent.HasQuestToGive()) return;

            var newQuest = questGiverComponent.TakeQuest();

            _quests.Add(newQuest);
            QuestTaken?.Invoke(newQuest, Entity);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}