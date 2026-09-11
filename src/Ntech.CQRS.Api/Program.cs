using Microsoft.EntityFrameworkCore;
using Ntech.CQRS.Application;
using Ntech.CQRS.Core.Context;
using Ntech.CQRS.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseInMemoryDatabase("SchoolDb"));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ntech.CQRS API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SchoolContext>();
    if (!context.Students.Any())
    {
        context.Students.AddRange(
            new Student { Name = "Nguyen Van A", Age = 20 },
            new Student { Name = "Tran Thi B", Age = 20 },
            new Student { Name = "Le Van C", Age = 22 }
        );
        context.SaveChanges();
    }
}

app.Run();
