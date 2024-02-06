using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList=applicationDbContext.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if((category.Name == category.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("name","Display Order cannot match the name");
            }
            if (ModelState.IsValid)
            {
                applicationDbContext.Categories.Add(category);
                applicationDbContext.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
             
        }

        public IActionResult Edit(int id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb=applicationDbContext.Categories.Find(id);   //i this statement only primary key can be used(in find(id))
            /*Category? categoryFromDb1 = applicationDbContext.Categories.FirstOrDefault(u => u.Id == id);
            Category? categoryFromDb2 = applicationDbContext.Categories.Where(u=>u.Id == id).FirstOrDefault();*/

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if ((category.Name == category.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("name", "Display Order cannot match the name");
            }
            if (ModelState.IsValid)
            {
                applicationDbContext.Categories.Update(category);
                applicationDbContext.SaveChanges();
                TempData["success"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = applicationDbContext.Categories.Find(id);   //i this statement only primary key can be used(in find(id))
           

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]

        public IActionResult DeletePost(int id)
        {

            Category? obj = applicationDbContext.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
          
            applicationDbContext.Categories.Remove(obj);
            applicationDbContext.SaveChanges();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
