using Cart.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet   ("api/carts", ()=>"Not implemented");
app.MapGet   ("api/carts/{id:guid}", (Guid id)=>"Not implemented");
app.MapPost  ("api/carts", (CartCreateDto cart)=>"Not implemented");
app.MapDelete("api/carts/{id:guid}", (Guid id) => "Not implemented");

app.MapPost  ("api/carts/{id:guid}/lines", (Guid id, LineCreateDto line) => "Not implemented");
app.MapPut   ("api/carts/{id:guid}/lines/{lineid:int}", (Guid id, int lineid, LineUpdateDto line)=>"Not implemented");
app.MapDelete("api/carts/{id:guid}/lines/{lineid:int}", (Guid id, int lineid) => "Not implemented");

app.Run();

public partial class Program { }