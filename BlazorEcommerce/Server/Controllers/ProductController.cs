﻿using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
		{

			var result = await _productService.GetProductListAsync();
			return Ok(result);

		}

		[HttpGet("{productId}")]
		public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
		{

			var result = await _productService.GetProductAsync(productId);
			return Ok(result);

		}

		[HttpGet("category/{categoryUrl}")]
		public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductByCategory(string categoryUrl)
		{

			var result = await _productService.GetProductsByCategory(categoryUrl);
			return Ok(result);

		}

		[HttpGet("search/{searchTerm}")]
		public async Task<ActionResult<ServiceResponse<List<Product>>>> SearchProducts(string searchTerm)
		{

			var result = await _productService.SearchProducts(searchTerm);
			return Ok(result);

		}

		[HttpGet("searchsuggestions/{searchTerm}")]
		public async Task<ActionResult<ServiceResponse<List<Product>>>> GetSearchSuggestions(string searchTerm)
		{

			var result = await _productService.GetProductSearchSuggestions(searchTerm);
			return Ok(result);

		}

		[HttpGet("featured")]
		public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProducts()
		{

			var result = await _productService.GetFeaturedProducts();
			return Ok(result);

		}
	}
}
