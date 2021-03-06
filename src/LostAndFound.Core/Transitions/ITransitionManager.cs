﻿using System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Transitions
{
    public interface ITransitionManager
    {
        Action FadeOutEnded { get; set; }
        Action FadeInEnded { get; set; }
        int FadeScale { get; set; }
        void Load();
        void Update(GameTime gameTime);
        void Draw();
        void SetState(FadeState fadeState);
        void SetFade(float fade);
    }
}