using MediatR;
using NCachedBookStore.Contracts.Services;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NCachedBookStore.Core.Handlers
{
    public class DeleteBookCommand : IRequest<int>
    {
        public int BookId { get; set; }

        public DeleteBookCommand(int bookId)
        {
            BookId = bookId;
        }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, int>
    {
        private readonly IDataService _db;

        public DeleteBookCommandHandler(IDataService db)
        {
            _db = db;
        }

        public async Task<int> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookId = request.BookId;
            if (bookId == 0) throw new InvalidDataException();

            await _db.Books.DeleteAsync(bookId);
            
            return bookId;
        }
    }
}
