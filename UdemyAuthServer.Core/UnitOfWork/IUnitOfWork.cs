using System.Threading.Tasks;

namespace UdemyAuthServer.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
