using PhoneBookMVC.DA;
using PhoneBookMVC.Entity;
using PhoneBookMVC.Filters;
using PhoneBookMVC.Models;
using PhoneBookMVC.ViewModels.ContactGroupsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Controllers
{
    [AuthorizeUser]
    public class ContactGroupsController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: ContactGroups
        public ActionResult Index()
        {
            ContactGroupsIndexVM model = new ContactGroupsIndexVM();
            model.ContactGroups = unitOfWork.ContactGroupRepository.GetByUserId(AuthenticationManager.LoggedUser.ID);
            return View(model);
        }

        public ActionResult Create()
        {
            ContactGroupsCreateVM model = new ContactGroupsCreateVM();
            model.Contacts = unitOfWork.ContactRepository.GetByUserId(AuthenticationManager.LoggedUser.ID).ToList();
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            ContactGroup group = unitOfWork.ContactGroupRepository.GetById(id.Value);

            if (group == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            ContactGroupsCreateVM model = new ContactGroupsCreateVM();

            model.Contacts = unitOfWork.ContactRepository.GetByUserId(AuthenticationManager.LoggedUser.ID).ToList();
            model.Name = group.Name;
            model.ID = group.ID;

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(ContactGroupsCreateVM model)
        {
            if (ModelState.IsValid)
            {
                List<Contact> contacts = unitOfWork.ContactRepository.GetByUserId(AuthenticationManager.LoggedUser.ID).ToList();

                if (model.ID <= 0)
                {
                    ContactGroup group = new ContactGroup { UserId = AuthenticationManager.LoggedUser.ID, Name = model.Name };
                    group.Contacts = new List<Contact>();

                    foreach (var c in contacts)
                    {
                        if ((Request.Form[c.ID.ToString()] != null) && (Request.Form[c.ID.ToString()] == "on"))
                        {
                            group.Contacts.Add(c);
                        }
                    }
                    unitOfWork.ContactGroupRepository.Insert(group);
                }
                else
                {
                    ContactGroup group = unitOfWork.ContactGroupRepository.GetById(model.ID);
                    if (group == null)
                    {
                        return Redirect("~/Error/PageNotFound");
                    }
                    group.Contacts.Clear();
                    

                    foreach (var c in contacts)
                    {
                        if ((Request.Form[c.ID.ToString()] != null) && (Request.Form[c.ID.ToString()] == "on"))
                        {
                            group.Contacts.Add(c);
                        }
                    }
                    unitOfWork.ContactGroupRepository.Update(group);
                }

                unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            ContactGroup group = unitOfWork.ContactGroupRepository.GetById(id.Value);

            if (group == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            ContactGroupsDetailsVM model = new ContactGroupsDetailsVM();
            model.Contacts = group.Contacts.ToList();

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            ContactGroup group = unitOfWork.ContactGroupRepository.GetById(id.Value);

            if (group == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            ContactGroupsDeleteVM model = new ContactGroupsDeleteVM();
            model.ID = group.ID;
            model.Name = group.Name;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(ContactGroupsDeleteVM model)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ContactGroupRepository.Delete(model.ID);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}