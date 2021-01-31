using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class AnimationDrawComponent : IComponent
    {
        private AnimatorComponent _animator;
        public IEntity Entity { get; set; }

        public void Start()
        {
            _animator = Entity.GetComponent<AnimatorComponent>();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
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