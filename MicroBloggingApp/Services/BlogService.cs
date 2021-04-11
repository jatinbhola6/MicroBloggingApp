using MicroBloggingApp.Data;
using MicroBloggingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBloggingApp.Services
{
    public class BlogService
    {
        private readonly MicroBlogginDBContext _context;

        public BlogService(MicroBlogginDBContext context)
        {
            this._context = context;
        }

        public async Task<bool> BlogModelExists(int id)
        {
            return  await _context.Blogs.AnyAsync(e => e.Id == id);
        }

        public async Task CreateBlog(BlogModel blog)
        {
            blog.CreateDate = DateTime.Now;
            _context.Add<BlogModel>(blog);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if(blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BlogModel>> GetAllBlogs(string userId)
        {
            return await _context.Blogs.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<BlogModel> GetBlog(string userId, int id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        }

        public async Task UpdateBlog(BlogModel blog)
        {
            this._context.Update<BlogModel>(blog);
            await this._context.SaveChangesAsync();
        }
    }
}
