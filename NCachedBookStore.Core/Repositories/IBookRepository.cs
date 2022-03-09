using NCachedBookStore.Contracts.DTO;
using NCachedBookStore.Contracts.Entities;
using System.Threading.Tasks;

namespace NCachedBookStore.Contracts.Repositories
{
    public interface IBookRepository : IRepository<Book> { }
}
