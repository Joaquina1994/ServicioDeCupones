using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Articulos")]
    public class ArticulosModel
    {
        [Key]
        public int Id_Articulo { get; set; }
        public string Nombre_Articulo   { get; set; }
        public string Descripcion_Articulo { get; set; }
        public bool Activo { get; set; }

        [ForeignKey("Categoria")]
        public int Id_Categoria { get; set; } 
        public virtual CategoriasModel? Categoria { get; set; }
    }
}
