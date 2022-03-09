using MediatR;
using NCachedBookStore.Contracts.DTO;
using NCachedBookStore.Contracts.Entities;
using NCachedBookStore.Contracts.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NCachedBookStore.Core.Handlers
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public int BookId { get; set; }
        public BookDto Entity { get; set; }

        public UpdateBookCommand(BookDto entity, int bookId)
        {
            Entity = entity;
            BookId = bookId;
        }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IDataService _db;

        public UpdateBookCommandHandler(IDataService db)
        {
            _db = db;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookId = request.BookId;
            if (bookId == 0) throw new InvalidDataException();

            var entity = request.Entity;

            var book = await _db.Books.GetAsync(bookId);
            if (book == null) throw new FileNotFoundException();

            book.AuthorName = entity.AuthorName;
            book.Description = entity.Description;
            book.Price = entity.Price;
            book.ISBN = entity.ISBN;
            book.Name = entity.Name;

            await _db.Books.UpdateAsync(book);
            
            return book;
        }
    }
}
