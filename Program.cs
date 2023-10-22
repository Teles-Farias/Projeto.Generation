using blogpessoal.Data;
using blogpessoal.Model;
using blogpessoal.Service;
using blogpessoal.Service.Implements;
using blogpessoal.Validator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// Configuração do CORS
builder.Services.AddCors(options =>
          {
              options.AddPolicy(name: "MyPolicy",
              policy =>
              {
                  policy.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
              });
          });

// Add services to the container.
builder.Services.AddControllers();

// Conexão com o Banco de dados
var connectionString = builder.Configuration.
        GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// CONEXAO COM O BANCO DE DADOS - NEW
if (builder.Configuration["Enviroment:Start"] == "PROD")
{
    builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("secrets.json");

    var connectionStrings = builder.Configuration.GetConnectionString("ProdConnection");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
}
else
{
    var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
}

// registrar validações do banco de dados -NEW
builder.Services.AddTransient<IValidator<Aluno>, AlunoValidator>();

//Registrar as classes de serviço (SERVICE)
builder.Services.AddScoped<IAlunoService, AlunoService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Registrar o Swagger
builder.Services.AddSwaggerGen(options =>
{
    //Personalizar a Págna inicial do Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Projeto Cadastro de Alunos",
        Description = "Projeto Generation - ASP.NET Core 7 - Entity Framework",
        Contact = new OpenApiContact
        {
            Name = "Teles Bruno",
            Email = "telesbrunosf@gmail.com",
            Url = new Uri("https://github.com/Teles-Farias")
        }
    });
});

var app = builder.Build();

// Criar o Banco de dados e as tabelas Automaticamente
using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Swagger como Página Inicial (Home) na Nuvem
if (app.Environment.IsProduction())
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Escola - V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("MyPolicy");


app.UseAuthorization();

app.MapControllers();

app.Run();
