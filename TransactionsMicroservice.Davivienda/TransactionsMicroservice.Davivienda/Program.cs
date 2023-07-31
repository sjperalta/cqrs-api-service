using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TransactionsMicroservice.Davivienda;
using TransactionsMicroservice.Davivienda.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageProducer, TransactionPublisher>();
builder.Services.AddScoped<IMessageConsumer, TransactionConsumer>();
builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var bankContext = scope.ServiceProvider.GetRequiredService<BankDbContext>();
        bankContext.Database.EnsureCreated();
    }

    using (var scope = app.Services.CreateScope())
    {
        var messageConsumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
        messageConsumer.Consume();
    }
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
