using System;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NotImplementedException = System.NotImplementedException;

namespace LostAndFound.Core.Games.Components
{
    internal class WandererComponent : IComponent
    {
        private readonly ZoneManager _zoneManager;
        public IEntity Entity { get; set; }

        private Vector2 _wanderPosition;
        private float _randomPositionGetTimer;
        private Random _random = new Random();

        public float Speed = 0.02f;
        public string Property = "SpawnZone";
        
        public WandererComponent(ZoneManager zoneManager)
        {
            _zoneManager = zoneManager;
        }
        
        public void Start()
        {
            _wanderPosition = Entity.Position;
        }

        public void Update(GameTime gameTime)
        {
            if (Vector2.Distance(_wanderPosition, Entity.Position) < 40)
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

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}