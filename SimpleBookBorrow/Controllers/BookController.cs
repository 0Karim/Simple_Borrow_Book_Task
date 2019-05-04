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
    public class BookController : Controller
    {
        IRepository<Books> repository;
        
        
        public BookController()
        {
            repository = new Repository<Books>();
        }
        
        // GET: Book
        //public ActionResult Index()
        //{
        //    IEnumerable<Books> books = repository.GetAll();
        //    return View();
        //}


        // GET: Book
        public ActionResult BookForm()
        {
            return View();
        }


        public JsonResult LoadBooks()
        {
            return Json(repository.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }
       

        public JsonResult NewOrEditBook(string BookName = "" , string Authorname ="" , int BookNo = 0 , string BookType = "" , int ID = 0)
        {
            string status = "";

            if(ID == 0) // add new 
            {
                Books book = new Books();
                book.Name = BookName;
                book.AuthorName = Authorname;
                book.BookNo = BookNo;
                book.BookType = BookType;

                try
                {
                    repository.Add(book);
                    
                    status = "Added-" + book.ID; 
                }
                catch (Exception ex)
                {

                }
            }
            else // edit
            {
                var book = repository.Find(b => b.ID == ID).FirstOrDefault();
                book.Name = BookName;
                book.AuthorName = Authorname;
                book.BookNo = BookNo;
                book.BookType = BookType;

                try
                {
                    repository.Update(book);
                    status = "updated";
                }
                catch (Exception ex)
                {

                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}