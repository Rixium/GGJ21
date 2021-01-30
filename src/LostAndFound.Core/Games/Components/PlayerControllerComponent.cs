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
        private readonly IGameInstance _gameInstance;
        private readonly IInputManager _inputManager;
        private int _speed = 2;

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