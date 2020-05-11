using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Server.IISIntegration;

namespace WebApp
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
			services.AddControllersWithViews();

			// Basic setup -- services.AddSwaggerGen( c=> c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "V1" }));

			// More elaborate Swagger setup
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { 
					Version = "v1",
					Title = "ToDo API",
					Description = "A simple example ASP.NET Core Web API",
					TermsOfService = new Uri("https://example.com/terms"),
					Contact = new OpenApiContact
					{
						Name = "Shayne Boyer",
						Email = string.Empty,
						Url = new Uri("https://twitter.com/spboyer"),
					},
					License = new OpenApiLicense
					{
						Name = "Use under LICX",
						Url = new Uri("https://example.com/license"),
					}
				});

			});

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});

			services.AddAuthentication(IISDefaults.AuthenticationScheme);

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// See MS Swagger article at:  https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio

			// enable Swagger middleware:
			app.UseSwagger();

			// Enable middleware to sere swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			}
			);


			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseAuthorization();  // Enable Windows authentication.

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
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
