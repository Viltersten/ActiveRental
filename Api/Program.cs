using Api.Services.Implementations;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager config = builder.Configuration;

services.Configure<BillingConfig>(config.GetSection(BillingConfig.SectionKey));

services.AddScoped<IAdminService, AdminService>();
services.AddScoped<IBillingService, BillingService>();

services.AddDbContext<AppDbContext>(Delegates.ContextOptions(config));
// services.AddDbContext<AppDbContext>(Delegates.ContextOptions(config, true));

services.AddControllers()
    .AddJsonOptions(a => a.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddFluentValidation(a =>
    {
        a.RegisterValidatorsFromAssemblyContaining<Program>();
        a.ValidatorOptions.DefaultClassLevelCascadeMode = CascadeMode.Continue;
        a.ValidatorOptions.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        a.ImplicitlyValidateChildProperties = true;
        a.DisableDataAnnotationsValidation = true;
    });

// services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


WebApplication app = builder.Build();
Init.Migrate(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();