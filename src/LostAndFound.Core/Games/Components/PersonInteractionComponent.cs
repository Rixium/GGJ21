using LostAndFound.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class PersonInteractionComponent : Component
    {
        private readonly IInputManager _inputManager;
        private readonly IGameInstance _gameInstance;

        public PersonInteractionComponent(IInputManager inputManager, IGameInstance gameInstance)
        {
            _inputManager = inputManager;
            _gameInstance = gameInstance;
        }

        

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}