using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Cupones_Historial")]
    public class Cupones_HistorialModel
    {       
        public int id_Cupon { get; set; }
        public string NroCupon { get; set; }    
        public DateOnly FechaUso { get; set; }
        public string CodCliente { get; set; }

    }
}
