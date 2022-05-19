using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class CepMap : IEntityTypeConfiguration<CepEntity>
    {
        public void Configure(EntityTypeBuilder<CepEntity> builder)
        {
            builder.ToTable("Cep");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Cep);

            builder.Property(c => c.Logradouro)
                   .IsRequired()
                   .HasMaxLength(60);

            builder.Property(c => c.Numero)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.HasOne(c => c.Municipio)
                   .WithMany(m => m.Ceps);
        }
    }
}
