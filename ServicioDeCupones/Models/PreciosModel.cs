﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioDeCupones.Models
{
    [Table("Precios")]
    public class PreciosModel
    {
        [Key]
        public int Id_Precio { get; set; }
        public int Id_Articulo { get; set; }
        public decimal Precio { get; set; } 

    }
}
