using System;
using LostAndFound.Core.Extensions;
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
            var entityCollider = Entity.GetComponent<BoxColliderComponent>();
            var entityZone = _gameInstance.ActiveZone;

            if (entityCollider.Bounds.X < 0)
            {
                var collider = entityZone.GetColliderWithProperty("Left");
                var property = collider.GetProperty("Left");
                Enum.TryParse<ZoneType>(property, out var zoneType);
                
                var zoneToGoTo = _gameInstance.GetZone(zoneType);

                if (zoneToGoTo != null)
                {
                    TeleportToZone(entityZone, zoneToGoTo);
                    Entity.Position =  new Vector2(zoneToGoTo.Image.Width, Entity.Position.Y);
                }
            } else if (entityCollider.Bounds.X > entityZone.Image.Width)
            {
                var collider = entityZone.GetColliderWithProperty("Right");
                var property = collider.GetProperty("Right");
                Enum.TryParse<ZoneType>(property, out var zoneType);
                
                var zoneToGoTo = _gameInstance.GetZone(zoneType);

                if (zoneToGoTo != null)
                {
                    TeleportToZone(entityZone, zoneToGoTo);
                    Entity.Position =  new Vector2(0, Entity.Position.Y);
                }
            }
        }

        private void TeleportToZone(IZone oldZone, IZone zoneToGoTo)
         {
            _gameInstance.MoveEntityToZone(oldZone, zoneToGoTo, Entity);
            _gameInstance.SetActiveZone(zoneToGoTo.ZoneType);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}