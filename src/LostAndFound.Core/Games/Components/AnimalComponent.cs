using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    internal class AnimalComponent : Component
    {
        private readonly ZoneManager _zoneManager;
        private WandererComponent _wandererComponent;
        private BoxColliderComponent _boxCollider;
        private IEntity _following;

        public AnimalComponent(ZoneManager zoneManager)
        {
            _zoneManager = zoneManager;
        }

        public override void Start()
        {
            _wandererComponent = Entity.GetComponent<WandererComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            if (_boxCollider == null)
            {
                return;
            }

            var (x, y) = Vector2.Lerp(Entity.Position, _boxCollider.Bounds.Center.ToVector2(), 0.03f);

            if (Vector2.Distance(Entity.Position, _boxCollider.Bounds.Center.ToVector2()) < 10)
            {
                return;
            }

            Entity.Position = new Vector2(x, y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Follow(IEntity entity)
        {
            if (_following == entity)
            {
                return;
            }

            _following = entity;
            _wandererComponent.Active = false;
            
            _following.GetComponent<ZoneInteractionComponent>().ZoneSwitch += zone =>
            {
                Entity.Position = new Vector2(_following.Position.X, _following.Position.Y);
                _zoneManager.MoveEntityToZone(Entity.Zone, _following.Zone, Entity);
            };

            _boxCollider = entity.GetComponent<BoxColliderComponent>();
        }
    }
}