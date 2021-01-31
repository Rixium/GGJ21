using System.Collections.Generic;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class AnimalSoundComponent : IComponent
    {
        public IEntity Entity { get; set; }
        public AnimalType _animalType = AnimalType.Dog;
        public int SoundInterval = 2;

        private SoundComponent _soundComponent;
        private List<string> _soundPaths = new List<string>();
        private double _lastSoundTime;

        public void Start()
        {
            _soundComponent = Entity.GetComponent<SoundComponent>();

            if (_animalType == AnimalType.Dog)
            {
                _soundPaths.Add("Audio/SoundEffects/AnimalSounds/bark_1");
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_lastSoundTime < gameTime.TotalGameTime.Seconds - SoundInterval)
            {
                _soundComponent.PlayRandomSoundFromList(_soundPaths);
                _lastSoundTime = gameTime.TotalGameTime.Seconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}