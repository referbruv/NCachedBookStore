using MediatR;
using Microsoft.AspNetCore.Mvc;
using NCachedBookStore.Contracts.DTO;
using NCachedBookStore.Core.Handlers;
using System.Threading.Tasks;

namespace NCachedBookStore.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: BooksController
        public async Task<ActionResult> Index()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());
            return View(books);
        }

        public async Task<IActionResult> EditSuccessAsync(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            return View("Success", book);
        }

        // GET: BooksController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            return View(book);
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(BookDto entity)
        {
            try
            {
                var book = await _mediator.Send(new CreateBookCommand(entity));   
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            return View(book);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, BookDto entity)
        {
            try
            {
                var book = await _mediator.Send(new UpdateBookCommand(entity, id));
                return RedirectToAction("EditSuccess", new { id });
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var _ = await _mediator.Send(new DeleteBookCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
