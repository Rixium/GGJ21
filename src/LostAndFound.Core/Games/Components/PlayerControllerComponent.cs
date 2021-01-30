using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Entities;
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

    public class PlayerControllerComponent : IComponent
    {
        private BoxColliderComponent _boxColliderComponent;
        
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }
        
        private readonly IGameInstance _gameInstance;
        private readonly IInputManager _inputManager;
        private int _speed = 1;
        private bool _canMove = true;

        public PlayerControllerComponent(IGameInstance gameInstance, IInputManager inputManager)
        {
            _gameInstance = gameInstance;
            _inputManager = inputManager;
        }

        public IEntity Entity { get; set; }

        public void Start()
        {
            _boxColliderComponent = Entity.GetComponent<BoxColliderComponent>();
        }

        public void Update(GameTime gameTime)
        {
            PlayerMovement();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
        
        private void PlayerMovement()
        {
            if (_gameInstance.ActiveZone == null)
            {
                return;
            }
            
            var xChange = 0;
            var yChange = 0;

            if (_inputManager.KeyDown(Keys.A))
            {
                xChange = -_speed;
            }
            else if (_inputManager.KeyDown(Keys.D))
            {
                xChange = _speed;
            }

            if (_inputManager.KeyDown(Keys.W))
            {
                yChange = -_speed;
            }
            else if (_inputManager.KeyDown(Keys.S))
            {
                yChange = _speed;
            }

            if (xChange != 0 || yChange != 0)
            {
                Move(Entity, xChange, yChange);
            }

            XVelocity = xChange;
            YVelocity = yChange;
        }
        
        public void Move(IEntity entity, int xMove, int yMove)
        {
            var newPosition = entity.Position + new Vector2(xMove, yMove);

            var bounds = _boxColliderComponent.Bounds.Add(new Rectangle(xMove, 0, 0, 0));

            foreach (var collider in _gameInstance.ActiveZone.Colliders)
            {
                if (collider.GetProperty("Solid") != null)
                {
                    if (collider.Bounds.Intersects(bounds))
                    {
                        var depth = bounds.GetIntersectionDepth(collider.Bounds);
                        newPosition.X += depth.X;
                    }
                }
            }
            
            bounds = _boxColliderComponent.Bounds.Add(new Rectangle(0, yMove, 0, 0));
            foreach (var collider in _gameInstance.ActiveZone.Colliders)
            {
                if (collider.GetProperty("Solid") != null)
                {
                    if (collider.Bounds.Intersects(bounds))
                    {
                        var depth = bounds.GetIntersectionDepth(collider.Bounds);
                        newPosition.Y += depth.Y;
                    }
                }
            }

            var mapBottom = new Rectangle(0, _gameInstance.ActiveZone.Image.Height - _boxColliderComponent.Height, _gameInstance.ActiveZone.Image.Width, 100);
            if (bounds.Intersects(mapBottom))
            {
                var depth = bounds.GetIntersectionDepth(mapBottom);
                newPosition.Y += depth.Y;
            }
            
            entity.Position = newPosition;
        }
    }
}