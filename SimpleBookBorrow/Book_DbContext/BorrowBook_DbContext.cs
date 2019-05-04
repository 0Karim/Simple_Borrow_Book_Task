using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SimpleBookBorrow.Models;

namespace SimpleBookBorrow.Book_DbContext
{
    public class BorrowBook_DbContext : DbContext
    {

        public BorrowBook_DbContext() : base ("BorrowedBooksEntities")
        {

        }

        public DbSet<Books> Books { set; get; }
        public DbSet<Borrower> Borrower { set; get; }
        public DbSet<BorrowedBooks> BorrowedBooks { set; get; }

    }
}