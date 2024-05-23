using Microsoft.AspNet.Identity;
using SmartManagerLibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace SmartManagerLibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string searchString, string sortOrder)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "author_desc" : "Author";

            var books = from b in db.Books select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Name.Contains(searchString) || b.Author.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(b => b.Name);
                    break;
                case "Author":
                    books = books.OrderBy(b => b.Author);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author);
                    break;
                default:
                    books = books.OrderBy(b => b.Name);
                    break;
            }

            return View(books.ToList());
        }

        [Authorize]
        public ActionResult BookDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Borrow/5
        [Authorize]
        public ActionResult Borrow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Books book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var borrowModel = new BorrowViewModel
            {
                BookId = book.BookId,
                BookName = book.Name,
                LoanDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddMonths(6) // Default return date
            };

            return View(borrowModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Borrow(BorrowViewModel model)
        {
            if (ModelState.IsValid)
            {
                var maxReturnDate = model.LoanDate.AddMonths(6);
                if (model.ReturnDate > maxReturnDate)
                {
                    ModelState.AddModelError("ReturnDate", "The return date cannot be more than 6 months from the loan date.");
                    return View(model);
                }

                var userId = User.Identity.GetUserId();
                var loan = new Loans
                {
                    BookId = model.BookId,
                    UserId = userId,
                    LoanDate = model.LoanDate,
                    ReturnDate = model.ReturnDate,
                    Returned = false
                };

                // Update quantity
                var bookToUpdate = db.Books.Find(model.BookId);
                if (bookToUpdate != null)
                {
                    // Ensure the quantity is not already zero or negative
                    if (bookToUpdate.Quantity > 0)
                    {
                        bookToUpdate.Quantity--;
                        db.Entry(bookToUpdate).State = EntityState.Modified;
                    }
                    else
                    {
                        ModelState.AddModelError("", "The book is out of stock.");
                        return View(model);
                    }
                }

                db.Loans.Add(loan);
                db.SaveChanges();
                return RedirectToAction("BorrowedBooks");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult BorrowedBooks()
        {
            var userId = User.Identity.GetUserId();
            var loans = db.Loans.Where(l => l.UserId == userId && l.Returned == false).ToList();

            // Create a list to store user and book info for each loan
            var loanDetails = loans.Select(loan => new LoanDetailViewModel
            {
                Loan = loan,
                User = db.Users.FirstOrDefault(u => u.Id == loan.UserId),
                Book = db.Books.FirstOrDefault(b => b.BookId == loan.BookId)
            }).ToList();

            return View(loanDetails);
        }
        // GET: Books/Return/5
        [Authorize]
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Loans loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }

            return View(loan);
        }

        // POST: Books/Return/5
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ReturnConfirmed(int? id)
        {
            Loans loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }

            // Update quantity
            var bookToUpdate = db.Books.Find(loan.BookId);
            if (bookToUpdate != null)
            {
                bookToUpdate.Quantity++;
                db.Entry(bookToUpdate).State = EntityState.Modified;
            }

            loan.Returned = true;
            loan.ReturnDate = DateTime.Now;
            db.Entry(loan).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("BorrowedBooks");
        }



        [Authorize]
        public ActionResult BorrowHistory()
        {
            var userId = User.Identity.GetUserId();
            var loans = db.Loans.Where(l => l.UserId == userId && l.Returned == true).ToList();

            // Create a list to store user and book info for each loan
            var loanDetails = loans.Select(loan => new LoanDetailViewModel
            {
                Loan = loan,
                User = db.Users.FirstOrDefault(u => u.Id == loan.UserId),
                Book = db.Books.FirstOrDefault(b => b.BookId == loan.BookId)
            }).ToList();

            return View(loanDetails);
        }
    }
}