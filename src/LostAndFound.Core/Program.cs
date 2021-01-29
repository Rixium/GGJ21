using System;
using Autofac;
using LostAndFound.Core.Modules;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<CoreModule>();
            var container = containerBuilder.Build();

            using var game = container.Resolve<Game>();
            game.Run();
        }
    }
}
