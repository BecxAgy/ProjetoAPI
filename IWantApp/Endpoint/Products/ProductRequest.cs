namespace IWantApp.Endpoint.Products
{
    public record ProductRequest(string Name, string Description, bool HasStock,Guid CategoryId, decimal Price);
}