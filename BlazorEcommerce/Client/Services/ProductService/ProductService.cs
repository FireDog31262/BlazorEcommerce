namespace BlazorEcommerce.Client.Services.ProductService
{
	public class ProductService : IProductService
	{
		private readonly HttpClient _http;

		public ProductService(HttpClient http)
		{
			_http = http;
		}

		public List<Product> Products { get; set; } = new List<Product>();
		public string Message { get; set; } = "Loading Products...";

		public event Action ProductsChanged;

		public async Task<ServiceResponse<Product>> GetProduct(int productId)
		{
			var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
			return result;
			
		}

		public async Task GetProducts(string? categoryUrl = null)
		{
			var result = categoryUrl == null ?
				await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product") :
				await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/category/{categoryUrl}");

			if (result != null && result.Data != null)
			{
				Products = result.Data;
			}

			ProductsChanged.Invoke();
		}

		public async Task<List<string>> GetSearchSuggestions(string searchTerm)
		{
			var result = await _http
				.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchTerm}");

			return result.Data;
		}

		public async Task SearchProducts(string searchTerm)
		{
			var result = await _http
				.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{searchTerm}");

			if (result != null && result.Data != null)
			{
				Products = result.Data;
			}
			if (Products.Count == 0)
			{
				Message = "No Products Found.";
			}
			ProductsChanged?.Invoke();
		}
	}
}
