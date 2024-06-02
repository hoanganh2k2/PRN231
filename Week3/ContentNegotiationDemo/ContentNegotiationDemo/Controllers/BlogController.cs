using ContentNegotiationDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Blog>> GetBlogs()
        {
            List<Blog> blogs = new();

            List<BlogPost> blogPosts1 = new()
            {
                new BlogPost
                {
                    Title = "Content negotiation in .NET Core",
                    MetaDescription = "Content negotiation is one of the key features in .NET Core for APIs.",
                    Published = true
                }
            };

            Blog blog1 = new()
            {
                Name = "Code Maze",
                Description = "A practical programmers resource",
                BlogPosts = blogPosts1
            };

            blogs.Add(blog1);

            return Ok(blogs);
        }
    }
}
