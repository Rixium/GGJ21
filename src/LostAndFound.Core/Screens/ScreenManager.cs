using System.Collections.Generic;

namespace LostAndFound.Core.Screens
{
    class ScreenManager : IScreenManager
    {
        private readonly IReadOnlyCollection<IScreen> _screens;
        private IScreen _activeScreen;

        public ScreenManager(IReadOnlyCollection<IScreen> screens)
        {
            _screens = screens;

            foreach (var screen in _screens)
            {
                screen.RequestScreenChange = SetActiveScreen;
            }
        }

        public void LoadScreens()
        {
            foreach (var screen in _screens)
            {
                screen.Load();
            }
        }
        
        public void SetActiveScreen(ScreenType screenType)
        {
            foreach (var screen in _screens)
            {
                if (screen.ScreenType != screenType) continue;
                _activeScreen = screen;
                break;
            }
        }

        public IScreen GetActiveScreen()
        {
            return _activeScreen;
        }
    }
}