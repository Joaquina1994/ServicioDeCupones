using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Tipo_Cupon")]
    public class Tipo_CuponModel
    {
        [Key]
        public int id_Tipo_Cupon { get; set; }  
        public string Nombre { get; set; }   

    }
}
