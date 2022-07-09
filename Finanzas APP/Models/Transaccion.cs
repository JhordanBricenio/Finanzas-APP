using System.ComponentModel.DataAnnotations.Schema;

namespace Finanzas_APP.Models
{

    [Table("Transaccion")]
    public class Transaccion
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Tipo { get; set; }


        public Decimal Monto { get; set; }

        [Column("Descripcion")]
        public string Nota { get; set; }
        public int CuentaId { get; set; }

        public virtual Cuenta? Cuenta { get; set; }


    }
}
