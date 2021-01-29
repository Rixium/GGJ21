using System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Screens
{
    public class GameScreen : IScreen
    {
        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.GameScreen;
        public void Load()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw()
        {
            
        }
    }
}