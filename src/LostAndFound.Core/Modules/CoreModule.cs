using Autofac;
using LostAndFound.Core.Graphics;
using Microsoft.Xna.Framework;

namespace LostAndFound.Core.Modules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Game1>().As<Game>();

            builder.RegisterType<RenderManager>().As<IRenderManager>().SingleInstance();

            base.Load(builder);
        }
    }
}