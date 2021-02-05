using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    internal class AnimalComponent : Component
    {
        private WandererComponent _wandererComponent;
        private BoxColliderComponent _boxCollider;

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
            _wandererComponent.Active = false;
            _boxCollider = entity.GetComponent<BoxColliderComponent>();
        }
    }
}