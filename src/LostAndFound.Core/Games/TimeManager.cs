using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Games
{
    public class TimeManager
    {
        public int Hour { get; set; } = 8;
        public int Minutes { get; set; }
        public int TimeScale { get; set; } = 1;

        private int _lastTick;

        public void UpdateTime(GameTime gameTime)
        {

            Minutes += gameTime.ElapsedGameTime.Seconds * TimeScale;
            
            if (Minutes > 59)
            {
                Hour++;
                Minutes = 0;

                if (Hour > 24)
                {
                    Hour = 0;
                }
            }
        }
    }
}