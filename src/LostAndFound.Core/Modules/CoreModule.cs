using Autofac;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Screens;
using LostAndFound.Core.System;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Modules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Game1>().As<Game>();

            builder.RegisterType<FileSystem>().As<IFileSystem>().InstancePerDependency();
            builder.RegisterType<ContentDeserializer>().As<IContentDeserializer>().InstancePerDependency();

            builder.RegisterType<RenderManager>().As<IRenderManager>().SingleInstance();

            builder.RegisterType<ScreenManager>().As<IScreenManager>().SingleInstance();

            builder.RegisterType<SplashScreen>().As<IScreen>();
            builder.RegisterType<EmptyScreen>().As<IScreen>();
            builder.RegisterType<MainMenuScreen>().As<IScreen>();

            builder.RegisterType<ContentChest>().As<IContentChest>().SingleInstance();

            builder.RegisterType<WindowConfiguration>().As<IWindowConfiguration>().SingleInstance();

            base.Load(builder);
        }
    }
}