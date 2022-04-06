using System.Reflection;
using System.Text.Json.Serialization;
using SimpleCqrs.Inventory.App.Di;
using SimpleCqrs.Inventory.Persistence;
using SimpleCqrs.Inventory.Persistence.Di;
using SimpleCqrs.Inventory.ReadModel.Di;
using SimpleCqrs.Shared.Infra.Di;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInventoryApp()
    .AddInventoryPersistence()
    .AddInventoryReadModel()
    .AddMessaging(Assembly.GetExecutingAssembly());

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull;
    });;
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