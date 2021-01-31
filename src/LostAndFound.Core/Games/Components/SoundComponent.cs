using System;
using System.Collections.Generic;
using LostAndFound.Core.Content;
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

        public void PlaySoundByName(string name, float volume = 1)
        {
            _contentChest.Get<SoundEffect>(name).Play(volume, 0, 0);
        }
        
        public void PlayRandomSoundFromList(List<string> soundPaths, float volume = 1)
        {
            Random random = new Random();
            var i = random.Next(1, soundPaths.Count);
            PlaySoundByName(soundPaths[i]);
        }
    }
}