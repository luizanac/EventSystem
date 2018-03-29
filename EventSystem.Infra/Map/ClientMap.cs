using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infra.Map
{
	public class ClientMap : IEntityTypeConfiguration<Client>
	{
		public void Configure(EntityTypeBuilder<Client> builder)
		{
			builder.Property(c => c.Id);

			builder.Property(c => c.Rg)
				.IsRequired()
				.HasColumnType("varchar(50)");

			builder.Property(c => c.Name)
				.IsRequired()
				.HasColumnType("varchar(50)");

			builder.Property(c => c.Email)
				.IsRequired()
				.HasColumnType("varchar(50)");

			builder.Property(c => c.Phone)
				.IsRequired()
				.HasColumnType("varchar(50)");

			builder.Property(c => c.Balance)
				.HasDefaultValue(0);
		}
	}
}