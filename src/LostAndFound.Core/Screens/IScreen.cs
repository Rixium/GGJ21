using System;

namespace LostAndFound.Core.Screens
{
    public enum ScreenType
    {
        Empty,
        Splash
    }
    
    public interface IScreen
    {
        Action<ScreenType> RequestScreenChange { get; set; }

        ScreenType ScreenType { get; }
        void Load();
        void Update();
        void Draw();
    }
}