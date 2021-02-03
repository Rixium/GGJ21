using System;
using LostAndFound.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    internal class WandererComponent : Component
    {
        private readonly ZoneManager _zoneManager;
        
        public bool Active { get; set; } = true;

        private Vector2 _wanderPosition;
        private float _randomPositionGetTimer;
        private Random _random = new Random();

        public float Speed = 0.02f;
        public string Property = "SpawnZone";
        
        public WandererComponent(ZoneManager zoneManager)
        {
            _zoneManager = zoneManager;
        }
        
        public override void Start()
        {
            _wanderPosition = Entity.Position;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active) return;
            
            if (Vector2.Distance(_wanderPosition, Entity.Position) < 100)
            {
                _randomPositionGetTimer -= gameTime.AsDelta();
                if (_randomPositionGetTimer < 0)
                {
                    _wanderPosition = GetNewWanderPosition();
                    _randomPositionGetTimer = _random.Next(1, 10) / 4f;
                }
            }
            
            var (x, y) = Vector2.Lerp(Entity.Position, _wanderPosition, Speed);
            Entity.Position = new Vector2(x, y);
        }

        private Vector2 GetNewWanderPosition()
        {
            var spawnZone = _zoneManager.ActiveZone.GetColliderWithProperty(Property);
            var randomPosition = spawnZone.GetRandomPositionInBounds();
            return randomPosition;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}