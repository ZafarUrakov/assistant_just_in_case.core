using assistant_just_in_case.core.Brokers.Storages;
using assistant_just_in_case.core.Services.Foundations.Converters;
using assistant_just_in_case.Services.Orchestrations.Telegrams;
using assistant_just_in_case.Services.Processings.Telegrams;
using doviz_bot.Brokers.Telegrams;
using doviz_bot.Services.Foundations.Telegrams;
using doviz_bot.Services.Foundations.TelegramUsers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add your services using the provided methods
AddBrokers(builder);
AddFoundationServices(builder);
AddProcessingServices(builder);
AddOrchestrationServices(builder);

var app = builder.Build();

//RegisterEventListeners(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

static void AddBrokers(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IStorageBroker, StorageBroker>();
    builder.Services.AddSingleton<ITelegramBroker, TelegramBroker>();
}

static void AddFoundationServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ITelegramService, TelegramService>();
    builder.Services.AddTransient<ITelegramUserService, TelegramUserService>();
    builder.Services.AddTransient<IConverterService, ConverterService>();
}

static void AddProcessingServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ITelegramUserProcessingService, TelegramUserProcessingService>();

}
static void AddOrchestrationServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ITelegramUserOrchestrationService, TelegramUserOrchestrationService>();
}

static void RegisterEventListeners(IApplicationBuilder app)
{
    app.ApplicationServices.GetRequiredService<ITelegramUserOrchestrationService>()
                .ListenTelegramUserMessage();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
