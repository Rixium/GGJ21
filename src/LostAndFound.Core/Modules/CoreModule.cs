using Autofac;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
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
            
            builder.RegisterType<ContentChest>().As<IContentChest>().SingleInstance();

            builder.RegisterType<WindowConfiguration>().As<IWindowConfiguration>().SingleInstance();

            base.Load(builder);
        }
    }
}