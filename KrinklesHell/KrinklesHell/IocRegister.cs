using KrinklesHell.SpriteStuff;
using Utilities.Logging;

namespace KrinklesHell
{
    public static class IocRegister
    {
        public static void Initialize()
        {
            RegisterTypes();
            RegisterSingletons();
        }

        private static void RegisterTypes()
        {
        }

        private static void RegisterSingletons()
        {
            Ioc.RegisterSingleton<ILogger, Logger>();
            Ioc.RegisterSingleton<ISpriteManager, SpriteManager>();
        }
    }
}