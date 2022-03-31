using Cart.Dtos;
using Cart.Models;

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

var carts = new Dictionary<Guid, ShoppingCart>();

app.MapGet   ("api/carts", () => carts.Values.Select(cart => CartDto.FromCart(cart)));
app.MapGet   ("api/carts/{id:guid}", (Guid id) 
    => carts.ContainsKey(id) 
        ? Results.Ok(CartDto.FromCart(carts[id])) 
        : Results.NotFound("No cart with this id")
);
app.MapPost  ("api/carts", (CartCreateDto cart) => {
    var newCart = new ShoppingCart();

    carts.Add(newCart.Id, newCart);
    return Results.Created($"api/carts/{newCart.Id}", CartDto.FromCart(newCart));
});
app.MapDelete("api/carts/{id:guid}", (Guid id) => {
    if (carts.ContainsKey(id))
    {
        carts.Remove(id);
        return Results.NoContent();
    }
    return Results.NotFound("No cart with this id");
});

app.MapPost  ("api/carts/{id:guid}/lines", (Guid id, LineCreateDto line) => "Not implemented");
app.MapPut   ("api/carts/{id:guid}/lines/{lineid:int}", (Guid id, int lineid, LineUpdateDto line)=>"Not implemented");
app.MapDelete("api/carts/{id:guid}/lines/{lineid:int}", (Guid id, int lineid) => "Not implemented");

app.Run();

public partial class Program { }