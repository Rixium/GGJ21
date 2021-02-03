using System.Collections.Generic;
using Asepreadr.Graphics;
using LostAndFound.Core.Content;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class AnimatorComponent : Component
    {
        private readonly Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();

        private string _currentAnimation = "Walk";
        private int _frameNumber;
        private float _currentFrameTime;

        public Animation ActiveAnimation =>
            _animations.ContainsKey(_currentAnimation) ? _animations[_currentAnimation] : null;

        public Sprite Current => ActiveAnimation?
            .Sprites[_frameNumber];

        public bool Animating { get; set; }

        public void SetAnimation(string animationName)
        {
            Animating = true;

            if (animationName.Equals(_currentAnimation)) return;

            _currentAnimation = animationName;
            _frameNumber = 0;
            _currentFrameTime = 0;
        }

        public override void Start()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!Animating) return;

            _currentFrameTime += gameTime.AsDelta();

            if (_currentFrameTime < ActiveAnimation?.FrameDuration) return;

            NextFrame();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public void StopAnimation()
        {
            Animating = false;
            _frameNumber = 0;
            _currentFrameTime = 0;
        }

        private void NextFrame()
        {
            _frameNumber++;
            _currentFrameTime = 0;

            if (_frameNumber >= ActiveAnimation?.FrameCount)
                _frameNumber = 0;
        }

        public void AddAnimation(string animationName, Animation animation) =>
            _animations.Add(animationName, animation);

        public void SetUp(IAnimationSet animationSet) => animationSet.AddTo(this);
    }
}