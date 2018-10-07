using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnterprisePatterns.Api.Common.Infrastructure.Persistence.NHibernate;
using EnterprisePatterns.Api.Customers.Domain.Repository;
using EnterprisePatterns.Api.Customers.Infrastructure.Persistence.NHibernate.Repository;
using AutoMapper;
using EnterprisePatterns.Api.Common.Application;
using EnterprisePatterns.Api.Customers.Application.Assembler;

namespace EnterprisePatterns.Api
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
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton(new SessionFactory(Environment.GetEnvironmentVariable("MYSQL_STRCON_HILO_DEMO")));
            var serviceProvider = services.BuildServiceProvider();
            var mapper = serviceProvider.GetService<IMapper>();
            services.AddSingleton(new CustomerAssembler(mapper));
            services.AddScoped<IUnitOfWork, UnitOfWorkNHibernate>();
            services.AddTransient<ICustomerRepository, CustomerNHibernateRepository>((ctx) =>
            {
                IUnitOfWork unitOfWork = ctx.GetService<IUnitOfWork>();
                return new CustomerNHibernateRepository((UnitOfWorkNHibernate)unitOfWork);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
