namespace CodePulse.API.Models.DTO
{
	public class UpdateBlogPostRequestDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Content { get; set; }
		public string FeaturedImageUrl { get; set; }
		public string UrlHandle { get; set; }
		public DateTime DatePublished { get; set; }
		public string Author { get; set; }
		public bool IsVisible { get; set; }
		public Guid[] Categories { get; set; } = Array.Empty<Guid>();
	}
}
