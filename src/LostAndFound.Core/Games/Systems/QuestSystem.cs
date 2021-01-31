using LostAndFound.Core.Content;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Systems
{
    public class QuestSystem : ISystem
    {
        private readonly ZoneManager _zoneManager;
        private readonly IContentChest _contentChest;

        public QuestSystem(ZoneManager zoneManager, IContentChest contentChest)
        {
            _zoneManager = zoneManager;
            _contentChest = contentChest;
        }

        public void Start()
        {
            var questZone = _zoneManager.GetZone(ZoneType.Street);

            foreach (var collider in questZone.Colliders)
            {
                if (collider.GetProperty("QuestGiver") == null)
                {
                    continue;
                }

                var person = _contentChest.Get<Texture2D>("Images/People/Marge");
                var questGiver = new Entity(collider.Bounds.ToVector2());
                questGiver.AddComponent(new StaticDrawComponent
                {
                    Image = person
                });

                questGiver.AddComponent(Program.Resolve<QuestGiverComponent>());

                questZone.AddEntity(questGiver);
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}