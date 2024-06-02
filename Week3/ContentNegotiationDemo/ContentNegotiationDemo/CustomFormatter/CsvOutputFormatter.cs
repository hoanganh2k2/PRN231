using ContentNegotiationDemo.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace ContentNegotiationDemo.CustomFormatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(Blog).IsAssignableFrom(type) || typeof(IEnumerable<Blog>).IsAssignableFrom(type))
            {
                return true;
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            HttpResponse response = context.HttpContext.Response;
            StringBuilder buffer = new();

            if (context.Object is IEnumerable<Blog> blogs)
            {
                foreach (Blog blog in blogs)
                {
                    FormatCsv(buffer, blog);
                }
            }
            else
            {
                Blog? blog = context.Object as Blog;
                FormatCsv(buffer, blog);
            }

            using (StreamWriter writer = new(response.Body, selectedEncoding))
            {
                await writer.WriteAsync(buffer.ToString());
                await writer.FlushAsync();
            }
        }

        private static void FormatCsv(StringBuilder buffer, Blog? blog)
        {
            if (blog == null)
                return;

            buffer.AppendLine("Name,Description");
            buffer.AppendLine($"{blog.Name},{blog.Description}");
            buffer.AppendLine("Title,MetaDescription,Published");

            foreach (BlogPost post in blog.BlogPosts)
            {
                buffer.AppendLine($"{post.Title},{post.MetaDescription},{post.Published:O}"); // :O for ISO 8601 format
            }
        }
    }
}
