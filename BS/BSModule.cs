using Ninject;
using Ninject.Modules;

namespace BS
{
    public class BSModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBoard>().To<Board>();
            Bind<IDisplayBoard>().To<ConsoleBoardDisplayer>();
            Bind<IUserInput>().To<UserInput>();
            Bind<Game>().ToSelf();
        }
    }
}