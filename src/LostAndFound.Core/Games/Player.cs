using System;
using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Games
{
    public class Player
    {
        private readonly GameData _gameData;
        private PlayerData PlayerData => _gameData.PlayerData;

        public Texture2D Image;
        public Func<Movement, Rectangle, bool> PlayerMove;

        public Rectangle Bounds =>
            new Rectangle((int) PlayerData.Position.X, (int) PlayerData.Position.Y, Image.Width, Image.Height);

        public Player(Texture2D texture, GameData gameData)
        {
            Image = texture;
            _gameData = gameData;
        }

        public void Update(PlayerData playerData, GameTime gameTime)
        {
            var movement = new Movement();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                movement.X = -1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                movement.X = 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                movement.Y = -1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                movement.Y = 1;
            }

            if (PlayerMove(movement, Bounds))
            {
                playerData.Position += movement.ToVector2;
            }
        }
    }

    public class Movement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector2 ToVector2 => new Vector2(X, Y);
    }
}