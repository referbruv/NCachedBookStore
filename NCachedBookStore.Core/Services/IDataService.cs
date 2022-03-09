using NCachedBookStore.Contracts.Repositories;
using System.Threading.Tasks;

namespace NCachedBookStore.Contracts.Services
{
    public interface IDataService
    {
        public IBookRepository Books { get; }
    }
}
