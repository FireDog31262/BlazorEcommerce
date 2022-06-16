namespace BlazorEcommerce.Server.Services.ProductService
{
	public class ProductService : IProductService
	{
		private readonly DataContext _context;
		public ProductService(DataContext context)
		{
			_context = context;
		}

		public async Task<ServiceResponse<List<Product>>> GetProductListAsync()
		{
			var response = new ServiceResponse<List<Product>>
			{
				Data = await _context.Products.Include(p => p.Variants).ToListAsync()
			};
			return response;
		}

		public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
		{
			var response = new ServiceResponse<List<Product>>
			{
				Data = await _context.Products.Include(p => p.Variants).Where(p => p.Featured).ToListAsync()
			};
			return response;
		}

		public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
		{
			var response = new ServiceResponse<Product>();
			var product = await _context.Products
				.Include(p => p.Variants)
				.ThenInclude(v => v.ProductType)
				.FirstOrDefaultAsync(p => p.Id == productId);

			if (product != null)
			{
				response.Data = product;
			}
			else
			{
				response.Success = false;
				response.Message = "Sorry, but this product does not exist.";
			}

			return response;
		}

		public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
		{
			var response = new ServiceResponse<List<Product>>
			{
				Data = await _context.Products
							.Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
							.Include(p => p.Variants)
							.ToListAsync()
			};

			return response;
		}

		public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchTerm)
		{
			var response = new ServiceResponse<List<Product>>
			{
				Data = await FindProductsByTerm(searchTerm)
			};

			return response;
		}

		private async Task<List<Product>> FindProductsByTerm(string searchTerm)
		{
			return await _context.Products
														.Where(p => p.Title.ToLower().Contains(searchTerm.ToLower())
														|| p.Description.ToLower().Contains(searchTerm.ToLower()))
														.Include(p => p.Variants)
														.ToListAsync();
		}

		public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchTerm)
		{
			var products = await FindProductsByTerm(searchTerm);
			List<string> result = new List<string>();

			foreach (var product in products)
			{
				if (product.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
				{
					result.Add(product.Title);
				}

				if (product.Description != null)
				{
					var puctuation = product.Description.Where(char.IsPunctuation)
						.Distinct().ToArray();
					var words = product.Description.Split()
						.Select(s => s.Trim(puctuation));

					foreach(var word in words)
					{
						if (word.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
							&& !result.Contains(word))
						{
							result.Add(word);
						}
					}
				}
			}

			return new ServiceResponse<List<string>>
			{
				Data = result
			};
		}
	}
}

