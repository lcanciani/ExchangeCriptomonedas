using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using BackEndExchange.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BackEndExchange.Services;
using BackEndExchange.Model.PropositoGeneral;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BackEndExchange.Model;

namespace BackEndExchange
{
    public class Startup
    {

        private readonly string miCors = "MiCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackExchange", Version = "v1" });
            });
            services.AddCors(options => {
              options.AddPolicy(name: miCors, builder => {
                builder.WithOrigins("*");
                builder.WithHeaders("*");
                //builder.WithExposedHeaders("*");
                builder.WithMethods("*");
                
              });
              });

            //var connectionString = Configuration.GetConnectionString("DevConecction");
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
           
            //services.AddSingleton<ExchangeDBContext>();

            //Arranca JWT

            var appSettings = appSettingsSection.Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSettings.secreto);
            /*se intala Microsoft.AspNetCore.Authentication.JwtBearer de nuget: 
             es un standart para crear tokens web.
            System.identityModel.Tokens.jwt este tambien hay que  bajar de nugets
              
             */
            services.AddAuthentication(d => { d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(d =>
            {
                d.RequireHttpsMetadata = false;
                d.SaveToken = true;
                d.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(llave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
      services.AddScoped<ICompraService, CompraService>();
      services.AddScoped<IUserService, UserService>();

      services.AddScoped<IVentaService,VentaService>();
            //services.AddRazorPages();
            services.AddDbContext<ExchangeDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConecction")));
            //services.AddDbContext<ExchangeDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConecction")), ServiceLifetime.Singleton);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackExchange v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(miCors);
            app.UseAuthentication();

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
