namespace LostAndFound.Core.Screens
{
    public interface IScreenManager
    {
        void SetActiveScreen(ScreenType screenType);
        void LoadScreens();
        IScreen GetActiveScreen();
    }
}