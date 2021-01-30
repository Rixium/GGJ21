﻿using Autofac;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Games;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Screens;
using LostAndFound.Core.System;
using LostAndFound.Core.Transitions;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Modules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Game1>().As<Game>();

            builder.RegisterType<ContentDeserializer>().As<IContentDeserializer>().InstancePerDependency();

            builder.RegisterType<RenderManager>().As<IRenderManager>().SingleInstance();

            builder.RegisterType<ScreenManager>().As<IScreenManager>().SingleInstance();
            builder.RegisterType<TransitionManager>().As<ITransitionManager>().InstancePerDependency();

            builder.RegisterType<SplashScreen>().As<IScreen>();
            builder.RegisterType<EmptyScreen>().As<IScreen>();
            builder.RegisterType<MainMenuScreen>().As<IScreen>();
            builder.RegisterType<GameScreen>().As<IScreen>();

            builder.RegisterType<ContentChest>().As<IContentChest>().SingleInstance();
            builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();
            builder.RegisterType<ApplicationFolder>().As<IApplicationFolder>().SingleInstance();

            builder.RegisterType<ContentChest>().As<IContentChest>().SingleInstance();

            builder.RegisterType<WindowConfiguration>().As<IWindowConfiguration>().SingleInstance();

            builder.RegisterType<GameInstance>().As<IGameInstance>().SingleInstance();
            builder.RegisterType<ZoneLoader>().As<IZoneLoader>().SingleInstance();

            builder.RegisterType<AsepriteSpriteMapLoader>().As<IContentLoader<AsepriteSpriteMap>>().SingleInstance();

            base.Load(builder);
        }
    }
}