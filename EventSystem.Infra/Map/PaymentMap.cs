using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infra.Map
{
	public class PaymentMap : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder.Property(p => p.Id);

			builder.Property(p => p.Value)
				.IsRequired();

			builder.Property(p => p.PaymenteDate)
				.IsRequired();

			builder
				.HasOne(p => p.PointOfSaleEvent)
				.WithMany(pse => pse.Payments);

			builder
				.HasOne(p => p.Client)
				.WithMany(c => c.Payments);
		}
	}
}