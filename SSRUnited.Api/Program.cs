using SSRUnited.Api;
using SSRUnited.Shared;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddShared(builder.Configuration);
builder.Services.AddApi(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
