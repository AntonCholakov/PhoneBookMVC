using PhoneBookMVC.DA;
using PhoneBookMVC.Entity;
using PhoneBookMVC.Filters;
using PhoneBookMVC.ViewModels.PhonesVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Controllers
{
    [AuthorizeUser]
    public class PhonesController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Phones
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return Redirect("~/Contacts");
            }

            PhonesIndexVM model = new PhonesIndexVM();
            model.ContactId = id.Value;
            model.Phones = unitOfWork.PhoneRepository.GetByContactId(id.Value).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int? contactId)
        {
            if (!contactId.HasValue)
            {
                return RedirectToAction("Index");
            }

            PhonesCreateVM model = new PhonesCreateVM();
            model.ContactId = contactId.Value;

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? contactId, int? id)
        {
            if (!id.HasValue || !contactId.HasValue)
            {
                return RedirectToAction("Index");
            }

            Phone phone = unitOfWork.PhoneRepository.GetById(id.Value);

            if (phone == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            PhonesCreateVM model = new PhonesCreateVM();
            model.ID = phone.ID;
            model.Number = phone.Number;
            model.Type = phone.Type;
            model.ContactId = contactId.Value;

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(PhonesCreateVM model)
        {
            if (ModelState.IsValid)
            {
                Phone phone = new Phone { ID=model.ID, Type = model.Type, Number = model.Number, ContactId = model.ContactId };
                if (model.ID <= 0)
                {
                    unitOfWork.PhoneRepository.Insert(phone);
                }
                else
                {
                    unitOfWork.PhoneRepository.Update(phone);
                }
                unitOfWork.Save();

                return RedirectToAction("Index/" + phone.ContactId);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            Phone phone = unitOfWork.PhoneRepository.GetById(id.Value);

            if (phone == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            PhonesCreateVM model = new PhonesCreateVM();
            model.ID = phone.ID;
            model.ContactId = phone.ContactId;
            model.Number = phone.Number;
            model.Type = phone.Type;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(PhonesCreateVM phonesVM)
        {
            unitOfWork.PhoneRepository.Delete(phonesVM.ID);
            unitOfWork.Save();

            return RedirectToAction("Index/"+phonesVM.ContactId);
        }
    }
}