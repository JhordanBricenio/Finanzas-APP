using System.ComponentModel.DataAnnotations.Schema;

namespace Finanzas_APP.Models
{

    [Table("Usuario")]
    public class Usuario
    {
        public int Id { get; set; }

        [Column("Nombre")]
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
