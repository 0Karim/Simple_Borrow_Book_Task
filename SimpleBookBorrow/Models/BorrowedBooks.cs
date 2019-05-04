using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookBorrow.Models
{
    public class BorrowedBooks
    {
        [Key , Column(Order = 1)]
        public int BorrowerID { set; get; }
        [Key, Column(Order = 2)]
        public int BookID { set; get; }


        //Relationship
        public virtual Books Books { set; get; }
        public virtual Borrower Borrower { set; get; }



        public bool? BookStatus { set; get; }
    }
}