using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class AnimationDrawComponent : Component
    {
        private AnimatorComponent _animator;

        public override void Start()
        {
            _animator = Entity.GetComponent<AnimatorComponent>();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var frame = _animator.Current;
            if (frame == null)
            {
                return;
            }

            Entity.Bottom = (int) (Entity.Position.Y + frame.Source.Height);
            Entity.Width = frame.Source.Width;
            Entity.Height = frame.Source.Height;
            
            spriteBatch.Draw(frame.Texture, Entity.Position, frame.Source, Color.White, 0f, frame.Origin, 1f,
                SpriteEffects.None, 0f);
        }
    }
}