using BlogAPI.Application.Validators.Articles;
using BlogAPI.Insfrastructure.Filters;
using BlogAPI.Persistance;
using BlogAPI.Insfrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using BlogAPI.Insfrastructure.Services.Storage.Local;
using BlogAPI.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ArticleAddValidator>()).ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", opt =>
    {
        opt.TokenValidationParameters = new()
        {
            ValidateIssuer = true, //daðýtan api adresi
            ValidateAudience = true, //hangi origin kullanacak 
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });

builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

builder.Services.AddStorage<LocalStorage>();

builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.WithOrigins("https://localhost:4200","http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
