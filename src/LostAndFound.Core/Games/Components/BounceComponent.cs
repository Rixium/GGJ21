using LostAndFound.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class BounceComponent : Component
    {
        private const float MoveDelay = 0.01f;
        private const int BounceHeight = 3;

        private bool _bouncing = true;
        private bool _fall;

        private float _bounceTimer;
        private float _bounceDistance;

        public float BounceSpeed = 1f;
        public bool Active { get; set; } = true;

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active) return;
            
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

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}