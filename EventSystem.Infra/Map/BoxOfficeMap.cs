using EventSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventSystem.Infra.Map
{
	public class BoxOfficeMap : IEntityTypeConfiguration<BoxOffice>
	{
		public void Configure(EntityTypeBuilder<BoxOffice> builder)
		{
			builder.Property(bo => bo.Id);
		}
	}
}