using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repository.Interface
{
	public interface IBlogPostRespository
	{
		Task<IEnumerable<BlogPost>> GetAllAsync();
		Task<BlogPost?> GetByIdAsync(Guid id);
		Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
		Task<BlogPost> CreateAsync(BlogPost blogPost);
		Task<BlogPost?> UpdateAsync(BlogPost blogPost);
		Task<BlogPost?> DeleteAsync(Guid id);

	}
}
