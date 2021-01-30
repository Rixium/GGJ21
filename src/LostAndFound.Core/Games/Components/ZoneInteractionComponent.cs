using System;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class ZoneInteractionComponent : IComponent
    {
        private readonly IGameInstance _gameInstance;
        public IEntity Entity { get; set; }

        public ZoneInteractionComponent(IGameInstance gameInstance)
        {
            _gameInstance = gameInstance;
        }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            var entityZone = _gameInstance.ActiveZone;
            foreach (var collider in entityZone.Colliders)
            {
                if (!collider.Bounds.Contains(Entity.Position))
                {
                    continue;
                }

                var toProperty = collider.GetProperty("To");
                if (toProperty == null) continue;

                var toZoneValues = toProperty.Split(',');
                Enum.TryParse<ZoneType>(toZoneValues[0], out var zoneType);
                Enum.TryParse<Direction>(toZoneValues[1], out var entranceDirection);

                var zoneToGoTo = _gameInstance.GetZone(zoneType);

                if (zoneToGoTo != null)
                {
                    TeleportToZone(entityZone, zoneToGoTo, entranceDirection);
                }
            }
        }

        private void TeleportToZone(IZone oldZone, IZone zoneToGoTo, Direction direction)
        {
            _gameInstance.MoveEntityToZone(oldZone, zoneToGoTo, Entity);
            _gameInstance.SetActiveZone(zoneToGoTo.ZoneType);

            Entity.Position = zoneToGoTo.GetTeleportPoint(direction);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}