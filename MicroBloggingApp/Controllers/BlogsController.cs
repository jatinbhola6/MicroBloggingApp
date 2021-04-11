using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicroBloggingApp.Data;
using MicroBloggingApp.Models;
using MicroBloggingApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace MicroBloggingApp.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly BlogService _blogService;

        private readonly UserService _userService;

        public BlogsController(BlogService blogService, UserService userService)
        {
            this._blogService = blogService;
            this._userService = userService;
        }
        // GET: BlogsController
        public async Task<IActionResult> Index()
        {
            List<BlogModel> allBlogs = await _blogService.GetAllBlogs(this._userService.GetEmail(User));
            return this.View(allBlogs);
        }

        // GET: BlogsController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id.HasValue)
            {
                BlogModel blog = await this._blogService.GetBlog(this._userService.GetEmail(User), id.Value);
                if (blog != null)
                {
                    return View(blog);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // GET: BlogsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Heading,Body")] BlogModel blogModel)
        {
            if (!ModelState.IsValid)
            {
                return View(blogModel);
            }
            else
            {
                blogModel.UserId = _userService.GetEmail(User);
                await _blogService.CreateBlog(blogModel);
                return RedirectToAction("Index");
            }
        }

        // GET: BlogsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                BlogModel blog = await _blogService.GetBlog(this._userService.GetEmail(User), id.Value);
                if (blog != null)
                {
                    return View(blog);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // POST: BlogsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CreateDate,Heading,Body")] BlogModel blog)
        {
            await _blogService.UpdateBlog(blog);
            return RedirectToAction("Details", "Blogs", new { id = id });

        }

        // GET: BlogsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var blog = await _blogService.GetBlog(_userService.GetEmail(User), id.Value);
                if (blog != null) return View(blog);
                else return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: BlogsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeleteBlog(id);
            return RedirectToAction("Index");
        }
    }
}
