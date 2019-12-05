using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniversityLecture.Repo;
using UniversityLecture.WEB.Profiles;
using UniversityLecture.WEB.Services;
using UniversityLecture.WEB.Interfaces;


namespace UniversityLecture.Web
{
    public class StartupTest
    {
        public StartupTest(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            new ConfigurationBuilder().Build();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new SqlModule(Configuration.GetConnectionString("UL")));
            builder.Register(context =>
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<LectureHallProfile>();
                    cfg.AddProfile<LecturerProfile>();
                    cfg.AddProfile<SubjectProfile>();
                    cfg.AddProfile<ReservationProfile>();

                }).CreateMapper();
            }).As<IMapper>().SingleInstance();
            builder.RegisterType<AuthenticateService>().As<IAuthenticate>();
        }
    }
}
