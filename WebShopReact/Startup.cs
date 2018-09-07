using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using WebShopMVC.Managers;
using WebShopMVC.Models;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using WebShopReact.Helpers;
using WebApi.Services;
using WebShop.Managers;
using Microsoft.AspNetCore.Http;
using Alexinea.Autofac.Extensions.DependencyInjection;

namespace WebShopReact
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddMvc();
			services.AddCors();
			services.AddAutoMapper();
			services.AddHttpContextAccessor();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			//services.AddScoped<IProductsManager, ProductsManager>();
			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterType<CustomerManager>().As<ICustomerManager>();
			builder.RegisterType<CustomerService>().As<ICustomerService>(); 
			builder.RegisterType<TokenHelper>().As<ITokenHelper>();
			builder.RegisterType<CartProductsManager>().As<ICartProductsManager>();
			builder.RegisterType<ProductsManager>().As<IProductsManager>();
			builder.RegisterType<WebShopDBContext>().Keyed<IWebShopDBContext>("WebShopDBContext");
			builder.RegisterType<WebShopDBContextInMemory>().Keyed<IWebShopDBContext>("WebShopDBContextInMemory");
			

			// configure strongly typed settings objects
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// configure jwt authentication
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes((string)(Configuration.GetValue(typeof(string), "Key")));
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.Events = new JwtBearerEvents
				{
					OnTokenValidated = context =>
					{
						var userService = context.HttpContext.RequestServices.GetRequiredService<ICustomerService>();
						var userId = int.Parse(context.Principal.Identity.Name);
						var user = userService.GetById(userId);
						if (user == null)
						{
							// return unauthorized if user no longer exists
							context.Fail("Unauthorized");
						}
						return Task.CompletedTask;
					}
				};
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			// configure DI for application services
			services.AddScoped<ICustomerService, CustomerService>();
			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.ContractResolver = new DefaultContractResolver();
			});
			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});

			builder.Populate(services);
			var container = builder.Build();
			return container.Resolve<IServiceProvider>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials());

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();
			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "api/{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}
	}
}
