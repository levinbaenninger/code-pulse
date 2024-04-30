using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoriesController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await _categoryRepository.GetAllAsync();

			// Map Domain Model to DTO
			var response = new List<CategoryDto>();
			foreach (var category in categories)
			{
				response.Add(new CategoryDto
				{
					Id = category.Id,
					Name = category.Name,
					UrlHandle = category.UrlHandle
				});
			}

			return Ok(response);
		}

		[HttpGet]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetCategory([FromRoute] Guid id)
		{
			var existingCategory = await _categoryRepository.GetByIdAsync(id);

			if (existingCategory == null)
			{
				return NotFound();
			}

			var response = new CategoryDto
			{
				Id = existingCategory.Id,
				Name = existingCategory.Name,
				UrlHandle = existingCategory.UrlHandle
			};

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
		{
			// Map DTO to Domain Model
			var category = new Category
			{
				Name = request.Name,
				UrlHandle = request.UrlHandle
			};

			// Add and save to Database
			await _categoryRepository.CreateAsync(category);

			// Map Domain Model to DTO
			var response = new CategoryDto
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			};

			return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, response);
		}

		[HttpPut]
		[Route("{id:guid}")]
		public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryRequestDto request)
		{
			// Map DTO to Domain Model
			var category = new Category
			{
				Id = id,
				Name = request.Name,
				UrlHandle = request.UrlHandle
			};

			category = await _categoryRepository.UpdateAsync(category);

			if (category == null)
			{
				return NotFound();
			}

			// Map Domain Model to DTO
			var response = new CategoryDto
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandle = category.UrlHandle
			};

			return Ok(response);
		}

		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
		{
			var categroy = await _categoryRepository.DeleteAsync(id);

			if (categroy == null)
			{
				return NotFound();
			}

			// Map Domain Model to DTO
			var response = new CategoryDto
			{
				Id = categroy.Id,
				Name = categroy.Name,
				UrlHandle = categroy.UrlHandle
			};

			return Ok(response);
		}
	}
}
