using PhoneBookMVC.DA;
using PhoneBookMVC.Entity;
using PhoneBookMVC.Models;
using PhoneBookMVC.ViewModels.QueryVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View("About", new QueryCreateVM());
        }

        [HttpPost]
        public ActionResult About(QueryCreateVM model)
        {
            if (ModelState.IsValid)
            {
                model.Date = DateTime.Now;

                Query query = new Query();
                query.ID = model.ID;
                query.Title = model.Title;
                query.Content = model.Content;
                query.Email = model.Email;
                query.Date = model.Date;
                
                unitOfWork.QueryRepository.Insert(query);
                unitOfWork.Save();

                MailMessage message = new MailMessage();
                message.Sender = new MailAddress(model.Email);
                message.To.Add("phonebookadm@gmail.com");
                message.Subject = model.Title + " - " + model.Date;
                message.From = new MailAddress(model.Email);
                message.Body = model.Title + "\n" + model.Content + "\nFrom: " + model.Email;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                #region Private
                smtp.Credentials = new NetworkCredential("phonebookadm@gmail.com", "programistaphonebook");
                #endregion

                smtp.Send(message);


                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}