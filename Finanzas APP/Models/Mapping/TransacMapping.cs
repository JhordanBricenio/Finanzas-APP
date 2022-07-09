using Finanzas_APP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finanzas_APP.Models.Mapping
{
    public class TransacMapping : IEntityTypeConfiguration<Transaccion>
    {
        public void Configure(EntityTypeBuilder<Transaccion> builder)
        {
            builder.ToTable("Transaccion");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Cuenta)
                .WithMany()
                .HasForeignKey(o => o.CuentaId);
        }
    }
    
}
