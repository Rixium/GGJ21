using System;
using System.Collections.Generic;
using Asepreadr;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Questing;
using LostAndFound.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Games.Components
{
    public class QuestHolderComponent : IComponent
    {
        private Random _random = new Random();
        public Action<Quest, IEntity> QuestTaken { get; set; }
        public Action<Quest> QuestComplete { get; set; }

        private readonly ZoneManager _zoneManager;
        private readonly IInputManager _inputManager;
        private readonly IContentChest _contentChest;
        public IEntity Entity { get; set; }

        private List<QuestGiverComponent> _questGiversNextTo = new List<QuestGiverComponent>();
        private MoneyBagComponent _moneyBagComponent;
        private BoxColliderComponent _boxColliderComponent;
        private AnimalHolderComponent _animalHolder;
        private SoundEffect _questSuccessSound;

        public QuestHolderComponent(ZoneManager zoneManager, IInputManager inputManager, IContentChest contentChest)
        {
            _zoneManager = zoneManager;
            _inputManager = inputManager;
            _contentChest = contentChest;
        }

        public void Start()
        {
            _moneyBagComponent = Entity.GetComponent<MoneyBagComponent>();
            _boxColliderComponent = Entity.GetComponent<BoxColliderComponent>();
            _animalHolder = Entity.GetComponent<AnimalHolderComponent>();

            _questSuccessSound = _contentChest.Get<SoundEffect>("Audio\\SoundEffects\\quest_complete");
        }

        public void Update(GameTime gameTime)
        {
            foreach (var entity in _zoneManager.ActiveZone.Entities)
            {
                CheckForQuestPickup(entity);

                if (CheckForDialog(entity))
                {
                    break;
                }

                CheckForQuests(entity);
            }
        }

        private bool CheckForDialog(IEntity entity)
        {
            var dialogComponent = entity.GetComponent<DialogComponent>();

            if (dialogComponent == null)
            {
                return false;
            }

            if (Vector2.Distance(Entity.Position, dialogComponent.Entity.Position) < 20)
            {
                dialogComponent.SetNear(true);

                bool alreadyAdded = false;
                foreach (var questGiver in _questGiversNextTo)
                {
                    if (questGiver == entity.GetComponent<QuestGiverComponent>())
                    {
                        alreadyAdded = true;
                        questGiver.Highlighted = true;
                        break;
                    }
                }

                if (!alreadyAdded)
                {
                    _questGiversNextTo.Add(dialogComponent.Entity.GetComponent<QuestGiverComponent>());
                }
                
                return _inputManager.KeyPressed(Keys.E) && dialogComponent.Talk();
            }

            foreach (var questGiver in _questGiversNextTo)
            {
                if (questGiver == entity.GetComponent<QuestGiverComponent>())
                {
                    questGiver.Highlighted = false;
                    _questGiversNextTo.Remove(questGiver);
                    break;
                }
            }

            dialogComponent.SetNear(false);

            return false;
        }

        private void CheckForQuestPickup(IEntity entity)
        {
            if (!_inputManager.KeyPressed(Keys.E))
            {
                return;
            }

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

            if (!_boxColliderComponent.Bounds.Intersects(bounds))
            {
                return;
            }

            _animalHolder.SetQuest(questFulfilmentComponent.Quest);
            _zoneManager.ActiveZone.RemoveEntity(questFulfilmentComponent.Entity);
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

            if (Vector2.Distance(Entity.Position, questGiverComponent.Entity.Position) < 20)
            {
                if (questGiverComponent.HasQuestToGive())
                {
                    var dialogComponent = entity.GetComponent<DialogComponent>();

                    var newQuest = questGiverComponent.TakeQuest();

                    var text = dialogComponent.AddText(
                        $"My {newQuest.AnimalType} loves to hang around at the {newQuest.AnimalZone}.");
                    newQuest.AddDialogHistory(text);

                    text = dialogComponent.AddText($"Please find {(_random.Next(0, 2) == 1 ? "her" : "him")}.");
                    newQuest.AddDialogHistory(text);

                    text = dialogComponent.AddText($"I'll give you ${newQuest.Reward}");
                    newQuest.AddDialogHistory(text);


                    QuestTaken?.Invoke(newQuest, Entity);
                }
                else if (_animalHolder.Quest != null)
                {
                    if (questGiverComponent.QuestIs(_animalHolder.Quest))
                    {
                        FulfilQuest(_animalHolder.Quest);
                        questGiverComponent.RefreshQuest();
                    }
                }
            }
        }

        private void FulfilQuest(Quest quest)
        {
            _moneyBagComponent.AddMoney(quest.Reward);
            QuestComplete?.Invoke(quest);
            _animalHolder.RemoveQuest();
            _questSuccessSound.Play();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}