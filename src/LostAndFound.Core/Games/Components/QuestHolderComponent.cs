﻿using System;
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
        public Action<Quest> QuestComplete { get; set; }

        private readonly ZoneManager _zoneManager;
        private readonly IInputManager _inputManager;
        public IEntity Entity { get; set; }

        private readonly IList<Quest> _quests = new List<Quest>();
        private QuestGiverComponent _questGiverNextTo;
        private MoneyBagComponent _moneyBagComponent;
        private BoxColliderComponent _boxColliderComponent;
        private AnimalHolderComponent _animalHolder;

        public QuestHolderComponent(ZoneManager zoneManager, IInputManager inputManager)
        {
            _zoneManager = zoneManager;
            _inputManager = inputManager;
        }

        public void Start()
        {
            _moneyBagComponent = Entity.GetComponent<MoneyBagComponent>();
            _boxColliderComponent = Entity.GetComponent<BoxColliderComponent>();
            _animalHolder = Entity.GetComponent<AnimalHolderComponent>();
        }

        public void Update(GameTime gameTime)
        {
            if (_inputManager.KeyPressed(Keys.E))
            {
                foreach (var entity in _zoneManager.ActiveZone.Entities)
                {
                    CheckForQuests(entity);
                    CheckForQuestFulfilment(entity);
                }
            }

            if (_questGiverNextTo != null)
            {
                _questGiverNextTo.Highlighted = false;
            }

            _questGiverNextTo = null;
        }

        private void CheckForQuestFulfilment(IEntity entity)
        {
            if (_animalHolder.Quest != null)
            {
                return;
            }

            var questFulfilmentComponent = entity.GetComponent<QuestFulfilmentComponent>();
            var bounds = new Rectangle((int) entity.Position.X, (int) entity.Position.Y, entity.Width, entity.Height);

            if (questFulfilmentComponent == null)
            {
                return;
            }

            if (questFulfilmentComponent.Quest.Completed)
            {
                return;
            }

            if (!_boxColliderComponent.Bounds.Intersects(bounds))
            {
                return;
            }

            questFulfilmentComponent.Quest.Completed = true;
            _moneyBagComponent.AddMoney(questFulfilmentComponent.Quest.Reward);

            _animalHolder.SetQuest(questFulfilmentComponent.Quest);
        }

        private void CheckForQuests(IEntity entity)
        {
            var questGiverComponent = entity.GetComponent<QuestGiverComponent>();
            if (questGiverComponent == null)
            {
                return;
            }

            if (Vector2.Distance(Entity.Position, questGiverComponent.Entity.Position) < 20)
            {
                _questGiverNextTo = questGiverComponent;

                if (!questGiverComponent.HasQuestToGive())
                {
                    return;
                }

                var newQuest = questGiverComponent.TakeQuest();
                _quests.Add(newQuest);
                QuestTaken?.Invoke(newQuest);
            }
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