namespace LostAndFound.Core.UI
{
    public interface IPanel
    {
        void AddElement<T>(T element) where T : IElement;
        void Update();
        void Draw();
    }
}