//===========================
// Copyright (c) Tarteeb LLC
// Manage Your Money Easy
//===========================

using System;
using System.Text.Json.Serialization;
using assistant_just_in_case.core.Brokers.Storages;
using assistant_just_in_case.core.Services.Foundations.Converters;
using assistant_just_in_case.Services.Foundations.Telegrams;
using assistant_just_in_case.Services.Foundations.TelegramUsers;
using assistant_just_in_case.Services.Orchestrations.Telegrams;
using assistant_just_in_case.Services.Processings.Telegrams;
using doviz_bot.Brokers.Telegrams;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace assistant_just_in_case
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication1", Version = "v1" });
            });
            AddBrokers(services);
            AddFoundationServices(services);
            AddProcessingServices(services);
            AddOrchestrationServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction() || env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
                RegisterEventListeners(app);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private static void RegisterEventListeners(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var telegramService = scope.ServiceProvider.GetRequiredService<ITelegramUserOrchestrationService>();

                telegramService.ListenTelegramUserMessage();

            }
        }

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddSingleton<ITelegramBroker, TelegramBroker>();
        }

        private static void AddFoundationServices(IServiceCollection services)
        {
            services.AddTransient<ITelegramUserService, TelegramUserService>();
            services.AddTransient<IConverterService, ConverterService>();
            services.AddTransient<ITelegramService, TelegramService>();
        }

        private static void AddProcessingServices(IServiceCollection services)
        {
            services.AddTransient<ITelegramUserProcessingService, TelegramUserProcessingService>();
        }

        private static void AddOrchestrationServices(IServiceCollection services)
        {
            services.AddTransient<ITelegramUserOrchestrationService, TelegramUserOrchestrationService>();
        }
    }
}
