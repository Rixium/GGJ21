using LostAndFound.Core.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Systems
{
    public class AudioSystem : ISystem
    {
        private readonly IContentChest _contentChest;

        public AudioSystem(IContentChest contentChest)
        {
            _contentChest = contentChest;
        }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Play(string soundEffect) => _contentChest.Get<SoundEffect>(soundEffect).Play();
    }
}