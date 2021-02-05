using System;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    internal class AnimalComponent : Component
    {
        private const int FollowRadius = 10;
        private readonly ZoneManager _zoneManager;
        private WandererComponent _wandererComponent;
        private BoxColliderComponent _boxCollider;
        private IEntity _following;
        private Vector2 _randomPosition;
        private float _timer;
        private Random _random = new Random();

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

            _timer -= gameTime.AsDelta();
            
            if (_timer <= 0)
            {
                _randomPosition = _boxCollider.Bounds.Expand(FollowRadius).GetRandomPositionInBounds();
                _timer = _random.Next(1, 10) / 4f;
            }

            var (x, y) = Vector2.Lerp(Entity.Position, _randomPosition, 0.03f);

            if (Vector2.Distance(Entity.Position, _randomPosition) < 10)
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

            Entity.GetComponent<BounceComponent>().BounceSpeed = 0.3f;
            _following.GetComponent<ZoneInteractionComponent>().ZoneSwitch += zone =>
            {
                Entity.Position = new Vector2(_following.Position.X, _following.Position.Y);
                _timer = 0;
                _randomPosition = Entity.Position;
                _zoneManager.MoveEntityToZone(Entity.Zone, _following.Zone, Entity);
            };

            _boxCollider = entity.GetComponent<BoxColliderComponent>();
        }
    }
}