using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class PlayerAnimationComponent : IComponent
    {
        private PlayerControllerComponent _moveComponent;
        private AnimatorComponent _animatorComponent;
        public IEntity Entity { get; set; }

        public void Start()
        {
            _moveComponent = Entity.GetComponent<PlayerControllerComponent>();
            _animatorComponent = Entity.GetComponent<AnimatorComponent>();
        }

        public void Update(GameTime gameTime)
        {
            if (_moveComponent.XVelocity > 0)
            {
                _animatorComponent.SetAnimation("Walk_Right");
            }
            else if (_moveComponent.XVelocity < 0)
            {
                _animatorComponent.SetAnimation("Walk_Left");
            }
            else if (_moveComponent.YVelocity > 0)
            {
                _animatorComponent.SetAnimation("Walk_Down");
            }
            else if (_moveComponent.YVelocity < 0)
            {
                _animatorComponent.SetAnimation("Walk_Up");
            }
            else
            {
                _animatorComponent.SetAnimation("Idle");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}