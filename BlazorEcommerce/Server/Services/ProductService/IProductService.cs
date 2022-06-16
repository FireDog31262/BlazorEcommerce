namespace BlazorEcommerce.Server.Services.ProductService
{
	public interface IProductService
	{
		Task<ServiceResponse<List<Product>>> GetProductListAsync();
		Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
		Task<ServiceResponse<Product>> GetProductAsync(int productId);
		Task<ServiceResponse<List<Product>>> SearchProducts(string searchTerm);

		Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchTerm);
		Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
	}
}
