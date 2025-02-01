using PetShop.Api.ApiConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddEndpointsApiExplorer();

builder
    .AddApiConfig()
    .AddCorsConfig()
    .AddDbContextConfig()
    .AddIdentityConfig()
    .AddCorsConfig()
    .AddSwaggerConfig();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
