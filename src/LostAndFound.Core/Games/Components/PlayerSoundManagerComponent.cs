using System.Collections.Generic;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class PlayerSoundManagerComponent : IComponent
    {
        public IEntity Entity { get; set; }

        private SoundComponent _soundComponent;
        private PlayerControllerComponent _playerControllerComponent;
        
        private double _footStepLength = 0.5;
        private List<string> _soundPaths = new List<string>();
        private bool _isMoving;
        private double _lastStepTime;

        public void Start()
        {
            _soundComponent = Entity.GetComponent<SoundComponent>();
            _playerControllerComponent = Entity.GetComponent<PlayerControllerComponent>();

            _soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_1");
            _soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_2");
            _soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_3");
            _soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_4");
            _soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_5");
            _soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_6");
            _soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_7");
            _soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_8");
        }

        public void Update(GameTime gameTime)
        {
            if ((_playerControllerComponent.XVelocity != 0 || _playerControllerComponent.YVelocity != 0) && !_isMoving)
            { 
                _isMoving = true;
                _soundComponent.PlayRandomSoundFromList(_soundPaths);
                _lastStepTime = gameTime.TotalGameTime.TotalSeconds;
            }
            else if (_playerControllerComponent.XVelocity == 0 && _playerControllerComponent.YVelocity == 0)
            {
                _isMoving = false;
            }

            if (_isMoving)
            {
                if (_lastStepTime + _footStepLength < gameTime.TotalGameTime.TotalSeconds)
                {
                    _soundComponent.PlayRandomSoundFromList(_soundPaths);
                    _lastStepTime = gameTime.TotalGameTime.TotalSeconds;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}