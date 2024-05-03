using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repository.Implementation
{
	public class BlogPostRepository : IBlogPostRespository
	{
		private readonly ApplicationDbContext _db;

		public BlogPostRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IEnumerable<BlogPost>> GetAllAsync()
		{
			return await _db.BlogPosts.Include(bp => bp.Categories).ToListAsync();
		}
		public async Task<BlogPost?> GetByIdAsync(Guid id)
		{
			return await _db.BlogPosts.Include(bp => bp.Categories).FirstOrDefaultAsync(bp => bp.Id == id);
		}

		public async Task<BlogPost> CreateAsync(BlogPost blogPost)
		{
			await _db.BlogPosts.AddAsync(blogPost);
			await _db.SaveChangesAsync();

			return blogPost;
		}

		public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
		{
			var existingBlogPost = await _db.BlogPosts.FindAsync(blogPost.Id);

			if (existingBlogPost != null)
			{
				existingBlogPost.Title = blogPost.Title;
				existingBlogPost.Description = blogPost.Description;
				existingBlogPost.Content = blogPost.Content;
				existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
				existingBlogPost.UrlHandle = blogPost.UrlHandle;
				existingBlogPost.DatePublished = blogPost.DatePublished;
				existingBlogPost.Author = blogPost.Author;
				existingBlogPost.IsVisible = blogPost.IsVisible;

				await _db.SaveChangesAsync();
				return blogPost;
			}

			return null;
		}

		public async Task<BlogPost?> DeleteAsync(Guid id)
		{
			var existingBlogPost = await _db.BlogPosts.FindAsync(id);

			if (existingBlogPost != null)
			{
				_db.BlogPosts.Remove(existingBlogPost);
				await _db.SaveChangesAsync();
				return existingBlogPost;
			}

			return null;
		}
	}
}
