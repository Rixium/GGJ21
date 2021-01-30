using System;
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
        
        private Vector2 lastFootstepPos;
        private int footStepLenght = 17;
        
        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            _soundComponent = Entity.GetComponent<SoundComponent>();
            _playerControllerComponent = Entity.GetComponent<PlayerControllerComponent>();

            if ((lastFootstepPos - Entity.Position).Length() > footStepLenght)
            {
                lastFootstepPos = Entity.Position;
                _soundComponent.PlaySoundByName("Audio/SoundEffects/Footstep");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}