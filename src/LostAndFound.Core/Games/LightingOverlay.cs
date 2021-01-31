using System.Collections.Generic;
using LostAndFound.Core.Content;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Systems;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAndFound.Core.Games
{
    public class LightingOverlay
    {
        public Texture2D Texture { get; set; }
        public double NightIntensity { get; set; } = 1;

        private readonly TimeManager _timeManager;
        private readonly IRenderManager _renderManager;
        private readonly IContentChest _contentChest;
        private readonly ZoneManager _zoneManager;

        private Color _nightColor = new Color(0, 2, 20);
        private Color _dayColor = new Color(252, 219, 3);
        private Color _overlayColor = Color.Black * 0;
        private Texture2D _lightTexture;
        private List<LightComponent> _lights = new List<LightComponent>();

        public LightingOverlay(SystemManager systemManager, IRenderManager renderManager, IContentChest contentChest,
            ZoneManager zoneManager)
        {
            _timeManager = systemManager.GetSystem<TimeManager>();
            _renderManager = renderManager;
            _contentChest = contentChest;
            _zoneManager = zoneManager;
        }

        public void Start()
        {
            GetLightComponents();
            _zoneManager.ZoneChanged += (type => GetLightComponents());
        }

        private void GetLightComponents()
        {
            _lights.Clear();

            foreach (var light in _zoneManager.ActiveZone.Entities)
            {
                var lightComponent = light.GetComponent<LightComponent>();

                if (lightComponent != null)
                {
                    _lights.Add(lightComponent);
                }
            }
        }

        public void Load()
        {
            Texture = _contentChest.Get<Texture2D>("Utils/pixel");
            _lightTexture = _contentChest.Get<Texture2D>("Utils/light");
        }

        public void Draw(Camera camera)
        {
            if (_timeManager.DayTotalMinutes < 960)
            {
                _overlayColor = _nightColor * (float) (Map(720, 960, 0, NightIntensity, _timeManager.DayTotalMinutes));
            }
            else if (_timeManager.DayTotalMinutes < 1440 && _timeManager.DayTotalMinutes > 1200)
            {
                _overlayColor =
                    _nightColor * (float) (Map(1200, 1440, NightIntensity, 0, _timeManager.DayTotalMinutes));
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////

            var m = Matrix.CreateOrthographicOffCenter(0,
                _renderManager.GraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth,
                _renderManager.GraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight,
                0, 0, 1
            );

            var a = new AlphaTestEffect(_renderManager.GraphicsDeviceManager.GraphicsDevice)
            {
                Projection = camera.GetMatrix() * m
            };

            var s1 = new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.Always,
                StencilPass = StencilOperation.Replace,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };

            var s2 = new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.LessEqual,
                StencilPass = StencilOperation.Keep,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };

            //////////////////////////////////////////////////////////////////////////////////////////////////////

            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, s1, null, a);

            foreach (var light in _lights)
            {
                light.Draw(_renderManager.SpriteBatch);
            }

            _renderManager.SpriteBatch.End();

            _renderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, s2, null, a);

            _renderManager.SpriteBatch.Draw(Texture,
                new Rectangle(0, 0, _zoneManager.ActiveZone.Image.Width, _zoneManager.ActiveZone.Image.Height),
                _overlayColor);

            _renderManager.SpriteBatch.End();
        }

        double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}