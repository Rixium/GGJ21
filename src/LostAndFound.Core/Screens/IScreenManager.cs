namespace LostAndFound.Core.Screens
{
    public interface IScreenManager
    {
        void LoadScreens();
        void SetActiveScreen<T>() where T : IScreen;
        IScreen GetActiveScreen();
    }
}