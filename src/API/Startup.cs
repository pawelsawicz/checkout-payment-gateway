using API.Domain;
using API.Domain.Events;
using API.Services;
using EventFlow;
using EventFlow.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace API
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
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(o => o.RegisterValidatorsFromAssemblyContaining<Startup>());
            
            services.AddCors();
            services.AddSingleton(EventFlowOptions.New
                .AddEvents(typeof(PaymentSucceeded), typeof(PaymentFailed))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformationReadModel>()
                .RegisterServices(registration =>
                    registration.Register(x => AcquiringBankServiceFactory.Create(Configuration)))
                .CreateResolver());
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
                app.UseHttpsRedirection();
            }

            app.UseHttpMetrics();
            app.UseMetricServer();
            app.UseMvc();
            app.UseCors();
        }
    }
}
