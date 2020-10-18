using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library_Management.Models;
using Microsoft.AspNet.Identity;

namespace Library_Management.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index(string sortOrder, string searchString)
        {
            var books = from b in _context.Books select b;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AuthorSortParam = sortOrder == "Author" ? "author_desc" : "Author";

            if (!User.IsInRole(RoleName.CanManageLibrary))
            {
                books = books.Where(b => b.UserId == null);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                books = searchString.Equals(BookAvailable.Available)
                    ? books.Where(b => b.UserId == null)
                    : books.Where(b => b.UserId != null);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(s => s.Name);
                    break;
                case "Author":
                    books = books.OrderBy(s => s.Author);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author);
                    break;
                default:
                    books = books.OrderBy(s => s.Name);
                    break;
            }

            if (!User.IsInRole(RoleName.CanManageLibrary))
            {
                return View("Index", books.ToList());
            }
            return View("AdminIndex", books.ToList());
        }

        public ActionResult MyBooks(string sortOrder, string searchString)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AuthorSortParam = sortOrder == "Author" ? "author_desc" : "Author";
            var books = from b in _context.Books select b;
            var currentUserId = User.Identity.GetUserId();
            books = books.Where(b => b.UserId.Equals(currentUserId));
            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(s => s.Name);
                    break;
                case "Author":
                    books = books.OrderBy(s => s.Author);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author);
                    break;
                default:
                    books = books.OrderBy(s => s.Name);
                    break;
            }

            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "CanManageLibrary")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Author,Status")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        [Authorize(Roles = "CanManageLibrary")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _context.Books.Find(id);
            if (book == null || book.UserId != null)
            {
                return HttpNotFound();
            }
            return View(book);
        }



        [Authorize(Roles = "CanManageLibrary")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Author,Status")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(book).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult Borrow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _context.Books.Find(id);
            if (book == null || book.UserId != null)
            {
                return HttpNotFound();
            }
            
            return View(book);
        }

        [HttpPost, ActionName("Borrow")]
        [ValidateAntiForgeryToken]
        public ActionResult BorrowConfirmed(int id)
        {
            Book book = _context.Books.Find(id);
            string currentUserId = User.Identity.GetUserId();
            BorrowHistory borrowHistory = new BorrowHistory
            {
                Book = book,
                BorrowDate = DateTime.Now,
                UserId = currentUserId

            };
            book.UserId = currentUserId;
            _context.BorrowHistory.Add(borrowHistory);
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _context.Books.Find(id);
            if (book == null || book.UserId == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public ActionResult Return(int id)
        {
            var book = _context.Books.Find(id);
            var borrowHistory = _context.BorrowHistory.FirstOrDefault(c => c.BookId == id && c.ReturnDate == null);
            book.UserId = null;
            borrowHistory.ReturnDate = DateTime.Now;
            _context.Entry(borrowHistory).State = EntityState.Modified;
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "CanManageLibrary")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [Authorize(Roles = "CanManageLibrary")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _context.Books.Find(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
