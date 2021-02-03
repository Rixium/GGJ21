using System;
using System.Collections.Generic;
using Asepreadr;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class SoundComponent : Component
    {
        private readonly IContentChest _contentChest;
        

        public SoundComponent(IContentChest contentChest)
        {
            _contentChest = contentChest;
        }
        
        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public void PlaySoundByName(string name, float volume = 1, float pan = 0)
        {
            var actualVolume = MathHelper.Clamp(volume, 0, 1);
            _contentChest.Get<SoundEffect>(name).Play(actualVolume, 0, pan);
        }
        
        public void PlayRandomSoundFromList(List<string> soundPaths, float volume = 1, float pan = 0)
        {
            Random random = new Random();
            var i = random.Next(1, soundPaths.Count);
            PlaySoundByName(soundPaths[i], volume, pan);
        }
    }
}