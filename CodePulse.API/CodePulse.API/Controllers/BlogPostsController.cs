﻿using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostsController : ControllerBase
	{
		private readonly IBlogPostRespository _blogPostRepository;

		public BlogPostsController(IBlogPostRespository blogPostRepository)
		{
			_blogPostRepository = blogPostRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetBlogPosts()
		{
			var blogPosts = await _blogPostRepository.GetAllAsync();

			// Map Domain Model to DTO
			var response = new List<BlogPostDto>();
			foreach (var blogPost in blogPosts)
			{
				response.Add(new BlogPostDto
				{
					Id = blogPost.Id,
					Title = blogPost.Title,
					Description = blogPost.Description,
					Content = blogPost.Content,
					FeaturedImageUrl = blogPost.FeaturedImageUrl,
					UrlHandle = blogPost.UrlHandle,
					DatePublished = blogPost.DatePublished,
					Author = blogPost.Author,
					IsVisible = blogPost.IsVisible
				});
			}

			return Ok(response);
		}

		[HttpGet]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetBlogPost([FromRoute] Guid id)
		{
			var existingCategory = await _blogPostRepository.GetByIdAsync(id);

			if (existingCategory == null)
			{
				return NotFound();
			}

			// Map Domain Model to DTO
			var response = new BlogPostDto
			{
				Id = existingCategory.Id,
				Title = existingCategory.Title,
				Description = existingCategory.Description,
				Content = existingCategory.Content,
				FeaturedImageUrl = existingCategory.FeaturedImageUrl,
				UrlHandle = existingCategory.UrlHandle,
				DatePublished = existingCategory.DatePublished,
				Author = existingCategory.Author,
				IsVisible = existingCategory.IsVisible
			};

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDto request)
		{
			// Map DTO to Domain Model
			var blogPost = new BlogPost
			{
				Title = request.Title,
				Description = request.Description,
				Content = request.Content,
				FeaturedImageUrl = request.FeaturedImageUrl,
				UrlHandle = request.UrlHandle,
				DatePublished = request.DatePublished,
				Author = request.Author,
				IsVisible = request.IsVisible
			};

			await _blogPostRepository.CreateAsync(blogPost);

			// Map Domain Model to DTO
			var response = new BlogPostDto
			{
				Id = blogPost.Id,
				Title = blogPost.Title,
				Description = blogPost.Description,
				Content = blogPost.Content,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				UrlHandle = blogPost.UrlHandle,
				DatePublished = blogPost.DatePublished,
				Author = blogPost.Author,
				IsVisible = blogPost.IsVisible
			};

			return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.Id }, response);
		}

		[HttpPut]
		[Route("{id:guid}")]
		public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, UpdateBlogPostRequestDto request)
		{
			// Map DTO to Domain Model
			var blogPost = new BlogPost
			{
				Id = id,
				Title = request.Title,
				Description = request.Description,
				Content = request.Content,
				FeaturedImageUrl = request.FeaturedImageUrl,
				UrlHandle = request.UrlHandle,
				DatePublished = request.DatePublished,
				Author = request.Author,
				IsVisible = request.IsVisible
			};

			blogPost = await _blogPostRepository.UpdateAsync(blogPost);

			if (blogPost == null)
			{
				return NotFound();
			}

			// Map Domain Model to DTO
			var response = new BlogPostDto
			{
				Id = blogPost.Id,
				Title = blogPost.Title,
				Description = blogPost.Description,
				Content = blogPost.Content,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				UrlHandle = blogPost.UrlHandle,
				DatePublished = blogPost.DatePublished,
				Author = blogPost.Author,
				IsVisible = blogPost.IsVisible
			};

			return Ok(response);
		}

		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
		{
			var blogPost = await _blogPostRepository.DeleteAsync(id);

			if (blogPost == null)
			{
				return NotFound();
			}

			// Map Domain Model to DTO
			var response = new BlogPostDto
			{
				Id = blogPost.Id,
				Title = blogPost.Title,
				Description = blogPost.Description,
				Content = blogPost.Content,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				UrlHandle = blogPost.UrlHandle,
				DatePublished = blogPost.DatePublished,
				Author = blogPost.Author,
				IsVisible = blogPost.IsVisible
			};

			return Ok(response);
		}
	}
}