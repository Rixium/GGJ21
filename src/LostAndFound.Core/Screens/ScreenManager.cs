using System.Collections.Generic;

namespace LostAndFound.Core.Screens
{
    class ScreenManager : IScreenManager
    {
        private readonly IReadOnlyCollection<IScreen> _screens;

        public ScreenManager(IReadOnlyCollection<IScreen> screens)
        {
            _screens = screens;
        }
        
        public void SetActiveScreen<T>() where T : IScreen
        {
            
        }

        public IScreen GetActiveScreen()
        {
            return null;
        }
    }
}