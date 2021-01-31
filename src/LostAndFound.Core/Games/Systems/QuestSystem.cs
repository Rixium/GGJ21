using LostAndFound.Core.Content;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Questing;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Systems
{
    public class QuestSystem : ISystem
    {
        private readonly ZoneManager _zoneManager;
        private readonly IContentChest _contentChest;
        private readonly IContentLoader<AsepriteSpriteMap> _spriteMapLoader;
        private AsepriteSpriteMap _spriteMap;

        public QuestSystem(ZoneManager zoneManager, IContentChest contentChest,
            IContentLoader<AsepriteSpriteMap> spriteMapLoader)
        {
            _zoneManager = zoneManager;
            _contentChest = contentChest;
            _spriteMapLoader = spriteMapLoader;
        }

        public void Start()
        {
            _spriteMap = _spriteMapLoader.GetContent("Assets\\Images\\Animals\\animals.json");

            var questZone = _zoneManager.GetZone(ZoneType.Street);

            foreach (var collider in questZone.Colliders)
            {
                if (collider.GetProperty("QuestGiver") == null)
                {
                    continue;
                }

                var person = _contentChest.Get<Texture2D>("Images/People/Marge");
                var personSprite = new Sprite(person);

                var questGiver = new Entity(collider.Bounds.ToVector2());
                questGiver.AddComponent(new StaticDrawComponent
                {
                    Image = personSprite
                });

                var questGiverComponent = Program.Resolve<QuestGiverComponent>();
                questGiver.AddComponent(questGiverComponent);

                questZone.AddEntity(questGiver);
            }
        }

        public void OnQuestTaken(Quest quest)
        {
            var animalZone = _zoneManager.GetZone(quest.AnimalZone);

            var spawnZone = animalZone.GetColliderWithProperty("SpawnZone");
            var randomSpawnPosition = spawnZone.GetRandomPositionInBounds();

            var entity = new Entity(randomSpawnPosition);
            var staticDrawComponent = new StaticDrawComponent();
            var animal = _spriteMap.CreateSpriteFromRegion(quest.AnimalImage);
            animal.Color = quest.AnimalColor;

            staticDrawComponent.Image = animal;

            entity.AddComponent(staticDrawComponent);
            
            var questFulfilmentComponent = Program.Resolve<QuestFulfilmentComponent>();
            questFulfilmentComponent.Quest = quest;
            entity.AddComponent(questFulfilmentComponent);

            animalZone.AddEntity(entity);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}