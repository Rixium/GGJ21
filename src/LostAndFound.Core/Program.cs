using System;
using Autofac;
using LostAndFound.Core.Modules;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core
{
    public static class Program
    {
        public static IContainer Container;

        [STAThread]
        private static void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<CoreModule>();
            Container = containerBuilder.Build();

            using var game = Container.Resolve<Game>();
            game.Run();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}