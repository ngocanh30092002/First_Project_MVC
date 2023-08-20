﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProject.Models;
using FirstProject.Models.Blog;
using App.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FirstProject.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("admin/blog/category/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Blog/Category
        public async Task<IActionResult> Index()
        {
            var qr = (from c in _context.Categories select c)
                .Include(c => c.ParentCategory)
                .Include(c => c.CategoryChildren);

            var list = (await qr.ToListAsync())
                        .Where(c => c.ParentCategory == null)
                        .ToList();

            return View(list);
        }

        // GET: Blog/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        //1
        // 2.1
        //   3.3
        // 2.2
        //   3.4


        private void CreateSelectItem(List<Category> source, List<Category> des, int level)
        {
            var prefix = string.Concat(Enumerable.Repeat("---", level));
            foreach(var category in source)
            {
                des.Add(new Category
                {
                    Title = prefix + " " + category.Title,
                    Id = category.Id
                });

                if(category.CategoryChildren?.Count > 0)
                {
                    CreateSelectItem(category.CategoryChildren.ToList(), des, level + 1);
                }
            }
        }

        // GET: Blog/Category/Create
        public async Task<IActionResult> CreateAsync()
        {
            var qr = (from c in _context.Categories select c)
               .Include(c => c.ParentCategory)
               .Include(c => c.CategoryChildren);

            var list = (await qr.ToListAsync())
                        .Where(c => c.ParentCategory == null)
                        .ToList();

            var items = new List <Category>();

            items.Add(new Category
            {
                Id = -1,
                Title = "Không có danh mục cha"
            });

            CreateSelectItem(list, items, 0);
            

            ViewData["ParentCategoryId"] = new SelectList(items, "Id", "Title");
            return View();
        }

        // POST: Blog/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentCategoryId,Title,Content,Slug")] Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.ParentCategoryId == -1)
                {
                    category.ParentCategoryId = null;
                }

                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var qr = (from c in _context.Categories select c)
               .Include(c => c.ParentCategory)
               .Include(c => c.CategoryChildren);

            var list = (await qr.ToListAsync())
                        .Where(c => c.ParentCategory == null)
                        .ToList();

            var items = new List<Category>();

            items.Add(new Category
            {
                Id = -1,
                Title = "Không có danh mục cha"
            });

            CreateSelectItem(list, items, 0);

            ViewData["ParentCategoryId"] = new SelectList(items, "Id", "Title", category.ParentCategoryId);
            return View(category);
        }

        // GET: Blog/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var qr = (from c in _context.Categories select c)
              .Include(c => c.ParentCategory)
              .Include(c => c.CategoryChildren);

            var list = (await qr.ToListAsync())
                        .Where(c => c.ParentCategory == null)
                        .ToList();

            var items = new List<Category>();

            items.Add(new Category
            {
                Id = -1,
                Title = "Không có danh mục cha"
            });

            CreateSelectItem(list, items, 0);
            ViewData["ParentCategoryId"] = new SelectList(items, "Id", "Title", category.ParentCategoryId);
            return View(category);
        }

        // POST: Blog/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentCategoryId,Title,Content,Slug")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if(category.ParentCategoryId == category.Id)
            {
                ModelState.AddModelError(string.Empty, "Phải chọn danh mục cha khác ");
            }

            if (ModelState.IsValid && category.ParentCategoryId != category.Id)
            {
                try
                {
                    if(category.ParentCategoryId == -1)
                    {
                        category.ParentCategoryId = null;
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }


            var qr = (from c in _context.Categories select c)
              .Include(c => c.ParentCategory)
              .Include(c => c.CategoryChildren);

            var list = (await qr.ToListAsync())
                        .Where(c => c.ParentCategory == null)
                        .ToList();

            var items = new List<Category>();

            items.Add(new Category
            {
                Id = -1,
                Title = "Không có danh mục cha"
            });

            CreateSelectItem(list, items, 0);
            ViewData["ParentCategoryId"] = new SelectList(items, "Id", "Title", category.ParentCategoryId);
            return View(category);
        }

        // GET: Blog/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Blog/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'AppDbContext.Categories'  is null.");
            }
            var category = await _context.Categories
                            .Include(c => c.CategoryChildren)
                            .FirstOrDefaultAsync(c => c.Id == id);

            if(category == null)
            {
                return NotFound();
            }

            foreach(var cCategory in category.CategoryChildren)
            {
                cCategory.ParentCategoryId = cCategory.ParentCategoryId;
            }
            
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
