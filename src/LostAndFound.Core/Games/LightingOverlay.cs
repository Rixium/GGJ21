using LostAndFound.Core.Content;
using LostAndFound.Core.Games.Systems;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class LightingOverlay
    {
        public Texture2D Texture { get; set; }
        public double NightIntensity { get; set; } = 0.7;

        private readonly TimeManager _timeManager;
        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;

        private Color _nightColor = new Color(0, 2, 20);
        private Color _dayColor = new Color(252, 219, 3);
        private Color _overlayColor = Color.Black * 0;

        public LightingOverlay(SystemManager systemManager, IRenderManager renderManager, IContentChest contentChest)
        {
            _timeManager = systemManager.GetSystem<TimeManager>();
            _renderManager = renderManager;
            _contentChest = contentChest;
        }

        public void Load()
        {
            Texture = _contentChest.Get<Texture2D>("Utils/pixel");
        }

        public void Draw()
        {
            if (_timeManager.DayTotalMinutes < 960)
            {
                _overlayColor = _nightColor * (float) (Map(720, 960, 0, NightIntensity, _timeManager.DayTotalMinutes));
            }
            else if (_timeManager.DayTotalMinutes < 1440 && _timeManager.DayTotalMinutes > 1200)
            {
                _overlayColor = _nightColor * (float) (Map(1200, 1440, NightIntensity, 0, _timeManager.DayTotalMinutes));
            }

            _renderManager.SpriteBatch.Draw(Texture, new Rectangle(0, 0, 1280, 720), _overlayColor);
        }

        double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}