using Alachisoft.NCache.Client;
using Alachisoft.NCache.Runtime.Caching;
using Alachisoft.NCache.Runtime.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using NCachedBookStore.Contracts.DTO;
using NCachedBookStore.Contracts.Entities;
using NCachedBookStore.Contracts.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NCachedBookStore.Core.Handlers
{
    public class CreateBookCommand : IRequest<Book>
    {
        public BookDto Entity { get; set; }

        public CreateBookCommand(BookDto entity)
        {
            Entity = entity;
        }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IDataService _db;
        private readonly ICache _cache;

        public CreateBookCommandHandler(IDataService db, IConfiguration configuration)
        {
            _db = db;
            _cache = CacheManager.GetCache(configuration["NCacheSettings:CacheName"].ToString());
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Entity;
            if (entity == null) throw new InvalidDataException();

            var book = new Book
            {
                AuthorName = entity.AuthorName,
                Description = entity.Description,
                ISBN = entity.ISBN,
                Name = entity.Name,
                Price = entity.Price
            };

            // add book
            await _db.Books.AddAsync(book);

            //try
            //{
            //    // Pre-Condition: Cache is already connected

            //    string key = $"book.Id";

            //    // Create a new cacheItem with the product
            //    var cacheItem = new CacheItem(key);

            //    // Enable write through for the cacheItem created
            //    var writeThruOptions = new WriteThruOptions(WriteMode.WriteThru, "NCachedWriteThruProvider");

            //    //var writeThruOptions = new WriteThruOptions(WriteMode.WriteBehind, "NCachedWriteThruProvider");

            //    // Add the item in the cache with WriteThru enabled
            //    CacheItemVersion itemVersion = _cache.Insert(key, cacheItem, writeThruOptions);
            //}
            //catch (OperationFailedException ex)
            //{
            //    if (ex.ErrorCode == NCacheErrorCodes.BACKING_SOURCE_NOT_AVAILABLE)
            //    {
            //        // Backing source is not available
            //    }
            //    else if (ex.ErrorCode == NCacheErrorCodes.SYNCHRONIZATION_WITH_DATASOURCE_FAILED)
            //    {
            //        // Synchronization of data with backing source is failed due to any error
            //    }
            //    else
            //    {
            //        // Exception can occur due to:
            //        // Connection Failures
            //        // Operation Timeout
            //        // Operation performed during state transfer
            //    }
            //}
            //catch (Exception)
            //{
            //    // Any generic exception like ArgumentNullException or ArgumentException
            //    throw;
            //}

            return await Task.FromResult(book);
        }
    }
}
