using NCachedBookStore.Contracts.Repositories;
using NCachedBookStore.Contracts.Services;
using NCachedBookStore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCachedBookStore.Infrastructure.Services
{
    public class DataService : IDataService
    {
        private readonly DatabaseContext _db;

        public DataService(DatabaseContext db)
        {
            _db = db;
        }

        public IBookRepository Books => new NCachedBookRepository(_db);

        public async Task CommitAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
