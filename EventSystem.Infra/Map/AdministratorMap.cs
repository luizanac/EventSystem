using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infra.Map
{
	public class AdministratorMap : IEntityTypeConfiguration<Administrator>
	{
		public void Configure(EntityTypeBuilder<Administrator> builder)
		{

			builder
				.HasMany(a => a.EventAdministrators)
				.WithOne(ea => ea.Administrator);
		}
	}
}