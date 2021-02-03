using System.Linq;
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

    public class PlayerControllerComponent : Component
    {
        private const int Speed = 1;

        private readonly ZoneManager _zoneManager;
        private readonly IInputManager _inputManager;

        private BoxColliderComponent _boxColliderComponent;
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }


        public PlayerControllerComponent(ZoneManager zoneManager, IInputManager inputManager)
        {
            _zoneManager = zoneManager;
            _inputManager = inputManager;
        }


        public override void Start()
        {
            _boxColliderComponent = Entity.GetComponent<BoxColliderComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            PlayerMovement();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        private void PlayerMovement()
        {
            if (_zoneManager.ActiveZone == null)
            {
                return;
            }

            var xChange = 0;
            var yChange = 0;

            if (_inputManager.KeyDown(Keys.A))
            {
                xChange = -Speed;
            }
            else if (_inputManager.KeyDown(Keys.D))
            {
                xChange = Speed;
            }

            if (_inputManager.KeyDown(Keys.W))
            {
                yChange = -Speed;
            }
            else if (_inputManager.KeyDown(Keys.S))
            {
                yChange = Speed;
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

            var allColliders = _zoneManager.ActiveZone.Colliders.Where(x => x.GetProperty("Solid") != null)
                .Select(x => x.Bounds).ToList();

            foreach (var collider in allColliders)
            {
                if (collider.Intersects(bounds))
                {
                    var depth = bounds.GetIntersectionDepth(collider);
                    newPosition.X += depth.X;
                }
            }

            bounds = _boxColliderComponent.Bounds.Add(new Rectangle(0, yMove, 0, 0));
            foreach (var collider in allColliders)
            {
                if (collider.Intersects(bounds))
                {
                    var depth = bounds.GetIntersectionDepth(collider);
                    newPosition.Y += depth.Y;
                }
            }

            var mapBottom = new Rectangle(0, _zoneManager.ActiveZone.Image.Height - _boxColliderComponent.Height,
                _zoneManager.ActiveZone.Image.Width, 100);
            if (bounds.Intersects(mapBottom))
            {
                var depth = bounds.GetIntersectionDepth(mapBottom);
                newPosition.Y += depth.Y;
            }

            entity.Position = newPosition;
        }
    }
}