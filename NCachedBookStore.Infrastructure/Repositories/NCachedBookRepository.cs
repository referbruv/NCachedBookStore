using Alachisoft.NCache.EntityFrameworkCore;
using NCachedBookStore.Contracts.Entities;
using NCachedBookStore.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCachedBookStore.Infrastructure.Repositories
{
    public class NCachedBookRepository : IBookRepository
    {
        private readonly DatabaseContext _context;

        public NCachedBookRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book entity)
        {
            _context.Books.Add(entity);
            await _context.SaveChangesAsync();

            string cacheKey;
            var cache = _context.GetCache();

            CachingOptions options = new CachingOptions
            {
                StoreAs = StoreAs.SeperateEntities
            };

            // Add to cache (without querying the database)
            cache.Insert(entity, out cacheKey, options);
        }

        public int Count()
        {
            return _context.Books.Count();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _context.Books.Find(id);
            _context.Books.Remove(entity);
            await _context.SaveChangesAsync();

            var cache = _context.GetCache();
            cache.Remove(entity);
        }

        public async Task<Book> GetAsync(int id)
        {
            CachingOptions options = new CachingOptions
            {
                StoreAs = StoreAs.SeperateEntities
            };

            var item = (from cust in _context.Books
                        where cust.Id == id
                        select cust).FromCache(options).ToList();

            return await Task.FromResult(item.FirstOrDefault());
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            CachingOptions options = new CachingOptions
            {
                StoreAs = StoreAs.SeperateEntities
            };
            var items = (from r in _context.Books select r).FromCache(options).ToList();
            return await Task.FromResult(items);
        }

        public async Task UpdateAsync(Book entity)
        {
            CachingOptions options = new CachingOptions
            {
                StoreAs = StoreAs.SeperateEntities
            };

            _context.Books.Update(entity);
            await _context.SaveChangesAsync();

            var cache = _context.GetCache();

            cache.Remove(entity);

            // Add to cache
            cache.Insert(entity, out string cacheKey, options);
        }
    }
}
