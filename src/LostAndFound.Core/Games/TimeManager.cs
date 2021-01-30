using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    public class TimeManager
    {
        public int Hour { get; set; } = 8;
        public double Minutes { get; set; }
        public double DayTotalMinutes { get; set; }
        public int TimeScale { get; set; } = 3;

        private int _dayBegin;

        public TimeManager()
        {
            _dayBegin = Hour;
        }

        public void UpdateTime(GameTime gameTime)
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
    }
}