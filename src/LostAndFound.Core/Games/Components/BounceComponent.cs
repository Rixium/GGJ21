using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class BounceComponent : IComponent
    {
        private const float MoveDelay = 0.01f;
        private const int BounceHeight = 3;

        private bool _bouncing = true;
        private bool _fall;

        private float _bounceTimer;
        private float _bounceDistance;

        public float BounceSpeed = 1f;

        public IEntity Entity { get; set; }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            _bounceTimer -= gameTime.AsDelta();

            if (_bouncing && _bounceTimer <= 0)
            {
                if (_bounceDistance > 0)
                {
                    Entity.Position -= new Vector2(0, BounceSpeed);
                    _bounceDistance -= BounceSpeed;
                }
                else
                {
                    _bouncing = false;
                    _fall = true;
                }

                _bounceTimer = MoveDelay;
            }
            else if (_fall && _bounceTimer <= 0)
            {
                if (_bounceDistance < BounceHeight)
                {
                    Entity.Position += new Vector2(0, BounceSpeed);
                    _bounceDistance += BounceSpeed;
                }
                else
                {
                    _bouncing = true;
                    _fall = false;
                }

                _bounceTimer = MoveDelay;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}