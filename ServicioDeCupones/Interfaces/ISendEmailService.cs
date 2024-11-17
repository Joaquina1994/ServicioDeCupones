namespace ServicioDeCupones.Interfaces
{
    public interface ISendEmailService
    {
        Task EnviarEmailCliente(string emailCliente, string numeroCupon);
    }
}
