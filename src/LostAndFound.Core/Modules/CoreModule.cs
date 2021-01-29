using Autofac;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Screens;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Modules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Game1>().As<Game>();

            builder.RegisterType<RenderManager>().As<IRenderManager>().SingleInstance();
            
            builder.RegisterType<ScreenManager>().As<IScreenManager>().SingleInstance();

            builder.RegisterType<SplashScreen>().As<IScreen>();

            base.Load(builder);
        }
    }
}