using FluentValidation.AspNetCore;
using FluentValidation;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Converters;

using PetShelter.DataAccessLayer;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
        });
});

builder.Services.AddDbContext<PetShelterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetShelterConnection"),
        providerOptions =>
        {
            providerOptions.MigrationsAssembly("PetShelter.DataAccessLayer");
            providerOptions.EnableRetryOnFailure();
        }));
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IDonationRepository, DonationRepository>();
builder.Services.AddScoped<IFundService, FundService>();
builder.Services.AddScoped<IFundRepository, FundRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation(fv =>
{
    fv.DisableDataAnnotationsValidation = true;
}).AddFluentValidationClientsideAdapters();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();