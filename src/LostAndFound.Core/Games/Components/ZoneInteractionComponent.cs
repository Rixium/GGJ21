using System;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class ZoneInteractionComponent : IComponent
    {
        private readonly ZoneManager _zoneManager;
        private BoxColliderComponent _entityCollider;
        public IEntity Entity { get; set; }

        public ZoneInteractionComponent(ZoneManager zoneManager)
        {
            _zoneManager = zoneManager;
        }

        public void Start()
        {
            _entityCollider = Entity.GetComponent<BoxColliderComponent>();
        }

        public void Update(GameTime gameTime)
        {
            var entityZone = _zoneManager.ActiveZone;

            if (_entityCollider.Bounds.X < 0)
            {
                var collider = entityZone.GetColliderWithProperty("Left");
                if (collider == null)
                {
                    return;
                }

                var property = collider.GetProperty("Left");
                Enum.TryParse<ZoneType>(property, out var zoneType);

                var zoneToGoTo = _zoneManager.GetZone(zoneType);

                if (zoneToGoTo != null)
                {
                    TeleportToZone(entityZone, zoneToGoTo);
                    var startYCollider = zoneToGoTo.GetColliderWithProperty("StartY");
                    Entity.Position = new Vector2(zoneToGoTo.Image.Width, startYCollider.Bounds.Y);
                }
            }
            else if (_entityCollider.Bounds.X > entityZone.Image.Width)
            {
                var collider = entityZone.GetColliderWithProperty("Right");
                if (collider == null)
                {
                    return;
                }

                var property = collider.GetProperty("Right");
                Enum.TryParse<ZoneType>(property, out var zoneType);

                var zoneToGoTo = _zoneManager.GetZone(zoneType);

                if (zoneToGoTo != null)
                {
                    TeleportToZone(entityZone, zoneToGoTo);
                    var startYCollider = zoneToGoTo.GetColliderWithProperty("StartY");
                    Entity.Position = new Vector2(0, startYCollider.Bounds.Y);
                }
            }
        }

        private void TeleportToZone(IZone oldZone, IZone zoneToGoTo)
        {
            _zoneManager.MoveEntityToZone(oldZone, zoneToGoTo, Entity);
            _zoneManager.SetActiveZone(zoneToGoTo.ZoneType);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}