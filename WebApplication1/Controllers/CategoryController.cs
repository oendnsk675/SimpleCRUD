using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryDataAccessLayer category;
        public CategoryController()
        {
            category = new CategoryDataAccessLayer();
        }

        // GET: HomeController1
        public ActionResult Index()
        {
            IEnumerable<Categories> categories = category.GetAll();
            return View(categories);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categories obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.Create(obj);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var result = category.GetCategroy(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.Update(obj);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // POST: HomeController1/Delete/5
        [HttpGet]
        public ActionResult Remove(int id)
        {
            try
            {
                category.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
