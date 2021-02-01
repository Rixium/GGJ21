using Autofac;
using LostAndFound.Core.Config;
using LostAndFound.Core.Content;
using LostAndFound.Core.Content.Aseprite;
using LostAndFound.Core.Content.ContentLoader;
using LostAndFound.Core.Games;
using LostAndFound.Core.Games.Animals;
using LostAndFound.Core.Games.Components;
using LostAndFound.Core.Games.Interfaces;
using LostAndFound.Core.Games.Person;
using LostAndFound.Core.Games.Systems;
using LostAndFound.Core.Games.Zones;
using LostAndFound.Core.Graphics;
using LostAndFound.Core.Input;
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
            builder.RegisterType<GameInterface>().InstancePerDependency();

            builder.RegisterType<InputManager>().As<IInputManager>().SingleInstance();

            builder.RegisterType<AsepriteSpriteMapLoader>().As<IContentLoader<AsepriteSpriteMap>>().SingleInstance();

            builder.RegisterType<LightingOverlay>();

            builder.RegisterType<PersonFactory>().As<IPersonFactory>().SingleInstance();
            builder.RegisterType<AnimalFactory>().As<IAnimalFactory>().SingleInstance();
            builder.RegisterType<ZoneManager>().SingleInstance();
            builder.RegisterType<SkyboxManager>().SingleInstance();

            RegisterAnimationSets(builder);
            RegisterComponents(builder);
            RegisterSystems(builder);

            base.Load(builder);
        }

        private static void RegisterAnimationSets(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerAnimationSet>();
        }

        private static void RegisterSystems(ContainerBuilder builder)
        {
            builder.RegisterType<SystemManager>().SingleInstance();
            builder.RegisterType<QuestSystem>().As<ISystem>().SingleInstance();
            builder.RegisterType<AmbientSoundManager>().As<ISystem>().SingleInstance();
            builder.RegisterType<TimeManager>().As<ISystem>().SingleInstance();
            builder.RegisterType<AudioSystem>().As<ISystem>().SingleInstance();
        }

        private static void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerAnimationComponent>().InstancePerDependency();
            builder.RegisterType<AnimatorComponent>().InstancePerDependency();
            builder.RegisterType<AnimationDrawComponent>().InstancePerDependency();

            builder.RegisterType<PersonInteractionComponent>().InstancePerDependency();
            builder.RegisterType<StaticDrawComponent>().InstancePerDependency();
            builder.RegisterType<PlayerControllerComponent>().InstancePerDependency();
            builder.RegisterType<ZoneInteractionComponent>().InstancePerDependency();
            builder.RegisterType<BoxColliderComponent>().InstancePerDependency();
            builder.RegisterType<SoundComponent>().InstancePerDependency();
            builder.RegisterType<PlayerSoundManagerComponent>().InstancePerDependency();
            builder.RegisterType<QuestGiverComponent>().InstancePerDependency();
            builder.RegisterType<QuestHolderComponent>().InstancePerDependency();
            builder.RegisterType<LightComponent>().InstancePerDependency();
            builder.RegisterType<QuestFulfilmentComponent>().InstancePerDependency();
            builder.RegisterType<MoneyBagComponent>().InstancePerDependency();
            builder.RegisterType<AnimalHolderComponent>().InstancePerDependency();
            builder.RegisterType<AnimalSoundComponent>().InstancePerDependency();
            builder.RegisterType<BounceComponent>().InstancePerDependency();
            builder.RegisterType<DialogComponent>().InstancePerDependency();
            builder.RegisterType<WandererComponent>().InstancePerDependency();
        }
    }
}