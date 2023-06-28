using CompositionRoot;

var builder = WebApplication.CreateBuilder(args);

//default dependencies
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//additional dependencies
builder.Services
    .AddCinemaRepositories()
    .AddTicketPriceEvaluators();

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
