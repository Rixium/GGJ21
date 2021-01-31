using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class BounceComponent : IComponent
    {
        private int _startY;
        private int _bounceHeight = 3;

        private bool _bouncing = true;
        private bool _fall = false;

        private float bounceTimer;
        private float _moveDelay = 0.01f;

        public IEntity Entity { get; set; }

        public void Start()
        {
            _startY = (int) Entity.Position.Y;
        }

        public void Update(GameTime gameTime)
        {
            bounceTimer -= gameTime.AsDelta();
            
            if (_bouncing && bounceTimer <= 0)
            {
                if (Entity.Position.Y > _startY - _bounceHeight)
                {
                    Entity.Position -= new Vector2(0, 1);
                }
                else
                {
                    _bouncing = false;
                    _fall = true;
                }
                bounceTimer = _moveDelay;
            } else if (_fall && bounceTimer <= 0)
            {
                if (Entity.Position.Y < _startY)
                {
                    Entity.Position += new Vector2(0, 1);
                }
                else
                {
                    _bouncing = true;
                    _fall = false;
                }
                bounceTimer = _moveDelay;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}