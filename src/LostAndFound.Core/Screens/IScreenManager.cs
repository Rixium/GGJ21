namespace LostAndFound.Core.Screens
{
    public interface IScreenManager
    {
        void SetActiveScreen<T>() where T : IScreen;
        IScreen GetActiveScreen();
    }
}