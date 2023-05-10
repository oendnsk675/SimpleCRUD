using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookDataAccsessLayer book;

        public HomeController()
        {
            book = new BookDataAccsessLayer();
        }

        public IActionResult Index()
        {
            
            IEnumerable<BookViewModel> bookList = book.GetAll();
            return View(bookList);
        }

        public IActionResult Create()
        {
            CategoryDataAccessLayer category = new CategoryDataAccessLayer();
            ViewBag.Category = category.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Books obj)
        {
            if(ModelState.IsValid)
            {
                book.Create(obj);
                return RedirectToAction("Index");
            }
            CategoryDataAccessLayer category = new CategoryDataAccessLayer();
            ViewBag.Category = category.GetAll();
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var books = book.GetBook(id);
            if (books == null)
            {
                return NotFound();
            }
            CategoryDataAccessLayer category = new CategoryDataAccessLayer();
            // var categories = category.GetAll()
            ViewBag.Category = category.GetAll();
            return View(books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Books obj)
        {
            if (ModelState.IsValid)
            {
                book.Update(obj);
                return RedirectToAction("Index");
            }else
            {
                CategoryDataAccessLayer category = new CategoryDataAccessLayer();
                // var categories = category.GetAll()
                ViewBag.Category = category.GetAll();
                return View(obj);
            }
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            book.Remove(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}