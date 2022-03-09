using MediatR;
using NCachedBookStore.Contracts.Entities;
using NCachedBookStore.Contracts.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NCachedBookStore.Core.Handlers
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int BookId { get; set; }

        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }
    }

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IDataService _db;

        public GetBookByIdQueryHandler(IDataService db)
        {
            _db = db;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var bookId = request.BookId;

            Console.WriteLine($"Querying for single BookId: {bookId}");

            var book = await _db.Books.GetAsync(bookId);
            
            if (book == null) throw new FileNotFoundException();

            return book;
        }
    }
}
