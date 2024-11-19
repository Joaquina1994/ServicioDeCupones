namespace ServicioDeCupones.Interfaces
{
    public interface ISendEmailService
    {
        Task EnviarEmailCliente(string emailCliente, string numeroCupon);
        Task EnviarEmailClienteCuponUsado(string emailCliente, string numeroCupon);
    }
}
