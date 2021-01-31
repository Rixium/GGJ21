using System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Screens
{
    public enum ScreenType
    {
        Empty,
        Splash,
        MainMenu,
        GameScreen
    }
    
    public interface IScreen
    {
        Action<ScreenType> RequestScreenChange { get; set; }

        ScreenType ScreenType { get; }
        void Load();
        void Update(GameTime gameTime);
        void Draw();
        void OnMadeActiveScreen();
    }
}