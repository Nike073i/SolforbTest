using SolforbTest.EfContext.Context;

namespace SolforbTest.Core.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected SolforbDbContext Context { get; }

        protected TestCommandBase()
        {
            Context = SolforbDbContextFactory.Create();
        }

        public void Dispose()
        {
            SolforbDbContextFactory.Destroy(Context);
        }
    }
}
