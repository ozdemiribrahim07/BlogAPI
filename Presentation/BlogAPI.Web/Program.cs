using BlogAPI.Application.Validators.Articles;
using BlogAPI.Insfrastructure.Filters;
using BlogAPI.Persistance;
using BlogAPI.Insfrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ArticleAddValidator>()).ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
