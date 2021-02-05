using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Asepreadr;
using Asepreadr.Aseprite;
using Asepreadr.Graphics;
using Asepreadr.Loaders;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Questing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Systems
{
    public class QuestSystem : ISystem
    {
        private readonly Random _random = new Random();
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
            var possibleImages = Directory.GetFiles("Assets\\Images\\People").ToList();

            foreach (var collider in questZone.Colliders)
            {
                if (collider.GetProperty("QuestGiver") == null)
                {
                    continue;
                }

                var randomPerson = _random.Next(0, possibleImages.Count);
                var selected = possibleImages[randomPerson].Replace($"Assets{Path.DirectorySeparatorChar}", "");
                possibleImages.RemoveAt(randomPerson);

                var person = _contentChest.Get<Texture2D>(selected);
                var personSprite = new Sprite(person);

                var questGiver = new Entity(collider.Bounds.ToVector2());
                questGiver.AddComponent(new StaticDrawComponent
                {
                    Image = personSprite
                });

                var questGiverComponent = Program.Resolve<QuestGiverComponent>();
                questGiverComponent.Name = selected.Split('\\').Last().Split('.').First();


                questGiver.AddComponent(questGiverComponent);

                questGiver.AddComponent(Program.Resolve<DialogComponent>());
                var wandererComponent = Program.Resolve<WandererComponent>();
                wandererComponent.Property = "SafeZone";
                wandererComponent.Speed = 0.001f;

                questGiver.AddComponent(wandererComponent);
                var bounceComponent = Program.Resolve<BounceComponent>();
                bounceComponent.BounceSpeed = 0.3f;
                questGiver.AddComponent(bounceComponent);
                questZone.AddEntity(questGiver);
            }
        }

        public void OnQuestTaken(Quest quest, IEntity taker)
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

            entity.AddComponent(Program.Resolve<ZoneInteractionComponent>());
            entity.AddComponent(Program.Resolve<BoxColliderComponent>());
            var animalSoundComponent = Program.Resolve<AnimalSoundComponent>();
            animalSoundComponent.SetSounds(
                (quest.AnimalType == AnimalType.Dog
                    ? new List<string>()
                    {
                        "Audio/SoundEffects/AnimalSounds/bark_1",
                        "Audio/SoundEffects/AnimalSounds/bark_2",
                        "Audio/SoundEffects/AnimalSounds/bark_3"
                    }
                    : new List<string>()
                    {
                        "Audio/SoundEffects/AnimalSounds/meow_1",
                        "Audio/SoundEffects/AnimalSounds/meow_2",
                        "Audio/SoundEffects/AnimalSounds/meow_3",
                        "Audio/SoundEffects/AnimalSounds/meow_4",
                        "Audio/SoundEffects/AnimalSounds/meow_5",
                        "Audio/SoundEffects/AnimalSounds/meow_6",
                        "Audio/SoundEffects/AnimalSounds/meow_7",
                        "Audio/SoundEffects/AnimalSounds/meow_8"
                    })
            );
            entity.AddComponent(animalSoundComponent);
            entity.AddComponent(Program.Resolve<SoundComponent>());
            entity.AddComponent(Program.Resolve<BounceComponent>());
            entity.AddComponent(Program.Resolve<WandererComponent>());

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