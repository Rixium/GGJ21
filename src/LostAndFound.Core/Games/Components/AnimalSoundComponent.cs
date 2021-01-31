using System;
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

            _animalType = Entity.GetComponent<QuestFulfilmentComponent>().Quest.AnimalType;

            switch (_animalType)
            {
                case AnimalType.Cat:
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/meow_1");
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/meow_2");
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/meow_3");
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/meow_4");
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/meow_5");
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/meow_6");
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/meow_7");
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/meow_8");
                    break;
                case AnimalType.Dog:
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/bark_1");
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/bark_2");
                    _soundPaths.Add("Audio/SoundEffects/AnimalSounds/bark_3");
                    break;
                case AnimalType.END:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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