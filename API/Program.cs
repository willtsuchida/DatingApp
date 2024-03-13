using API.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration); // Application Services - clean code
builder.Services.AddIdentityServices(builder.Configuration);

//adicionando o DataContext como um Service
/*
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
FOI PARA O SERVICE */



//Habilitando CORS
/*
builder.Services.AddCors(); // Continuar no http pipeline, usar na ordem correta
FOI PARA O SERVICE */

/*
builder.Services.AddScoped<ITokenService, TokenService>(); //AddTransient, AddScoped, Add Singleton
FOI PARA O SERVICE */


//Service pro token

/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, //se nao qlqr token criado por fora via ser aceito
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])
                ),
            ValidateIssuer = false,
            ValidateAudience = false
        }; //Adicioanr o middleware to CORS
    });
 FOI PRO IDENTITY SERVICES*/

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer(); REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER
// builder.Services.AddSwaggerGen(); REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()) REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER
// {
//     app.UseSwagger(); REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER
//     app.UseSwaggerUI(); REMOVIDO PQ NAO ESTAMOS USANDO SWAGGER
// }

app.UseHttpsRedirection();

//Continuando configuracao do CORS
app.UseCors(
    corsPolicyBuilder =>
             corsPolicyBuilder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("https://localhost:4200")
);

app.UseAuthentication(); // do u have a valid token?
app.UseAuthorization(); // what are u allowed to do

app.MapControllers();

app.Run();
