﻿using System;
using System.Collections.Generic;
using LostAndFound.Core.Games.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class PlayerSoundManagerComponent : IComponent
    {
        public IEntity Entity { get; set; }

        private SoundComponent _soundComponent;
        private PlayerControllerComponent _playerControllerComponent;
        
        private double _footStepLength = 0.5;
        List<string> soundPaths = new List<string>();
        private bool _isMoving;
        private double _lastStepTime;

        public void Start()
        {
            _soundComponent = Entity.GetComponent<SoundComponent>();
            _playerControllerComponent = Entity.GetComponent<PlayerControllerComponent>();

            soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_1");
            soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_2");
            soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_3");
            soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_4");
            soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_5");
            soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_6");
            soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_7");
            soundPaths.Add("Audio/SoundEffects/Footsteps/footstep_8");
        }

        public void Update(GameTime gameTime)
        {
            var playerVelocity = _playerControllerComponent.XVelocity + _playerControllerComponent.YVelocity;
            
            if (playerVelocity != 0 && !_isMoving)
            { 
                _isMoving = true;
                PlayRandomFootstep();
                _lastStepTime = gameTime.TotalGameTime.TotalSeconds;
            }
            else if (playerVelocity == 0)
            {
                _isMoving = false;
            }

            if (_isMoving)
            {
                if (_lastStepTime + _footStepLength < gameTime.TotalGameTime.TotalSeconds)
                {
                    PlayRandomFootstep();
                    _lastStepTime = gameTime.TotalGameTime.TotalSeconds;
                }
            }

            // if ((lastFootstepPos - Entity.Position).Length() > footStepLenght)
            // {
            //     lastFootstepPos = Entity.Position;
            //     PlayRandomFootstep();
            // }
        }

        private void PlayRandomFootstep()
        {
            Random random = new Random();
            var i = random.Next(0, 7);
            _soundComponent.PlaySoundByName(soundPaths[i]);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}