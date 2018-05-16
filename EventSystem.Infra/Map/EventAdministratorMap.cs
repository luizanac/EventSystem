using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infra.Map
{
	public class EventAdministratorMap : IEntityTypeConfiguration<EventAdministrator>
	{
		public void Configure(EntityTypeBuilder<EventAdministrator> builder)
		{
			builder
				.HasOne(ea => ea.Administrator)
				.WithMany(a => a.EventAdministrators);

			builder
				.HasMany(ea => ea.Events)
				.WithOne(e => e.EventAdministrator);
		}
	}
}