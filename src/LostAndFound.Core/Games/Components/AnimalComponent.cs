using System;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
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
        private readonly Random _random = new Random();
        public IEntity Owner;

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
            CheckForOwner();

            if (_following == null)
            {
                return;
            }

            _timer -= gameTime.AsDelta();

            if (_timer <= 0)
            {
                if (_boxCollider != null)
                {
                    _randomPosition = _boxCollider.Bounds.Expand(FollowRadius).GetRandomPositionInBounds();
                }
                else
                {
                    _randomPosition = Owner.Position;
                }

                _timer = _random.Next(1, 10) / 4f;
            }

            var (x, y) = Vector2.Lerp(Entity.Position, _randomPosition, 0.03f);

            if (Vector2.Distance(Entity.Position, _randomPosition) < 10)
            {
                return;
            }

            Entity.Position = new Vector2(x, y);
        }

        private void CheckForOwner()
        {
            foreach (var entity in _zoneManager.ActiveZone.Entities)
            {
                if (entity == Entity)
                {
                    continue;
                }

                if (entity != Owner)
                {
                    continue;
                }

                Follow(Owner);

                var isInsideInteractionRange = (Vector2.Distance(Entity.Position, entity.Position) < InteractionRange);

                if (!isInsideInteractionRange)
                {
                    continue;
                }

                var questGiverComponent = Owner.GetComponent<QuestGiverComponent>();
                questGiverComponent.CompleteQuest(Entity);
            }
        }

        public double InteractionRange { get; set; } = 30;

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Follow(IEntity entity)
        {
            if (_following == Owner)
            {
                return;
            }

            if (_following == entity)
            {
                return;
            }

            _boxCollider = null;
            _following = entity;
            _wandererComponent.Active = false;

            Entity.GetComponent<BounceComponent>().BounceSpeed = 0.3f;
            var zoneInteractionComponent = _following.GetComponent<ZoneInteractionComponent>();

            if (zoneInteractionComponent != null)
            {
                zoneInteractionComponent.ZoneSwitch += OnZoneSwitch;
                _boxCollider = entity.GetComponent<BoxColliderComponent>();
            }
        }

        private void OnZoneSwitch(ZoneType obj)
        {
            if (Entity.Destroyed)
            {
                var zoneInteractionComponent = _following.GetComponent<ZoneInteractionComponent>();

                if (zoneInteractionComponent != null)
                {
                    zoneInteractionComponent.ZoneSwitch -= OnZoneSwitch;
                }

                return;
            }

            Entity.Position = new Vector2(_following.Position.X, _following.Position.Y);
            _timer = 0;
            _randomPosition = Entity.Position;
            _zoneManager.MoveEntityToZone(Entity.Zone, _following.Zone, Entity);
        }
    }
}