using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class AnimationDrawComponent : IComponent
    {
        public IEntity Entity { get; set; }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var animator = Entity.GetComponent<AnimatorComponent>();
            var frame = animator.Current;
            if (frame == null)
            {
                return;
            }

            spriteBatch.Draw(frame.Texture, Entity.Position, frame.Source, Color.White, 0f, frame.Origin, 1f,
                SpriteEffects.None, 0f);
        }
    }
}