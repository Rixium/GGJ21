using System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Screens
{
    public class EmptyScreen : IScreen
    {
        public Action<ScreenType> RequestScreenChange { get; set; }
        public ScreenType ScreenType => ScreenType.Empty;
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