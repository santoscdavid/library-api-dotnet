using AutoMapper;
using FluentValidation.AspNetCore;
using LivrariaAPI.Data;
using LivrariaAPI.Data.AlguelRepo;
using LivrariaAPI.Data.EditoraRepo;
using LivrariaAPI.Data.LivroRepo;
using LivrariaAPI.Data.UsuarioRepo;
using LivrariaAPI.Services;
using LivrariaAPI.Services.Interface;
using LivrariaAPI.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace LivrariaAPI
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
            //var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            //var databaseUri = new Uri(databaseUrl);
            //var userInfo = databaseUri.UserInfo.Split(':');

            //var builder = new NpgsqlConnectionStringBuilder
            //{
            //    Host = databaseUri.Host,
            //    Port = databaseUri.Port,
            //    Username = userInfo[0],
            //    Password = userInfo[1],
            //    Database = databaseUri.LocalPath.TrimStart('/')
            //};

            //var connect = builder.ToString();

            services.AddDbContext<DataContext>(
                    context => context.UseSqlite(Configuration.GetConnectionString("Default")
                    )
            );

            services.AddCors();

            services.AddControllers()
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<EditoraValidator>())
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<UsuarioValidator>())
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<LivroValidator>())
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<AluguelValidator>())
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddTransient<IAuthService, AuthService>();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // services.AddScoped<IRepository, Repository>();
            services.AddScoped<IEditoraRepository, EditoraRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IAluguelRepository, AluguelRepository>();

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            var apiProviderDescription = services.BuildServiceProvider()
                                                 .GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
            {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(

                        description.GroupName,
                        new OpenApiInfo()
                        {
                            Title = "Livraria API",
                            Version = description.ApiVersion.ToString(),
                            TermsOfService = new Uri("http://SemPorEnquanto.com"),
                            Description = "API feita para auxiliar a locação de livros.",
                            License = new OpenApiLicense()
                            {
                                Name = "Livraria Lincese",
                                Url = new Uri("http://mit.com")
                            },
                            Contact = new OpenApiContact()
                            {
                                Name = "David Santos",
                                //Email = "david.csantos69@gmail.com",
                                Url = new Uri("https://github.com/DSantos69/API-DotNet")
                            }
                        });
                }

                var xmlCommentsFiles = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFiles);

                options.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiProviderDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger()
               .UseSwaggerUI(options =>
               {
                   foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                   {
                       options.SwaggerEndpoint(
                           $"/swagger/{description.GroupName}/swagger.json",
                           description.GroupName.ToUpperInvariant());
                   }
                   options.RoutePrefix = "";
               });



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
