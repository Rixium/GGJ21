using System;
using Autofac;
using LostAndFound.Core.Modules;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core
{
    public static class Program
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  You're doing really fucking well Dan, I am glad I met u so u can teach me how to become programming mastr. 1337 sh!t bro. //
        //  Now let's make some fucking games, fren! (Seriously this time, I won't quit... I really won't.                            //
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
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