using System;
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

        public int InteractionRange = 20;

        private readonly ZoneManager _zoneManager;
        private readonly IInputManager _inputManager;
        private readonly IContentChest _contentChest;
        public IEntity Entity { get; set; }
        
        private MoneyBagComponent _moneyBagComponent;
        private BoxColliderComponent _boxColliderComponent;
        private AnimalHolderComponent _animalHolder;
        private SoundEffect _questSuccessSound;
        private QuestGiverComponent _firstActiveQuestGiver;
        private bool _isInsideInteractionRange;

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
                _isInsideInteractionRange = (Vector2.Distance(Entity.Position, entity.Position) < InteractionRange);

                _firstActiveQuestGiver ??= entity.GetComponent<QuestGiverComponent>();

                if (_firstActiveQuestGiver == null) continue;

                if (entity != _firstActiveQuestGiver.Entity) continue;
                
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

            if (_isInsideInteractionRange)
            {
                dialogComponent.SetNear(true);

                if (_firstActiveQuestGiver != null)
                {
                    _firstActiveQuestGiver.Highlighted = true;
                }

                return _inputManager.KeyPressed(Keys.E) && dialogComponent.Talk();
            }

            if (_firstActiveQuestGiver != null)
            {
                _firstActiveQuestGiver.Highlighted = false;
                _firstActiveQuestGiver = null;
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

            if (_isInsideInteractionRange)
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