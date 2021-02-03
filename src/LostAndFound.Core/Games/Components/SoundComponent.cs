using System;
using System.Collections.Generic;
using Asepreadr;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class SoundComponent : IComponent
    {
        private readonly IContentChest _contentChest;
        public IEntity Entity { get; set; }

        public SoundComponent(IContentChest contentChest)
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