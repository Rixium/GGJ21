using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Input
{
    public interface IInputManager
    {
        bool KeyPressed(Keys key);
        bool KeyReleased(Keys key);
        bool KeyHeld(Keys key);
        void Update(GameTime gameTime);
    }
}