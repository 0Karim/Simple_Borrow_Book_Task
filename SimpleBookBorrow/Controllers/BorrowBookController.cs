using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBookBorrow.DAL.IRepositories;
using SimpleBookBorrow.DAL;
using SimpleBookBorrow.Models;



namespace SimpleBookBorrow.Controllers
{
    public class BorrowBookController : Controller
    {
        // GET: BorrowBook
        private Repository_borrowBook _repository;
        public BorrowBookController()
        {
            _repository = new Repository_borrowBook();
        }
        public ActionResult BorrowBookForm()
        {
            List<Books> booksList = _repository.GetAllBooks();
            List<Borrower> borrowerList = _repository.GetAllBorrowers();
            ViewBag.BooksList = new SelectList(booksList, "ID", "Name");
            ViewBag.BorrowerList = new SelectList(borrowerList, "ID", "Firsname");
            return View();
        }

        public JsonResult LoadAllBooks()
        {
            return Json(_repository.GetAllBooks(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadAllBorrowers()
        {
            List<KeyValuePair<string , string>> borrow_book_info = new List<KeyValuePair<string , string>>();
            IEnumerable<BorrowedBooks> list = _repository.GetAll();
            foreach(BorrowedBooks item in list)
            {
                Books book = _repository.getBook(item.BookID);
                if(book != null)
                {
                    item.Books = book;
                }
                borrow_book_info.Add(new KeyValuePair<string,string>(item.Books.Name, item.Borrower.Firsname));
            }            
            //_repository.GetAll().Select(obj => new { obj.Books.Name, obj.Borrower.Firsname }).ToList();
            return Json(borrow_book_info , JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddBorrow(int SelectedBook = 0 , int selectedBorrower = 0)
        {
            string status = "";

            if(selectedBorrower != 0 && SelectedBook != 0)
            {
                BorrowedBooks obj = new BorrowedBooks();
                obj.BookID = SelectedBook;
                obj.BorrowerID = selectedBorrower;
                obj.BookStatus = true;

                try
                {
                    bool check = _repository.DecrementBooksNum(obj.BookID);
                    if (check)
                    {
                        _repository.Add(obj);
                        status = "Added";
                    }
                    else
                    {
                        status = "NotAdded";
                    }

                }catch(Exception ex)
                {

                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}