using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Games.Models;
using LostAndFound.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class PersonInteractionComponent : IComponent
    {
        private readonly IInputManager _inputManager;

        public PersonInteractionComponent(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public IEntity Entity { get; set; }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Entity.GameInstance.ActiveZone.ZoneType == ZoneType.Street)
            {
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}