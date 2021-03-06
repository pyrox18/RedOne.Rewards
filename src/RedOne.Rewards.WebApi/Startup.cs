using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RedOne.Rewards.Application.Dtos;
using RedOne.Rewards.Application.Interfaces;
using RedOne.Rewards.Application.Services;
using RedOne.Rewards.Domain.Interfaces;
using RedOne.Rewards.Infrastructure.Factories;
using RedOne.Rewards.Infrastructure.Interfaces;
using RedOne.Rewards.Infrastructure.Repositories;
using RedOne.Rewards.Infrastructure.Services;
using RedOne.Rewards.WebApi.Authentication;
using RedOne.Rewards.WebApi.Configuration;
using RedOne.Rewards.WebApi.Middleware;
using System.Text;

namespace RedOne.Rewards.WebApi
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
            // Repositories
            services.AddScoped<IAdminUserRepository, AdminUserRepository>();
            services.AddScoped<IConsumerUserRepository, ConsumerUserRepository>();
            services.AddScoped<IMemberLevelRepository, MemberLevelRepository>();
            services.AddScoped<IRewardRepository, RewardRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<IUsageRepository, UsageRepository>();
            services.AddScoped<IRewardRedemptionRepository, RewardRedemptionRepository>();

            // Application services
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IConsumerUserService, ConsumerUserService>();
            services.AddScoped<IMemberLevelService, MemberLevelService>();
            services.AddScoped<IRewardService, RewardService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IUsageService, UsageService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();

            // Factories
            services.AddScoped<IMockBackgroundTaskFactory, MockBackgroundTaskFactory>();

            // Hosted services
            services.AddHostedService<BackgroundTaskHostedService>();

            // App settings
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // JWT authentication
            services.AddScoped<ITokenService, JwtTokenService>();
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
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

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers()
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<CreateRewardDtoValidator>();
                    x.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RedOne.Rewards.WebApi", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RedOne.Rewards.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
