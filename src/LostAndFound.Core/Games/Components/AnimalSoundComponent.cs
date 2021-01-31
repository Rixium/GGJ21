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
        private QuestFulfilmentComponent _questFulfilmentComponent;
        private List<string> _soundPaths = new List<string>();
        private double _lastSoundTime;
        
        Random random = new Random();

        public void Start()
        {
            _lastSoundTime = random.Next(0, 5000);
            _soundComponent = Entity.GetComponent<SoundComponent>();
            _questFulfilmentComponent = Entity.GetComponent<QuestFulfilmentComponent>();

            _animalType = _questFulfilmentComponent.Quest.AnimalType;

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
            var distance = Vector2.Distance(Entity.Position, _questFulfilmentComponent.QuestFulfiller.Position);
            var soundVolume = Map(100, 0, 0, 1, distance);
            
            if (_lastSoundTime < gameTime.TotalGameTime.TotalMilliseconds - (SoundInterval * 100))
            {
                _soundComponent.PlayRandomSoundFromList(_soundPaths, (float)soundVolume);
                _lastSoundTime = gameTime.TotalGameTime.TotalMilliseconds + random.Next(0, 4000);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
        
        double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}