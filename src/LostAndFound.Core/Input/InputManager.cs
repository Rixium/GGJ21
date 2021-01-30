using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Input
{
    public class InputManager : IInputManager
    {
        private KeyboardState _lastKeyState;
        private KeyboardState _currentState;

        public bool KeyPressed(Keys key) => _currentState.IsKeyDown(key) && _lastKeyState.IsKeyUp(key);

        public bool KeyReleased(Keys key)
        {
            throw new NotImplementedException();
        }

        public bool KeyHeld(Keys key)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            _lastKeyState = _currentState;
            _currentState = Keyboard.GetState();
        }

        public bool KeyDown(Keys key) => _currentState.IsKeyDown(key);
    }
}