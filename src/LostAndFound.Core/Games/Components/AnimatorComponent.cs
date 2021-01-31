using System.Collections.Generic;
using LostAndFound.Core.Content;
using LostAndFound.Core.Extensions;
using LostAndFound.Core.Games.Entities;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games.Components
{
    public class AnimatorComponent : IComponent
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

        public IEntity Entity { get; set; }

        public void Start()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (!Animating) return;

            _currentFrameTime += gameTime.AsDelta();

            if (_currentFrameTime < ActiveAnimation?.FrameDuration) return;

            NextFrame();
        }

        public void Draw(SpriteBatch spriteBatch)
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