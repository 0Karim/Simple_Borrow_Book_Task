using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleBookBorrow.Models;
using SimpleBookBorrow.Book_DbContext;

namespace SimpleBookBorrow.DAL
{
    public class Repository_borrowBook : Repository<BorrowedBooks>
    {
        private BorrowBook_DbContext _context;
        public Repository_borrowBook()
        {
            _context = new BorrowBook_DbContext();
        }

        public List<Books> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public List<Borrower> GetAllBorrowers()
        {
            return _context.Borrower.ToList();
        }

        public bool DecrementBooksNum(int book_id)
        {
            Books book = _context.Books.SingleOrDefault(bo => bo.ID == book_id);
            if(book != null)
            {
                if(book.BookNo > 0)
                {
                    book.BookNo = book.BookNo - 1;
                }else
                {
                    return false;
                }

            }
            _context.SaveChanges();

            return true;
        }

        public Books getBook(int book_id)
        {
            return _context.Books.SingleOrDefault(b => b.ID == book_id);
        }
    }
}