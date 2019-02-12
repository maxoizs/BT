using Ninject.Modules;

namespace BS
{
    public class BSModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBoard>().To<Board>();
            Bind<IDisplayBoard>().To<ConsoleBoardDisplayer>();
            Bind<IPlayerInput>().To<UserInput>();
            Bind<IPlayerInput>().To<ComputerInput>();
            Bind<Game>().ToSelf();
        }
    }
}