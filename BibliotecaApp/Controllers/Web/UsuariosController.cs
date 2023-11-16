using Microsoft.AspNetCore.Mvc;
using BibliotecaApp.Models;
using BibliotecaApp.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace BibliotecaApp.Controllers.Web;

[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class UsuariosController : Controller
{
    private readonly UserManager<Usuario> _userManager;

    public UsuariosController(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    /*// GET: Usuarios
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return _context.Usuarios != null ? 
            View(await _context.Usuarios.ToListAsync()) :
            Problem("Entity set 'BibliotecaAppContext.Usuarios'  is null.");
    }

    // GET: Usuarios/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(string? id)
    {

        if (id == null || _context.Usuarios == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }*/

    // GET: Usuarios/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Usuarios/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Email,Senha,ValidSenha")] UsuarioCreateViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var usuario = new Usuario {
            UserName = viewModel.Nome,
            Email = viewModel.Email
        };

        var result = await _userManager.CreateAsync(usuario, viewModel.Senha);

        if(result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(viewModel);
    }
/*
    // GET: Usuarios/Edit/5
    [HttpGet("Edit/{id:Guid}")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Usuarios == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    // PUT: Usuarios/Edit/5
    [HttpPut("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,Email,Senha,Type")] Usuario usuario)
    {
        if (id != usuario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    // GET: Usuarios/Delete/5
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null || _context.Usuarios == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(m => m.Id == id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // POST: Usuarios/Delete/5
    [HttpDelete("Delete/{id:Guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Usuarios == null)
        {
            return Problem("Entity set 'BibliotecaAppContext.Usuarios'  is null.");
        }
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
        }
            
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    private bool UsuarioExists(string id)
    {
        return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
    }*/
}