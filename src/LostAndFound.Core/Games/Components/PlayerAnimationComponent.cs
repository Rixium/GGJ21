using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class PlayerAnimationComponent : IComponent
    {
        private PlayerControllerComponent _moveComponent;
        public IEntity Entity { get; set; }
        
        public void Start()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            _moveComponent = Entity.GetComponent<PlayerControllerComponent>();
            var animatorComponent = Entity.GetComponent<AnimatorComponent>();
            
            if (_moveComponent.XVelocity > 0)
            {
                animatorComponent.SetAnimation("Walk_Right");
            } else if (_moveComponent.XVelocity < 0)
            {
                animatorComponent.SetAnimation("Walk_Left");
            } else if (_moveComponent.YVelocity > 0)
            {
                animatorComponent.SetAnimation("Walk_Down");
            } else if (_moveComponent.YVelocity < 0)
            {
                animatorComponent.SetAnimation("Walk_Up");
            }
            else
            {
                animatorComponent.SetAnimation("Idle");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}