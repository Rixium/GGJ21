﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Graphics
{
    internal class RenderManager : IRenderManager
    {
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDeviceManager GraphicsDeviceManager { get; set; }
    }
}