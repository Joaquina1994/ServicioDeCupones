using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServicioDeCupones.Data;
using ServicioDeCupones.Interfaces;
using ServicioDeCupones.Models;
using ServicioDeCupones.Models.DTO;
using System.Drawing;

namespace ServicioDeCupones.Controllers
{
    public class SolicitudCuponesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ICuponesService _cuponesServices;
        private readonly ISendEmailService _sendEmailService;

        public SolicitudCuponesController(DataContext dataContext, ICuponesService cuponesServices, ISendEmailService sendEmailService)
        {
            _dataContext = dataContext;
            _cuponesServices = cuponesServices;
            _sendEmailService = sendEmailService;
        }

        [HttpPost("SolicitarCupon")]
        public async Task<IActionResult> SolicitarCupon(ClientesDto clientesDto)
        {
            try
            {
                if (clientesDto.CodCliente.IsNullOrEmpty())
                
                    throw new Exception("El campo Dni no puede estar vacio");

                    string numeroCupon = await _cuponesServices.GenerarNumeroCupon();

                    Cupones_ClientesModel cupones_Cliente = new Cupones_ClientesModel();    
                    cupones_Cliente.id_Cupon = clientesDto.Id_Cupon;
                    cupones_Cliente.CodCliente = clientesDto.CodCliente;
                    cupones_Cliente.FechaAsignado = DateTime.Now;
                    cupones_Cliente.NroCupon = numeroCupon;

                    _dataContext.Cupones_Clientes.Add(cupones_Cliente);
                    await _dataContext.SaveChangesAsync();

                    await _sendEmailService.EnviarEmailCliente(clientesDto.Email, numeroCupon);    

                    return Ok(new
                    {
                        Mensaje = "El registro se dio de alta.",
                        NroCupon = numeroCupon
                    });

                

            }catch (Exception ex)
            {
                return BadRequest($"Error:{ex.Message}");
            }

        }

        /*[HttpPost("UsarCupon")]
        public async Task<IActionResult> UsarCupon(string NroCupon, string CodCliente)
        {


        }*/
    }
}
