using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Articulos")]
    public class ArticulosModel
    {
        [Key]
        public int id_Articulos { get; set; }
        public string Nombre_Articulo   { get; set; }
        public string Descripcion_Articulo { get; set; }
        public bool Activo { get; set; }
    }
}
