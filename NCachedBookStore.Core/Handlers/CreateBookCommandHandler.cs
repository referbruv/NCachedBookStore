using MediatR;
using NCachedBookStore.Contracts.DTO;
using NCachedBookStore.Contracts.Entities;
using NCachedBookStore.Contracts.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NCachedBookStore.Core.Handlers
{
    public class CreateBookCommand: IRequest<Book>
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

        public CreateBookCommandHandler(IDataService db)
        {
            _db = db;
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

            await _db.Books.AddAsync(book);
            
            return book;
        }
    }
}
