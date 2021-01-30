using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class SoundComponent : IComponent
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
        }

        public void PlaySound(SoundEffect sound, float volume)
        {
            sound.Play(volume, 1, 1);
        }
    }
}