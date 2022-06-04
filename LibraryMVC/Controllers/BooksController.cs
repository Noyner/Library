using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryMVC.Models;
using PagedList;

namespace LibraryMVC.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "author_desc" : "Author";
            ViewBag.YearSortParm = sortOrder == "YearOfPublishing" ? "year_desc" : "YearOfPublishing";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var books = db.Books.Include(b => b.BorrowHistories)
                .Select(b => new BookViewModel
                {
                    BookId = b.BookId,
                    Author = b.Author,
                    Publisher = b.Publisher,
                    SerialNumber = b.SerialNumber,
                    Title = b.Title,
                    Genre = b.Genre,
                    YearOfPublishing = b.YearOfPublishing,
                    NumberOfPages = b.NumberOfPages,
                    IsAvailable = !b.BorrowHistories.Any(h => h.ReturnDate == null)
                });

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString)
                                         || s.Author.Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(s => s.Title);
                    break;
                case "YearOfPublishing":
                    books = books.OrderBy(s => s.YearOfPublishing);
                    break;
                case "year_desc":
                    books = books.OrderByDescending(s => s.YearOfPublishing);
                    break;
                case "Author":
                    books = books.OrderBy(s => s.Author);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author);
                    break;
                default:
                    books = books.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));

            //return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
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

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,SerialNumber,Author,Publisher,Genre,NumberOfPages,YearOfPublishing")] Book book)
        //public ActionResult Create(Book book, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);


            //if (ModelState.IsValid)
            //{
            //    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
            //    file.SaveAs(path);
            //    db.Books.Add(new Book()
            //    {
            //        BookId = book.BookId,
            //        Title = book.Title,
            //        SerialNumber = book.SerialNumber,
            //        Author = book.Author,
            //        Publisher = book.Publisher,
            //        Genre = book.Genre,
            //        NumberOfPages = book.NumberOfPages,
            //        YearOfPublishing = book.YearOfPublishing,
            //        //FilePath = "~/UploadedFiles/" + file.FileName
            //    });
            //    db.SaveChanges();
            //    return View(book);
            //}
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,SerialNumber,Author,Publisher,Genre,NumberOfPages,YearOfPublishing")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
