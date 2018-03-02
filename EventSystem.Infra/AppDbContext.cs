using EventSystem.Domain.Entities;
using EventSystem.Shared.Entities;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;

namespace EventSystem.Infra
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{}
		
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Ignore<Notifiable>();
			modelBuilder.Ignore<Notification>();

		}

		public DbSet<User> Users { get; set; }
		public DbSet<Administrator> Administrators { get; set; }
		public DbSet<PointOfSale> PointOfSales { get; set; }
		public DbSet<EventAdministrator> EventAdministrators { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<PointOfSaleEvent> PointOfSaleEvents { get; set; }
		public DbSet<Event> Events { get; set; }
	}
}