using System;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class PlayerAnimationComponent : IComponent
    {
        private PlayerControllerComponent _moveComponent;
        
        // public IAnimator HeadAnimator { get; set; }
        public IEntity Entity { get; set; }
        
        public void Start()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            _moveComponent = Entity.GetComponent<PlayerControllerComponent>();
            
            if (_moveComponent.XVelocity > 0)
            {
                Console.WriteLine("WALKIN RIGHT");
            } else if (_moveComponent.XVelocity < 0)
            {
                Console.WriteLine("WALKIN LEFT");
            }
            
            if (_moveComponent.YVelocity > 0)
            {
                Console.WriteLine("WALKIN DOWN");
            } else if (_moveComponent.YVelocity < 0)
            {
                Console.WriteLine("WALKIN UP");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}