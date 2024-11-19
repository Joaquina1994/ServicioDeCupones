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

        [HttpPost("EnviarSolicitudCupones")]
        public async Task<IActionResult> EnviarSolicitudCupones([FromBody] ClientesDto clientesDto)
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

        [HttpPost("QuemarCupon")]
        public async Task<IActionResult> QuemarCupon([FromQuery]string nroCupon, ClientesDto clientesDto)
        {
            try
            {
                var respuesta = await _clienteService.QuemarCupon(nroCupon);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error:{ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCuponesActivos([FromQuery]string codCliente)
        {
            try
            {
                var respuesta = await _clienteService.ObtenerCuponesActivos(codCliente);
                return Ok(respuesta);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error:{ex.Message}");
            }
            
        }
       
    }
}
