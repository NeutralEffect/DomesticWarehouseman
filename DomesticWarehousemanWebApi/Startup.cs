using DomesticWarehousemanWebApi.Data;
using DomesticWarehousemanWebApi.DTO.Account;
using DomesticWarehousemanWebApi.Helpers;
using DomesticWarehousemanWebApi.Middleware;
using DomesticWarehousemanWebApi.Security.RequirementHandlers;
using DomesticWarehousemanWebApi.Security.Requirements;
using DomesticWarehousemanWebApi.Services;
using DomesticWarehousemanWebApi.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi
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
			// Section: Misc
			services
				.AddControllers()
				.AddFluentValidation();

			services.AddHttpContextAccessor();

			services.AddScoped<ErrorHandlingMiddleware>();

			var applicationLimits = new ApplicationLimits();

			Configuration
				.GetSection("ApplicationLimits")
				.Bind(applicationLimits);

			services.AddSingleton(applicationLimits);

			// Section: Authentication
			var authenticationSettings = new AuthenticationSettings();

			Configuration
				.GetSection("Authentication")
				.Bind(authenticationSettings);

			services.AddSingleton(authenticationSettings);

			services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();

			services.AddAuthentication
			(
				options =>
				{
					options.DefaultAuthenticateScheme = "Bearer";
					options.DefaultScheme = "Bearer";
					options.DefaultChallengeScheme = "Bearer";
				}
			)
			.AddJwtBearer
			(
				configuration =>
				{
					configuration.RequireHttpsMetadata = false;
					configuration.SaveToken = true;
					configuration.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = authenticationSettings.JwtIssuer,
						ValidAudience = authenticationSettings.JwtIssuer, //FIXME: Might become problematic
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
					};
				}
			);
			
			// Section: Authorization
			services.AddScoped<IAuthorizationHandler, StorageEditorRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, StorageCommentatorRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, StorageViewerRequirementHandler>();
			services.AddScoped<IAuthorizationHandler, AccountOwnerRequirementHandler>();

			services.AddAuthorization
			(
				options =>
				{
					options.AddPolicy
					(
						"StorageEditor",
						policy =>
						{
							policy.Requirements.Add(new StorageEditorRequirement());
						}
					);

					options.AddPolicy
					(
						"StorageCommentator",
						policy =>
						{
							policy.Requirements.Add(new StorageCommentatorRequirement());
						}
					);

					options.AddPolicy
					(
						"StorageViewer",
						policy =>
						{
							policy.Requirements.Add(new StorageViewerRequirement());
						}
					);

					options.AddPolicy
					(
						"AccountOwner",
						policy =>
						{
							policy.Requirements.Add(new AccountOwnerRequirement());
						}
					);
				}
			);

			// Section: Data
			services.AddDbContext<DomesticWarehousemanDbContext>
			(
				options => options.UseSqlServer("Name=ConnectionStrings:DomesticWarehousemanDbDevelopment")
			);

			services
				.AddScoped<IAccountsService, AccountsService>()
				.AddScoped<IEssentialListService, EssentialListService>()
				.AddScoped<IItemsService, ItemsService>()
				.AddScoped<IResourcesService, ResourcesService>()
				.AddScoped<IShoppingListsService, ShoppingListsService>()
				.AddScoped<IStorageMembersService, StorageMembersService>()
				.AddScoped<IStoragesService, StoragesService>();

			services.AddScoped<IValidator<AccountRegisterDto>, AccountRegisterDtoValidator>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHttpsRedirection();
			}

			app.UseMiddleware<ErrorHandlingMiddleware>();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			//FIXME: Disabled for development
			//app.UseHttpsRedirection();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
