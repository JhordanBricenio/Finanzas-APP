using Finanzas_APP.Models;
using Microsoft.EntityFrameworkCore;
using Finanzas_APP.Models.Mapping;

namespace Finanzas_APP.DB
{
    public class BbEntities : DbContext
    
    {

        //Necesario para ejecutar pruebas unitarias
        public BbEntities() { }


        public BbEntities(DbContextOptions<BbEntities> options) : base(options){ }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CuentaMapping());
            modelBuilder.ApplyConfiguration(new TransacMapping());

        }



        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Transaccion> Transaccions { get; set; }
        public virtual DbSet<Categoria> TipoCuentas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }


       
    }
}
