using LostAndFound.Core.Games.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class TimeManager : ISystem
    {
        public int Hour { get; set; } = 8;
        public double Minutes { get; set; }
        public double DayTotalMinutes { get; set; }
        public int TimeScale { get; set; } = 500;

        private int _dayBegin;

        public TimeManager()
        {
            _dayBegin = Hour;
        }

        public void Start()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            Minutes += gameTime.ElapsedGameTime.TotalSeconds * TimeScale;
            DayTotalMinutes += gameTime.ElapsedGameTime.TotalSeconds * TimeScale;
            
            if (Minutes > 59)
            {
                Hour++;
                Minutes = 0;

                if (Hour > 24)
                {
                    Hour = 0;
                }

                if (Hour == _dayBegin)
                {
                    DayTotalMinutes = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}