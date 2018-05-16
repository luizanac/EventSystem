using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infra.Map
{
	public class EventMap : IEntityTypeConfiguration<Event>
	{
		public void Configure(EntityTypeBuilder<Event> builder)
		{
			builder.Property(e => e.Id);

			builder.Property(e => e.Name)
				.IsRequired()
				.HasColumnType("varchar(50)");
			
			builder.Property(e => e.Photo)
				.HasColumnType("varchar(50)");

			builder.Property(e => e.StartDate)
				.IsRequired();
			
			builder.Property(e => e.EndDate)
				.IsRequired();

			builder
				.HasMany(e => e.PointOfSaleEvents)
				.WithOne(pse => pse.Event);

			builder
				.HasOne(e => e.EventAdministrator)
				.WithMany(ea => ea.Events);
		}
	}
}