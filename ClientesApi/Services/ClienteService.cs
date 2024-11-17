using ClientesApi.Interfaces;
using ClientesApi.Models.DTO;
using Newtonsoft.Json;
using System.Text;

namespace ClientesApi.Services
{
    public class ClienteService : IClienteService
    {
        public async Task<string> SolicitarCupon(ClientesDto clientesDto)
        {
            try
            {
                ClientesDto clienteDto = new ClientesDto();
                {


                };
                var jsonCliente = JsonConvert.SerializeObject(clienteDto);
                var contenido = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsync("https://localhost:7131/api/SolicitudCupones/SolicitarCupon", contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    var msg = await respuesta.Content.ReadAsStringAsync();  
                    return msg;
                }
                else
                {
                    var error = await respuesta.Content.ReadAsStringAsync(); 
                    throw new Exception($"{error}");  
                }

            }catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
               
            }
        }
    }
}
