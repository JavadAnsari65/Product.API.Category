using Product.API.Category.Application;
using Product.API.Category.Infrastructure.Configuration;
using Product.API.Category.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add DbContext
builder.Services.AddDbContext<CategoryDbContext>();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(CustomMap));

//Add Services
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CRUDService>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
