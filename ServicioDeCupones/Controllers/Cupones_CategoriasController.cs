using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicioDeCupones.Data;
using ServicioDeCupones.Models;

namespace ApiServicioCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Cupon_CategoriaController : ControllerBase
    {
        private readonly DataContext _context;

        public Cupon_CategoriaController(DataContext context)
        {
            _context = context;
        }


        // crea las relaciones entre cupones y categorias
        [HttpPost("AsignarCuponACategorias")]
        public async Task<IActionResult> AsignarCuponACategorias(int idCupon, [FromBody] List<int> idsCategorias)
        {
            if (idsCategorias == null || idsCategorias.Count == 0)
            {
                return BadRequest("Debe proporcionar al menos una categoría.");
            }

            
            var cupon = await _context.Cupones.FindAsync(idCupon);
            if (cupon == null)
            {
                return NotFound("Cupón no encontrado.");
            }

            
            var categorias = await _context.Categorias
                                            .Where(c => idsCategorias.Contains(c.Id_Categoria))
                                            .ToListAsync();

            if (categorias.Count != idsCategorias.Count)
            {
                return NotFound("Una o más categorías no existen.");
            }

           
            var relaciones = idsCategorias.Select(idCategoria => new Cupones_CategoriasModel
            {
                Id_Cupon = idCupon,
                Id_Categoria = idCategoria
            }).ToList();

            _context.Cupones_Categorias.AddRange(relaciones);
            await _context.SaveChangesAsync();

            return Ok("Cupón asignado a las categorías exitosamente.");
        }

        [HttpGet("CategoriasPorCupon/{idCupon}")]
        public async Task<IActionResult> CategoriasPorCupon(int idCupon)
        {
            var categorias = await _context.Cupones_Categorias
                                           .Where(cc => cc.Id_Cupon == idCupon)
                                           .Select(cc => new
                                           {
                                               cc.Id_Categoria,
                                               NombreCategoria = cc.Categorias.Nombre
                                           })
                                           .ToListAsync();

            if (categorias == null || categorias.Count == 0)
            {
                return NotFound("No se encontraron categorías asociadas al cupón.");
            }

            return Ok(categorias);
        }

       
        
        // DELETE: api/Cupon_Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupon_CategoriaModel(int id)
        {
            var cupon_CategoriaModel = await _context.Cupones_Categorias.FindAsync(id);
            if (cupon_CategoriaModel == null)
            {
                return NotFound();
            }

            _context.Cupones_Categorias.Remove(cupon_CategoriaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Cupon_CategoriaModelExists(int id)
        {
            return _context.Cupones_Categorias.Any(e => e.Id_Cupones_Categorias == id);
        }
    }
}