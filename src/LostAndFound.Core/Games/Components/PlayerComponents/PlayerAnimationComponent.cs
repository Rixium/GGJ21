using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components.PlayerComponents
{

    public class AnimationComponent : Component
    {
        private AnimatorComponent _animatorComponent;
        
        public override void Start()
        {
            _animatorComponent = Entity.GetComponent<AnimatorComponent>();
        }
        
        public override void Update(GameTime gameTime)
        {
            _animatorComponent.SetAnimation("Idle");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
    
    public class PlayerAnimationComponent : Component
    {
        private PlayerControllerComponent _moveComponent;
        private AnimatorComponent _animatorComponent;
        

        public override void Start()
        {
            _moveComponent = Entity.GetComponent<PlayerControllerComponent>();
            _animatorComponent = Entity.GetComponent<AnimatorComponent>();
        }

        public override void Update(GameTime gameTime)
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

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}