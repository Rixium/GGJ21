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
    public class QuestHolderComponent : IComponent
    {
        public Action<Quest> QuestTaken { get; set; }
        private readonly ZoneManager _zoneManager;
        private readonly IInputManager _inputManager;
        public IEntity Entity { get; set; }

        private readonly IList<Quest> _quests = new List<Quest>();
        private QuestGiverComponent _questGiverNextTo;

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
                    _questGiverNextTo = questGiverComponent;

                    if (!questGiverComponent.HasQuestToGive())
                    {
                        continue;
                    }
                    
                    if (_inputManager.KeyPressed(Keys.E))
                    {
                        var newQuest = questGiverComponent.TakeQuest();
                        _quests.Add(newQuest);
                        QuestTaken?.Invoke(newQuest);
                    }

                    return;
                }
            }

            if (_questGiverNextTo != null)
            {
                _questGiverNextTo.Highlighted = false;
            }

            _questGiverNextTo = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_questGiverNextTo != null)
            {
                _questGiverNextTo.Highlighted = true;
            }
        }
    }

}