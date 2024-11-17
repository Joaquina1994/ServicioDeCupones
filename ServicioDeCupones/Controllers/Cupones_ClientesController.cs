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
    public class Cupones_ClientesController : ControllerBase
    {
        private readonly DataContext _context;

        public Cupones_ClientesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Cupones_Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupones_ClientesModel>>> GetCupones_Clientes()
        {
            return await _context.Cupones_Clientes.ToListAsync();
        }

        // GET: api/Cupones_Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupones_ClientesModel>> GetCupones_ClientesModel(string id)
        {
            var cupones_ClientesModel = await _context.Cupones_Clientes.FindAsync(id);

            if (cupones_ClientesModel == null)
            {
                return NotFound();
            }

            return cupones_ClientesModel;
        }

        // PUT: api/Cupones_Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupones_ClientesModel(string id, Cupones_ClientesModel cupones_ClientesModel)
        {
            if (id != cupones_ClientesModel.NroCupon)
            {
                return BadRequest();
            }

            _context.Entry(cupones_ClientesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupones_ClientesModelExists(id))
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

        // POST: api/Cupones_Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cupones_ClientesModel>> PostCupones_ClientesModel(Cupones_ClientesModel cupones_ClientesModel)
        {
            _context.Cupones_Clientes.Add(cupones_ClientesModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Cupones_ClientesModelExists(cupones_ClientesModel.NroCupon))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Se dio de alta el registro en Cupon_Cliente");
        }

        // DELETE: api/Cupones_Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupones_ClientesModel(string id)
        {
            var cupones_ClientesModel = await _context.Cupones_Clientes.FindAsync(id);
            if (cupones_ClientesModel == null)
            {
                return NotFound();
            }

            _context.Cupones_Clientes.Remove(cupones_ClientesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Cupones_ClientesModelExists(string id)
        {
            return _context.Cupones_Clientes.Any(e => e.NroCupon == id);
        }
    }
}
