using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class PersonInteractionComponent : IComponent
    {
        private readonly IInputManager _inputManager;
        private readonly IGameInstance _gameInstance;

        public PersonInteractionComponent(IInputManager inputManager, IGameInstance gameInstance)
        {
            _inputManager = inputManager;
            _gameInstance = gameInstance;
        }

        public IEntity Entity { get; set; }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}