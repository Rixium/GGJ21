using System;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Games.Components
{
    public enum Direction
    {
        Left,
        Right
    }

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

    public class PlayerControllerComponent : IComponent
    {
        private readonly IGameInstance _gameInstance;
        private readonly IInputManager _inputManager;

        public PlayerControllerComponent(IGameInstance gameInstance, IInputManager inputManager)
        {
            _gameInstance = gameInstance;
            _inputManager = inputManager;
        }

        public IEntity Entity { get; set; }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            var xChange = 0;
            var yChange = 0;

            if (_inputManager.KeyDown(Keys.A))
            {
                xChange = -1;
            }
            else if (_inputManager.KeyDown(Keys.D))
            {
                xChange = 1;
            }

            if (_inputManager.KeyDown(Keys.W))
            {
                yChange = -1;
            }
            else if (_inputManager.KeyDown(Keys.S))
            {
                yChange = 1;
            }

            Entity.Position += new Vector2(xChange, yChange);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        // public bool CanPlayerMove(Movement movement, Rectangle bounds)
        // {
        //     var newBounds = new Rectangle(bounds.X + movement.X, bounds.Y + movement.Y, bounds.Width, bounds.Height);
        //     return _gameInstance.ActiveZone.Colliders.Any(x => x.Bounds.Intersects(newBounds));
        // }
    }
}