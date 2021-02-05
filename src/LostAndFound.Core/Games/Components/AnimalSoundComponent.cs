using System;
using System.Collections.Generic;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class AnimalSoundComponent : Component
    {
        private readonly IGameInstance _gameInstance;
        private const int SoundInterval = 2;

        private SoundComponent _soundComponent;
        private List<string> _soundPaths = new List<string>();
        private double _lastSoundTime;

        private readonly Random _random = new Random();
        private IEntity _playerEntity;

        public AnimalSoundComponent(IGameInstance gameInstance)
        {
            _gameInstance = gameInstance;
        }

        public override void Start()
        {
            _lastSoundTime = _random.Next(0, 5000);
            _soundComponent = Entity.GetComponent<SoundComponent>();

            _playerEntity = _gameInstance.GetPlayer();
        }

        public override void Update(GameTime gameTime)
        {
            var distance = Vector2.Distance(Entity.Position, _playerEntity.Position);
            var soundVolume = Map(100, 0, 0, 1, distance);

            if (_lastSoundTime < gameTime.TotalGameTime.TotalMilliseconds - (SoundInterval * 100))
            {
                _soundComponent.PlayRandomSoundFromList(_soundPaths, (float) soundVolume);
                _lastSoundTime = gameTime.TotalGameTime.TotalMilliseconds + _random.Next(0, 4000);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        private static double Map(double a1, double a2, double b1, double b2, double s) =>
            b1 + (s - a1) * (b2 - b1) / (a2 - a1);

        public void SetSounds(List<string> list) => _soundPaths = list;
    }
}