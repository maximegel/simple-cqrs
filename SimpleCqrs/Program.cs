using System.Reflection;
using SimpleCqrs.Inventory.Persistence;
using SimpleCqrs.Inventory.Persistence.Di;
using SimpleCqrs.Inventory.Projection.Di;
using SimpleCqrs.Shared.Infra.Di;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInventoryPersistence()
    .AddInventoryProjection()
    .AddMessaging(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SupportNonNullableReferenceTypes();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InventorySqlContext>();
    dbContext.Database.EnsureCreated();
}

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