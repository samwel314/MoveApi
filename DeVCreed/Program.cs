using DeVCreed.Data;
using DeVCreed.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Connect TO db  // --- 

var Connstr = builder.Configuration["MyConnection"];

builder.Services.AddDbContext<AppDbContext>
    (o => o.UseSqlServer(Connstr));

// Add services to the container.
builder.Services.AddScoped<IGenresService, GenresService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();

builder.Services.AddAutoMapper(typeof(Program)); 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(); 

builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc(name: "v1", info: new OpenApiInfo
    {
        Version = "1",
        Title = "TestApi",
        Description = "MyFirstApi",
        TermsOfService = new Uri(uriString: "https://www.youtube.com/"),

        Contact = new OpenApiContact
        {
            Name = "zoka",
            Email = "samwel@mail.com",
            Url = new Uri(uriString: "https://www.youtube.com/")
        },
        License = new OpenApiLicense
        {
            Name = "License",   
            Url = new Uri(uriString : "https://www.youtube.com/") 
        }

    }) ;

    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization" , 
        Type = SecuritySchemeType.ApiKey ,
        Scheme = "Bearer" , 
        BearerFormat = "JWT" , 
        In = ParameterLocation.Header , 
        Description = "Enter Your Jwt Key " 
    }
    );

    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme , 
                    Id = "Bearer" , 

                } , 
                Name = "Bearer" , 
                In = ParameterLocation.Header 
            } , 
            new  List<string> ()
           
        }
    }); 
}); 
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Search -0   
app.UseCors(c =>
{
    c.AllowAnyHeader() 
    .AllowAnyMethod().WithOrigins().AllowAnyOrigin();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
