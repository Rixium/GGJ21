using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Graphics
{
    public interface IRenderManager
    {
        SpriteBatch SpriteBatch { get; set; }
        public GraphicsDeviceManager GraphicsDeviceManager { get; set; }
    }
}