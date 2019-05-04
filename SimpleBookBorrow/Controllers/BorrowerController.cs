using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBookBorrow.Models;
using SimpleBookBorrow.DAL.IRepositories;
using SimpleBookBorrow.DAL;
using SimpleBookBorrow.Book_DbContext;


namespace SimpleBookBorrow.Controllers
{
    public class BorrowerController : Controller
    {
        IRepository<Borrower> repository;


        public BorrowerController()
        {
            repository = new Repository<Borrower>();
        }

        // GET: Borrower
        public ActionResult BorrowerForm()
        {
            return View();
        }


        public JsonResult LoadBorrowers()
        {
            repository.TurnOffProxyCreation(); //to not enable circular problem
            return Json(repository.GetAll() , JsonRequestBehavior.AllowGet);
        }
   

        public JsonResult NewOrEditBorrower(string BorrowerFirstName = "", string BorrowerLastName = "", int MobileNo = 0, bool Gender = false, int ID = 0)
        {
            string status = "";

            if (ID == 0) // add new 
            {
                Borrower borrow = new Borrower();
                borrow.Firsname = BorrowerFirstName;
                borrow.Lastname = BorrowerLastName;
                borrow.MobileNo = MobileNo;
                borrow.Gender = Gender;

                try
                {
                    repository.Add(borrow);

                    status = "Added-" + borrow.ID;
                }
                catch (Exception ex)
                {

                }
            }
            else // edit
            {
                var borrow = repository.Find(b => b.ID == ID).FirstOrDefault();
                
                borrow.Firsname = BorrowerFirstName;
                borrow.Lastname = BorrowerLastName;
                borrow.MobileNo = MobileNo;
                borrow.Gender = Gender;

                try
                {
                    repository.Update(borrow);
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