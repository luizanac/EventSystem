using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infra.Map
{
	public class PointOfSaleMap : IEntityTypeConfiguration<PointOfSale>
	{
		public void Configure(EntityTypeBuilder<PointOfSale> builder)
		{
			builder.Property(ps => ps.Id);

			builder.Property(ps => ps.Cnpj)
				.HasColumnType("varchar(50)");
			
			builder.Property(ps => ps.Phone)
				.HasColumnType("varchar(50)");

			builder
				.HasMany(ps => ps.PointOfSaleEvents)
				.WithOne(pse => pse.PointOfSale);
		}
	}
}