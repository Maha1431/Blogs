using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using DataAccessLayer.Interfaces;
using AutoMapper;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.AutoMapperProfile;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataAccessLayer.ExceptionHandling;

namespace BlogWebApi
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
            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddDbContext<BlogApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BlogApplication")).UseLazyLoadingProxies());
            services.AddSwaggerGen(swagger =>
            {

                // To Enable authorization using Swagger (JWT)  
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using bearer scheme",

                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };
                swagger.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                         jwtSecurityScheme, Array.Empty<string>()
                        }
                    });
            });
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(profile: new UserProfile());
                mc.AddProfile(profile: new BlogProfile());
                mc.AddProfile(profile: new CommentProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN")),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blogs");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blogs"); });
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
