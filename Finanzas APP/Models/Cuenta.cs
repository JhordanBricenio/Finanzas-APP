using System.ComponentModel.DataAnnotations.Schema;

namespace Finanzas_APP.Models
{
    [Table("Cuenta")]
    public class Cuenta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public decimal Monto { get; set; }
        public string Moneda { get; set; }

        public int UsuarioId { get; set; }

        public int CategoriaId { get; set; }


        //La cuenta tiene un Tipo de cuenta asociado Realacion de 1 a N
        public Categoria? Categoria { get; set; }

        public Usuario? Usuario { get; set; }

    }
}
