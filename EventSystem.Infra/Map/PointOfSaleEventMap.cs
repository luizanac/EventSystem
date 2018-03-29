using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infra.Map
{
	public class PointOfSaleEventMap : IEntityTypeConfiguration<PointOfSaleEvent>
	{
		public void Configure(EntityTypeBuilder<PointOfSaleEvent> builder)
		{
			builder.Property(pse => pse.Id);

			builder.HasOne(pse => pse.Event)
				.WithMany(e => e.PointOfSaleEvents);

			builder.HasOne(pse => pse.PointOfSale)
				.WithMany(e => e.PointOfSaleEvents);

			builder.HasMany(pse => pse.Payments)
				.WithOne(p => p.PointOfSaleEvent);
		}
	}
}