﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Finanzas_APP.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
