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
    public class BorrowHistoryController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: BorrowHistory
        public ActionResult Index()
        {
            var borrowHistory = _context.BorrowHistory.Include(b => b.Book);
            if (User.IsInRole(RoleName.CanManageLibrary))
            {
                return View(borrowHistory.ToList());
            }

            var currentUserId = User.Identity.GetUserId();
            borrowHistory = borrowHistory.Where(b => b.UserId == currentUserId);
            return View(borrowHistory.ToList());
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
