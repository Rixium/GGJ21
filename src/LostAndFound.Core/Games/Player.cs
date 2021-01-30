using LostAndFound.Core.Games.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAndFound.Core.Games
{
    public class Player
    {
        public Texture2D Image;

        public Player(Texture2D texture)
        {
            Image = texture;
        }

        public void Update(PlayerData playerData, GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                playerData.Position -= new Vector2(1, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                playerData.Position += new Vector2(1, 0);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                playerData.Position -= new Vector2(0, 1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                playerData.Position += new Vector2(0, 1);
            }
        }
    }
}