using System;
using LostAndFound.Core.Content;
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

        private QuestGiverComponent _questGiverNextTo;
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
            if (_inputManager.KeyPressed(Keys.E))
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

            if (_questGiverNextTo != null)
            {
                _questGiverNextTo.Highlighted = false;
            }

            _questGiverNextTo = null;
        }

        private bool CheckForDialog(IEntity entity)
        {
            var dialogComponent = entity.GetComponent<DialogComponent>();

            if (dialogComponent == null)
            {
                return false;
            }

            return Vector2.Distance(Entity.Position, dialogComponent.Entity.Position) < 20 && dialogComponent.Talk();
        }

        private void CheckForQuestPickup(IEntity entity)
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

            if (!_boxColliderComponent.Bounds.Intersects(bounds))
            {
                return;
            }

            _animalHolder.SetQuest(questFulfilmentComponent.Quest);
            _zoneManager.ActiveZone.RemoveEntity(questFulfilmentComponent.Entity);
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

                if (questGiverComponent.HasQuestToGive())
                {
                    var dialogComponent = entity.GetComponent<DialogComponent>();
                    
                    var newQuest = questGiverComponent.TakeQuest();
                    
                    var text = dialogComponent.AddText($"My {newQuest.AnimalType} loves to hang around at the {newQuest.AnimalZone}.");
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
            if (_questGiverNextTo != null)
            {
                _questGiverNextTo.Highlighted = true;
            }
        }
    }
}