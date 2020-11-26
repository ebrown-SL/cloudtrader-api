using AutoMapper;
using CloudTrader.Api.Data;
using CloudTrader.Api.Exceptions;
using CloudTrader.Api.Service.Helpers;
using CloudTrader.Api.Service.Interfaces;
using CloudTrader.Api.Service.Models;
using CloudTrader.Api.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace CloudTrader.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<JwtTokenOptions>()
                .Bind(Configuration.GetSection(nameof(JwtTokenOptions)))
                .ValidateDataAnnotations();

            services.AddMvc(options =>
            {
                options.Filters.Add(new GlobalExceptionFilter());
            });

            services.AddAutoMapper(typeof(UserProfile));

            services.AddControllers();

            services.AddDbContext<UserContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITraderApiClient, TraderApiClient>();

            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IPasswordUtils, PasswordUtils>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IMineApiService, MineApiService>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userId = Guid.Parse(context.Principal.Identity.Name);
                        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                        var user = await userRepository.GetUser(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                    }
                };
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;

                var key = Encoding.ASCII.GetBytes(Configuration.GetSection(nameof(JwtTokenOptions)).Get<JwtTokenOptions>().Key);
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CloudTrader API",
                    Description = "Endpoints for the CloudTrader API service"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloudTrader API");
            });

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}