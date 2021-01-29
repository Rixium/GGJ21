using System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Screens
{
    public class MainMenuScreen : IScreen
    {
        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.MainMenu;
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