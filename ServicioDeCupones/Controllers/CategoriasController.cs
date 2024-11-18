using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioDeCupones.Data;
using ServicioDeCupones.Models;

namespace ServicioDeCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet("ObtenerCategorias")]
        public async Task<ActionResult<IEnumerable<CategoriasModel>>> ObtenerCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("ObtenerCategoriaPorId{id}")]
        public async Task<ActionResult<CategoriasModel>> ObtenerCategoriaPorId(int id)
        {
            var categoriasModel = await _context.Categorias.FindAsync(id);

            if (categoriasModel == null)
            {
                return NotFound();
            }

            return categoriasModel;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarCategoria(int id, CategoriasModel categoriasModel)
        {
            if (id != categoriasModel.Id_Categoria)
            {
                return BadRequest();
            }

            _context.Entry(categoriasModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriasModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AgregarCategoria")]
        public async Task<ActionResult<CategoriasModel>> AgregarCategoria(CategoriasModel categoriasModel)
        {
            _context.Categorias.Add(categoriasModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ObtenerCategoriaPorId", new { id = categoriasModel.Id_Categoria }, categoriasModel);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarCategoria(int id)
        {
            var categoriasModel = await _context.Categorias.FindAsync(id);
            if (categoriasModel == null)
            {
                return NotFound();
            }

            categoriasModel.Activo = false;

            _context.Categorias.Update(categoriasModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriasModelExists(int id)
        {
            return _context.Categorias.Any(e => e.Id_Categoria == id);
        }
    }
}
