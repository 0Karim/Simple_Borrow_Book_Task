using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleBookBorrow.Models
{
    public class Books
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string Name { set; get; }
        public string AuthorName { set; get; }
        public int? BookNo { set; get; }

        public string BookType { set; get; }


        //Relationship
        public virtual ICollection<BorrowedBooks> BorrowedBooks { set; get; }
    }
}