﻿namespace CodePulse.API.Models.DTO
{
	public class BlogPostDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public string FeaturedImageUrl { get; set; } = string.Empty;
		public string UrlHandle { get; set; } = string.Empty;
		public DateTime DatePublished { get; set; }
		public string Author { get; set; } = string.Empty;
		public bool IsVisible { get; set; }
		public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
	}
}
