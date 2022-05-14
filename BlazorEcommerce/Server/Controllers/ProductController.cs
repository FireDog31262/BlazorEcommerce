﻿using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private DataContext _context;

		public ProductController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetProducts()
		{
			var result = await _context.Products.ToListAsync();
			return Ok(result);
		}
	}
}
