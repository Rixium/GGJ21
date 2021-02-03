using Asepreadr;
using LostAndFound.Core.Games.Systems;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class SkyboxManager
    {
        private readonly IRenderManager _renderManager;
        private readonly SystemManager _systemManager;
        private readonly IContentChest _contentChest;

        private Texture2D _pixel;
        private Color _skyboxColor;
        private Color _dayColor = new Color(161, 230, 255);
        private Color _nightColor = new Color(7, 11, 28);

        public SkyboxManager(IRenderManager renderManager, SystemManager systemManager, IContentChest contentChest)
        {
            _renderManager = renderManager;
            _systemManager = systemManager;
            _contentChest = contentChest;
        }

        public void Load()
        {
            _pixel = _contentChest.Get<Texture2D>("Utils/pixel");
        }

        public void Draw()
        {
            if (_systemManager.GetSystem<TimeManager>().DayTotalMinutes < 960)
            {
                _skyboxColor = Color.Lerp(_dayColor, _nightColor, (float) Map(720, 960, 0, 1,
                    _systemManager.GetSystem<TimeManager>().DayTotalMinutes)) ;
            }
            else if (_systemManager.GetSystem<TimeManager>().DayTotalMinutes < 1440 &&
                     _systemManager.GetSystem<TimeManager>().DayTotalMinutes > 1200)
            {
                _skyboxColor = Color.Lerp(_nightColor, _dayColor, (float) Map(1200, 1440, 0, 1,
                    _systemManager.GetSystem<TimeManager>().DayTotalMinutes));
            }

            _renderManager.SpriteBatch.Draw(_pixel, new Rectangle(0, 0, 1280, 720), _skyboxColor);
        }

        double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}