using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Data;
using BibliotecaApp.Models;
using BibliotecaApp.Validations;
using BibliotecaApp.Extensions;
using BibliotecaApp.Pages;
using BibliotecaApp.Models.ViewModel;
using BibliotecaApp.Pages.Shared;
using Microsoft.AspNetCore.Identity;

namespace BibliotecaApp.Controllers.Web
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LivrosController : Controller
    {
        private readonly BibliotecaAppContext _context;
        private readonly ILogger<LivrosController> _logger;

        public LivrosController(BibliotecaAppContext context, ILogger<LivrosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Livros
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.Livros != null ?
                        View(await _context.Livros.ToListAsync()) :
                        Problem("Entity set 'BibliotecaAppContext.Livro'  is null.");
        }


        // GET: Livros
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            return _context.Livros != null ?
                        View(await _context.Livros.ToListAsync()) :
                        Problem("Entity set 'BibliotecaAppContext.Livro'  is null.");
        }

        // GET: Livros/Details/5
        [HttpGet("Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Autor,AnoPublicacao,ISBN,QuantidadeDisponivel,CapaUrl")] Livro livro)
        {
            if (!ModelState.IsValid) return View(livro);

            ModelState.AddModelErrorIfNotEmpty("Titulo", livro.Titulo.ValidarTitulo());
            ModelState.AddModelErrorIfNotEmpty("Autor", livro.Autor.ValidarAutor());
            ModelState.AddModelErrorIfNotEmpty("AnoPublicacao", livro.AnoPublicacao.ValidarAnoPublicacao());
            ModelState.AddModelErrorIfNotEmpty("QuantidadeDisponivel", livro.QuantidadeDisponivel.ValidarQuantidadeDisponivel());
            ModelState.AddModelErrorIfNotEmpty("ISBN", livro.ISBN.ValidarISBN());

            var livroExistente = await _context.Livros.FirstOrDefaultAsync(l => l.ISBN == livro.ISBN);
            if(livroExistente != null)
            {
                ModelState.AddModelErrorIfNotEmpty("ISBN", "ISBN já registrado");
            }

            if (ModelState.IsValid)
            { 
                livro.Id = Guid.NewGuid();
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(livro);
        }

        // GET: Livros/Edit/5
        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // PUT: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        [Route("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Titulo,Autor,AnoPublicacao,ISBN,QuantidadeDisponivel,CapaUrl")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(livro);

            ModelState.AddModelErrorIfNotEmpty("Titulo", livro.Titulo.ValidarTitulo());
            ModelState.AddModelErrorIfNotEmpty("Autor", livro.Autor.ValidarAutor());
            ModelState.AddModelErrorIfNotEmpty("AnoPublicacao", livro.AnoPublicacao.ValidarAnoPublicacao());
            ModelState.AddModelErrorIfNotEmpty("QuantidadeDisponivel", livro.QuantidadeDisponivel.ValidarQuantidadeDisponivel());
            ModelState.AddModelErrorIfNotEmpty("ISBN", livro.ISBN.ValidarISBN());

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { success = true, redirectToUrl = Url.Action("Index", "Livros") });
            }

            return View(livro);
        }

        // GET: Livros/Delete/5
        [HttpGet("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Livros == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // DELETE: Livros/Delete/5
        [HttpDelete("Delete/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Livros == null)
            {
                return Problem("Entity set 'BibliotecaAppContext.Livro'  is null.");
            }
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
            }
            
            await _context.SaveChangesAsync();
            return Json(new { success = true, redirectToUrl = Url.Action("Index", "Livros") });
        }

        // GET: Livros/Emprestimo/{livroId}
        [HttpGet("Emprestimo/{livroId:guid}")]
        public async Task<IActionResult> Emprestimo([FromRoute] Guid? livroId)
        {
            var livro = await _context.Livros.FindAsync(livroId);

            if (livro is null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (userId is null)
                return NotFound();

            var viewModel = new EmprestimoViewModel
            {
                LivroId = livroId!.Value,
                UsuarioId = userId,
                Titulo = livro.Titulo,
                LivroDisponivel = livro.QuantidadeDisponivel > 0,
                DataRetirada = DateOnly.FromDateTime(DateTime.Today), // Inicialização direta aqui
                DataPrevistaDevolucao = DateOnly.FromDateTime(DateTime.Today.AddDays(7)) // Inicialização direta aqui
            };

            return View(viewModel);
        }

        // POST: Livros/Emprestimo
        [HttpPost("Emprestimo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Emprestimo([Bind("LivroId,UsuarioId")] Guid livroId, string usuarioId)
        {
            var livro = await _context.Livros.FindAsync(livroId);

            if(livro is null)
                return NotFound();
            
            if(livro.QuantidadeDisponivel <= 0)
            {
                return View("Error", new ErrorModel { Message = "Livro não disponível para empréstimo." });
            }

            var emprestimo = new Emprestimo
            {
                LivroId = livroId,
                UsuarioId = usuarioId,
                DataRetirada = DateOnly.FromDateTime(DateTime.Now.Date)
            };

            livro.QuantidadeDisponivel -= 1;

            _context.Add(emprestimo);
            await _context.SaveChangesAsync();

            _context.Update(livro);
            await _context.SaveChangesAsync();

            return RedirectToAction("ConfirmacaoEmprestimo", new { id = emprestimo.Id });
        }

        // GET: Livros/ConfirmacaoEmprestimo/{id}
        [HttpGet("ConfirmacaoEmprestimo/{id}")]
        public async Task<IActionResult> ConfirmacaoEmprestimo(Guid id)
        {
            var emprestimo = await _context.Emprestimos.Include(emp => emp.Livro).FirstOrDefaultAsync(emp => emp.Id == id);

            if (emprestimo == null)
            {
                return NotFound();
            }
            
            return View(emprestimo);
        }

        // GET: Livros/Reserva
        /*[HttpGet("Reserva")]
        public IActionResult Reserva()
        {
            return View();
        }

        // POST: Livros/Reserva
        [HttpPost("Reserva")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserva([Bind("LivroId")] Guid livroId)
        {
            var livro = await _context.Livros.FindAsync(livroId);

            if (livro is null)
                return NotFound();

            return View();
        }*/

        private bool LivroExists(Guid id)
        {
          return (_context.Livros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
