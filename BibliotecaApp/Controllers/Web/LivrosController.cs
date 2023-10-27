﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaApp.Data;
using BibliotecaApp.Models;
using BibliotecaApp.Util;

namespace BibliotecaApp.Controllers.Web
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LivrosController : Controller
    {
        private readonly BibliotecaAppContext _context;

        public LivrosController(BibliotecaAppContext context)
        {
            _context = context;
        }

        // GET: Livros
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.Livro != null ?
                        View(await _context.Livro.ToListAsync()) :
                        Problem("Entity set 'BibliotecaAppContext.Livro'  is null.");
        }

        // GET: Livros/Details/5
        [HttpGet("Details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
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
        public async Task<IActionResult> Create([Bind("Titulo,Autor,AnoPublicacao,ISBN,QuantidadeDisponivel")] Livro livro)
        {
            if (!ModelState.IsValid) return View(livro);

            // lançam exceptions
            livro.Titulo.ValidarTitulo();
            livro.Autor.ValidarAutor();
            livro.AnoPublicacao.ValidarAnoPublicacao();
            livro.QuantidadeDisponivel.ValidarQuantidadeDisponivel();
            livro.ISBN.ValidarISBN();

            livro.Id = Guid.NewGuid();
            _context.Add(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Livros/Edit/5
        [HttpGet("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // PUT: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("Edit/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Titulo,Autor,AnoPublicacao,ISBN,QuantidadeDisponivel")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                livro.Titulo.ValidarTitulo();
                livro.Autor.ValidarAutor();
                livro.AnoPublicacao.ValidarAnoPublicacao();
                livro.QuantidadeDisponivel.ValidarQuantidadeDisponivel();
                livro.ISBN.ValidarISBN();

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
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
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
            if (_context.Livro == null)
            {
                return Problem("Entity set 'BibliotecaAppContext.Livro'  is null.");
            }
            var livro = await _context.Livro.FindAsync(id);
            if (livro != null)
            {
                _context.Livro.Remove(livro);
            }
            
            await _context.SaveChangesAsync();
            return Json(new { success = true, redirectToUrl = Url.Action("Index", "Livros") });
        }

        private bool LivroExists(Guid id)
        {
          return (_context.Livro?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}