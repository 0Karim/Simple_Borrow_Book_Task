using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleBookBorrow.Models
{
    public class Borrower
    {
        [Key , DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string Firsname { set; get; }
        public string Lastname { set; get; }
        public int? MobileNo { set; get; }
        public bool? Gender { set; get; }
        
        
        //Relationship
        public virtual ICollection<BorrowedBooks> BorrowedBooks { set; get; }
    }
}