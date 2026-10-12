using UnitConverterApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<UnitDataProvider>();

builder.Services.AddScoped<IConversionService, ConversionService>();
builder.Services.AddScoped<ITemperatureConversionService, TemperatureConversionService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();
