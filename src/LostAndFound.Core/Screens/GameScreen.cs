using System;
using LostAndFound.Core.Games;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Screens
{
    public class GameScreen : IScreen
    {
        private readonly IGameInstance _gameInstance;

        public GameScreen(IGameInstance gameInstance)
        {
            _gameInstance = gameInstance;
        }
        
        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.GameScreen;
        
        public void Load()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            _gameInstance.Update(gameTime);
        }

        public void Draw()
        {
            _gameInstance.Draw();
        }
    }
}