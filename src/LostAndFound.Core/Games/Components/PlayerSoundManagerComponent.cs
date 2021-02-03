using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class PlayerSoundManagerComponent : Component
    {
        

        private SoundComponent _soundComponent;
        private PlayerControllerComponent _playerControllerComponent;
        
        private double _footStepLength = 0.6;
        private List<string> _soundPaths = new List<string>();
        private bool _isMoving;
        private double _lastStepTime;

        public override void Start()
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

        public override void Update(GameTime gameTime)
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}