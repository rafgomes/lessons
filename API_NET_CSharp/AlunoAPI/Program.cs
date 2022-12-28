using AlunoAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection(); //redireciona para HTTPS

app.MapMethods(AlunoGet.Template, AlunoGet.Metodo, AlunoGet.Func);
app.MapMethods(IndexGet.Template, IndexGet.Metodo, IndexGet.Func);
app.MapMethods(AlunosGet.Template, AlunosGet.Metodo, AlunosGet.Func);



app.Run();

