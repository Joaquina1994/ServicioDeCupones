using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientesApi.Data;
using ClientesApi.Models;
using ClientesApi.Interfaces;
using ClientesApi.Services;
using ClientesApi.Models.DTO;

namespace ClientesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /*// GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesModel>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesModel>> GetClientesModel(string id)
        {
            var clientesModel = await _context.Clientes.FindAsync(id);

            if (clientesModel == null)
            {
                return NotFound();
            }

            return clientesModel;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientesModel(string id, ClientesModel clientesModel)
        {
            if (id != clientesModel.CodCliente)
            {
                return BadRequest();
            }

            _context.Entry(clientesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> EnviarSolicitudCupones([FromBody]ClientesDto clientesDto)
        {
            try
            {
                var respuesta = await _clienteService.SolicitarCupon(clientesDto);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:{ex.Message}");
            }
        }
        
        /*// DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientesModel(string id)
        {
            var clientesModel = await _context.Clientes.FindAsync(id);
            if (clientesModel == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientesModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesModelExists(string id)
        {
            return _context.Clientes.Any(e => e.CodCliente == id);
        }*/
    }
}
