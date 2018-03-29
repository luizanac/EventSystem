using System.Text;
using AutoMapper;
using EventSystem.Domain.Handlers;
using EventSystem.Domain.Repositories;
using EventSystem.Domain.Services;
using EventSystem.Infra;
using EventSystem.Infra.Repositories;
using EventSystem.Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;


namespace EventSystem.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
			});
            
			services.AddAuthorization(options =>
			{
				options.AddPolicy("PointOfSale", policy => policy.RequireClaim("PointOfSale").RequireClaim("Administrator"));
				options.AddPolicy("EventAdministrator", policy => policy.RequireClaim("EventAdministrator").RequireClaim("Administrator"));
			});
            
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(cfg =>
			{
				cfg.RequireHttpsMetadata = false;
				cfg.SaveToken = true;
				
				cfg.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidIssuer = Configuration["Token:Issuer"],
					ValidAudience =  Configuration["Token:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]))
				};
			});
			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Eventos API", Version = "v1" });
			});

			services.AddAutoMapper();
			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			});
			
			#region Dependency Injection

			#region Repositories

			services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IEventAdministratorRepository, EventAdministratorRepository>();
			services.AddTransient<IPointOfSaleRepository, PointOfSaleRepository>();
			services.AddTransient<IEventRepository, EventRepository>();
			services.AddTransient<IBoxOfficeRepository, BoxOfficeRepository>();
			
			

			#endregion

			#region Services

			services.AddTransient<IJwtService, JwtService>();

			#endregion

			#region Handlers

			services.AddTransient<UserHandler, UserHandler>();
			services.AddTransient<EventAdministratorHandler, EventAdministratorHandler>();
			services.AddTransient<PointOfSaleHandler, PointOfSaleHandler>();
			services.AddTransient<EventHandler, EventHandler>();
			services.AddTransient<BoxOfficeHandler, BoxOfficeHandler>();

			#endregion
			

			services.AddLogging();

			#endregion
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext dbContext)
		{
			app.UseCors(builder => 
				builder
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowAnyOrigin()
					.AllowCredentials());
			
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eventos API V1");
			});

			app.UseMvc();
			/*var admin = new Administrator("Luiz Anacleto", "luizanacletozuchinali@gmail.com", "12345678") {IsActive = true};
			dbContext.Administrators.Add(admin);
			dbContext.SaveChanges();*/
		}
	}
}