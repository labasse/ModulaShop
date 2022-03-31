using Cart.Dtos;
using Cart.Models;
using Cart.Services;

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

app.MapPost  ("api/carts/{id:guid}/lines", async (Guid id, LineCreateDto lineDto) =>
{
    if(!carts.ContainsKey(id))
    {
        return Results.UnprocessableEntity("No cart with this id");
    }
    else if(lineDto.Qty <= 0)
    {
        return Results.UnprocessableEntity("Quantity must be strictly bigger than 0");
    }
    var cart = carts[id];
    var index = cart.GetLineIndex(lineDto.ProductId);

    if (index < 0) {
        using var httpClient = new HttpClient();
        var catalogApi = new CatalogApiProxy("https://localhost:7247", httpClient);

        try
        {
            var product = await catalogApi.GetProductAsync(lineDto.ProductId);

            index = cart.Lines.Count();
            carts[id].AddItem(new ShoppingCart.Item(lineDto.ProductId, product.Name, (decimal)product.Price)
            {
                Qty = lineDto.Qty
            });
        }
        catch (ApiException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    else
    {
        cart.Lines.ElementAt(index).Qty += lineDto.Qty;
    }
    return Results.Created(
        $"api/carts/{id}/lines/{index}",
        LineDto.FromCartItem(index, cart.Lines.ElementAt(index))
    );
});

app.MapPut   ("api/carts/{id:guid}/lines/{lineid:int}", (Guid id, int lineid, LineUpdateDto line)=>"Not implemented");
app.MapDelete("api/carts/{id:guid}/lines/{lineid:int}", (Guid id, int lineid) => "Not implemented");

app.Run();

public partial class Program { }