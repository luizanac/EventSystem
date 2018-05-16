using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infra.Map
{
	public class UserMap : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.Id);

			builder.Property(u => u.Name)
				.IsRequired()
				.HasColumnType("varchar(50)");
			
			builder.Property(u => u.Email)
				.IsRequired()
				.HasColumnType("varchar(50)");
			
			builder.Property(u => u.Password)
				.IsRequired()
				.HasColumnType("varchar(64)");

			builder.Property(u => u.IsActive)
				.HasDefaultValue(true);
			
			builder.Property(u => u.IsDisabled)
				.HasDefaultValue(false);
		}
	}
}