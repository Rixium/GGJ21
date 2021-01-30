using System;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Transitions
{
    public enum FadeState
    {
        None,
        FadingIn,
        FadingOut
    }

    public class TransitionManager : ITransitionManager
    {
        private readonly IRenderManager _renderManager;
        private readonly IWindowConfiguration _windowConfiguration;
        private readonly IContentChest _contentChest;
        private Texture2D _pixel;

        public Action FadeOutEnded { get; set; }
        public Action FadeInEnded { get; set; }
        public int FadeScale { get; set; } = 1;

        private float _currentFade = 1;
        private FadeState _fadeState = FadeState.FadingIn;

        public TransitionManager(IRenderManager renderManager, IWindowConfiguration windowConfiguration,
            IContentChest contentChest)
        {
            _renderManager = renderManager;
            _windowConfiguration = windowConfiguration;
            _contentChest = contentChest;
        }

        public void Load()
        {
            _pixel = _contentChest.Get<Texture2D>("utils/pixel");
        }

        public void Update(GameTime gameTime)
        {
            if (_fadeState == FadeState.FadingIn)
            {
                FadeIn(gameTime);
            }
            else if (_fadeState == FadeState.FadingOut)
            {
                FadeOut(gameTime);
            }
        }

        private void FadeOut(GameTime gameTime)
        {
            _currentFade += gameTime.AsDelta() * FadeScale;

            if (_currentFade < 1)
            {
                return;
            }

            _currentFade = 1;
            _fadeState = FadeState.None;
            
            FadeOutEnded?.Invoke();
        }

        private void FadeIn(GameTime gameTime)
        {
            _currentFade -= gameTime.AsDelta() * FadeScale;

            if (_currentFade > 0)
            {
                return;
            }

            _currentFade = 0;
            _fadeState = FadeState.None;
            
            FadeInEnded?.Invoke();
        }

        public void Draw()
        {
            _renderManager.SpriteBatch.Draw(_pixel,
                new Rectangle(0, 0, _windowConfiguration.WindowWidth, _windowConfiguration.WindowHeight),
                Color.Black * _currentFade);
        }

        public void SetState(FadeState fadeState) => _fadeState = fadeState;

        public void SetFade(float fade) => _currentFade = fade;
    }
}