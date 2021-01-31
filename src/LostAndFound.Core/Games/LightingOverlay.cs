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

        private RenderTarget2D _renderTarget2D;

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

            _renderTarget2D = new RenderTarget2D(_renderManager.GraphicsDeviceManager.GraphicsDevice,
                _renderManager.GraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth,
                _renderManager.GraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight,
                false,
                _renderManager.GraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24, 0, RenderTargetUsage.PreserveContents);
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
            _renderManager.SpriteBatch.GraphicsDevice.SetRenderTarget(_renderTarget2D);
            _renderManager.SpriteBatch.GraphicsDevice.Clear(new Color(0, 0, 0, 255));

            if (_timeManager.DayTotalMinutes < 960)
            {
                _overlayColor = _nightColor * (float) (Map(720, 960, 0, NightIntensity, _timeManager.DayTotalMinutes));
            }
            else if (_timeManager.DayTotalMinutes < 1440 && _timeManager.DayTotalMinutes > 1200)
            {
                _overlayColor =
                    _nightColor * (float) (Map(1200, 1440, NightIntensity, 0, _timeManager.DayTotalMinutes));
            }

            var blend = new BlendState
            {
                AlphaSourceBlend = Blend.Zero,
                AlphaDestinationBlend = Blend.InverseSourceColor,
                ColorSourceBlend = Blend.Zero,
                ColorDestinationBlend = Blend.InverseSourceColor
            };

            _renderManager.SpriteBatch.Begin(blendState: blend, transformMatrix: camera.GetMatrix());

            _renderManager.SpriteBatch.Draw(Texture,
                new Rectangle(0, 0, _zoneManager.ActiveZone.Image.Width, _zoneManager.ActiveZone.Image.Height),
                Color.White * 0.5f);

            foreach (var light in _lights)
            {
                _renderManager.SpriteBatch.Draw(light.Texture,
                    new Rectangle((int) (light.Entity.Position.X + light.Offset.X) - light.Size / 2,
                        (int) (light.Entity.Position.Y + light.Offset.Y) - light.Size / 2, light.Size, light.Size),
                    light.LightColor * 0.5f);
            }

            _renderManager.SpriteBatch.End();

            _renderManager.SpriteBatch.GraphicsDevice.SetRenderTarget(null);
        }

        public void DrawNow()
        {
            _renderManager.SpriteBatch.Begin();
            _renderManager.SpriteBatch.Draw(_renderTarget2D, Vector2.Zero, _overlayColor);
            _renderManager.SpriteBatch.End();
        }

        double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}