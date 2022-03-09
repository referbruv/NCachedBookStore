using MediatR;
using NCachedBookStore.Contracts.Entities;
using NCachedBookStore.Contracts.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NCachedBookStore.Core.Handlers
{
    public class GetAllBooksQuery : IRequest<IEnumerable<Book>>
    {
    }

    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
    {
        private readonly IDataService _db;

        public GetAllBooksQueryHandler(IDataService db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _db.Books.GetAllAsync();
        }
    }
}
