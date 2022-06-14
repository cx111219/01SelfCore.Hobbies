using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SelfCore.Hobbies.Domains;
using SelfCore.Hobbies.Services.Interceptors;
using System;
using System.IO;
using System.Text;

namespace SelfCore.Hobbies.WebApi
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
            services.AddDbContext<HobbyContext>(op =>
            op.UseMySql(this.Configuration.GetConnectionString("default"), ServerVersion.Parse("5.7.32-mysql"))
            .AddInterceptors(new SqlLogInterceptor()));

            services.AddCors(opt =>
            {
                opt.AddPolicy("cxcore", policy =>
                {
                    policy.WithOrigins("http://localhost:4000").AllowAnyHeader()
                    .AllowCredentials().AllowAnyMethod();
                });
            });
            services.AddControllers();

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new()
                    {
                        ValidAudience = Configuration["Authentication:Audience"],
                        ValidIssuer = Configuration["Authentication:Issuer"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                    };
                    opt.RequireHttpsMetadata = false; // 不适用https
                }
                );

            // 中间件有助于检测和诊断 Entity Framework Core 迁移错误
            services.AddDatabaseDeveloperPageExceptionFilter();
           
            services.AddSwaggerGen(c=>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SelfCore.Hobbies.WebApi", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SelfCore.Hobbies.Services.xml"), true);
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SelfCore.Hobbies.Domains.xml"), true);
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SelfCore.Hobbies.WebApi.xml"), true);

            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseCors();
            app.UseRouting();
            app.UseCors("cxcore");

            app.UseAuthentication(); // 认证
            app.UseAuthorization(); // 授权

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "api/{controller}/{action=Index}/{id?}");
            });
        }
    }
}
