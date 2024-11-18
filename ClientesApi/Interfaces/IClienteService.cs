using ClientesApi.Models.DTO;

namespace ClientesApi.Interfaces
{
    public interface IClienteService
    {
        Task<string> SolicitarCupon(ClientesDto clientesDto);
        Task<string> QuemarCupon(string nroCupon);
        Task<string> ObtenerCuponesActivos(string codCliente);
    }
}
