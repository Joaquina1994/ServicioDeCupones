using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioDeCupones.Data;
using ServicioDeCupones.Models;

namespace ServicioDeCupones.Controllers
{
    public class ArticulosCategoriasController : Controller
    {
        private readonly DataContext _context;

        public ArticulosCategoriasController(DataContext context)
        {
            _context = context;
        }
        [HttpPost("AsignarArticulosACategorias")]
        public async Task<IActionResult> AsignarArticulosACategorias(int categoriaId, List<int> articuloIds)
        {

            var categoria = await _context.Set<CategoriasModel>().FindAsync(categoriaId);
            if (categoria == null)
            {
                return NotFound("Categoría no encontrada.");
            }

            
            var articulos = await _context.Articulos
                .Where(a => articuloIds.Contains(a.Id_Articulo))
                .ToListAsync();

            if (articulos.Count != articuloIds.Count)
            {
                return BadRequest("Uno o más artículos no existen.");
            }

            
            foreach (var articulo in articulos)
            {
                articulo.Id_Categoria = categoriaId;
            }

            
            await _context.SaveChangesAsync();

            return Ok("Artículos asignados correctamente.");
        }
    }
}
