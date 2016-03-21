using Newtonsoft.Json;
using PhoneBookMVC.DA;
using PhoneBookMVC.Entity;
using PhoneBookMVC.Filters;
using PhoneBookMVC.Models;
using PhoneBookMVC.ViewModels.ContactsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Helpers;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using PhoneBookMVC.ViewModels.ContactGroupsVM;

namespace PhoneBookMVC.Controllers
{
    [AuthorizeUser]
    public class ContactsController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Contacts
        public ActionResult Index(ContactsIndexVM model)
        {
            model.Props = new Dictionary<string, object>();
            model.Props["firstname"] = model.FirstName;
            model.Props["lastname"] = model.LastName;
            model.Props["groupId"] = model.GroupId;

            AppContext ctx = new AppContext();
            ContactRepository contactRepo = new ContactRepository(ctx);
            model.Contacts = contactRepo.GetByUserId(AuthenticationManager.LoggedUser.ID).ToList();

            if (!String.IsNullOrEmpty(model.FirstName))
            {
                model.Contacts = model.Contacts.Where(c => c.FirstName.ToLower().Contains(model.FirstName.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(model.LastName))
            {
                model.Contacts = model.Contacts.Where(c => c.LastName.ToLower().Contains(model.LastName.ToLower())).ToList();
            }
            if (model.GroupId != 0)
            {
                model.Contacts = model.Contacts.Where(c => c.ContactGroups.FirstOrDefault(g => g.ID == model.GroupId) != null).ToList();
            }

            switch (model.SortOrder)
            {
                case "firstname_asc":
                    model.Contacts = model.Contacts.OrderBy(c => c.FirstName).ToList();
                    break;
                case "firstname_desc":
                    model.Contacts = model.Contacts.OrderByDescending(c => c.FirstName).ToList();
                    break;
                case "email_asc":
                    model.Contacts = model.Contacts.OrderBy(c => c.Email).ToList();
                    break;
                case "email_desc":
                    model.Contacts = model.Contacts.OrderByDescending(c => c.Email).ToList();
                    break;
                case "lastname_desc":
                    model.Contacts = model.Contacts.OrderByDescending(c => c.LastName).ToList();
                    break;
                case "lastname_asc":
                    model.Contacts = model.Contacts.OrderBy(c => c.LastName).ToList();
                    break;
                case "birthdate_desc":
                    model.Contacts = model.Contacts.OrderByDescending(c => c.BirthDay).ToList();
                    break;
                case "birthdate_asc":
                    model.Contacts = model.Contacts.OrderBy(c => c.BirthDay).ToList();
                    break;

                default:
                    model.Contacts = model.Contacts.OrderBy(c => c.FirstName).ToList();
                    break;
            }

            int pageSize = int.Parse(WebConfigurationManager.AppSettings["contactsPerPage"]);
            int pageNumber = (model.Page ?? 1);
            model.PagedContacts = model.Contacts.ToPagedList(pageNumber, pageSize);

            ContactGroupRepository groupRepo = new ContactGroupRepository(ctx);
            model.ContactGroups = groupRepo.GetByUserId(AuthenticationManager.LoggedUser.ID);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateEdit", new ContactsCreateVM());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            Contact contact = unitOfWork.ContactRepository.GetById(id.Value);

            if (contact == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            ContactsCreateVM model = new ContactsCreateVM();
            model.ID = contact.ID;
            model.FirstName = contact.FirstName;
            model.LastName = contact.LastName;
            model.UserId = contact.UserId;
            model.Email = contact.Email;
            model.PhotoFilePath = contact.PhotoFilePath;
            model.BirthDate = contact.BirthDay;

            return View("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult CreateEdit(ContactsCreateVM model, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                Contact contact;

                if (model.ID > 0)
                {
                    contact = unitOfWork.ContactRepository.GetById(model.ID);
                }
                else
                {
                    contact = new Contact();
                }

                // Email

                // Photo
                WebImage img = null;

                if (photo != null)
                {
                    string path = Server.MapPath(@"~/Content/contacts_photos/" + AuthenticationManager.LoggedUser.ID);
                    var file = Request.Files["photo"];

                    if (file.ContentLength > int.Parse(WebConfigurationManager.AppSettings["allowedImageSize"]))
                    {
                        ModelState.AddModelError(string.Empty, "Image too big");
                        return View(model);
                    }

                    try
                    {
                        // if file is not an image, exception will be thrown - Invalid Image Format
                        img = new WebImage(file.InputStream);
                        string guid = Guid.NewGuid().ToString();
                        try
                        {
                            img.Save(path + "//" + guid, img.ImageFormat, true);
                        }
                        catch (IOException)
                        {
                            Directory.CreateDirectory(path);
                            img.Save(path + "//" + guid, img.ImageFormat, true);
                        }

                        if (model.ID > 0 && !contact.PhotoFilePath.Contains("default"))
                        {
                            System.IO.File.Delete(Server.MapPath("~" + contact.PhotoFilePath));
                        }

                        // Saving PhotoFilePath
                        contact.PhotoFilePath = "/Content/contacts_photos/" + AuthenticationManager.LoggedUser.ID + "/" + guid + "." + img.ImageFormat;
                    }
                    catch (ArgumentException)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Image Format");
                        return View(model);
                    }
                }
                else
                {
                    string filepath = Server.MapPath(contact.PhotoFilePath);
                    DeleteExistingPhoto(filepath);

                    contact.PhotoFilePath = "/Content/contacts_photos/default.png";
                    
                }

                model.UserId = AuthenticationManager.LoggedUser.ID;
                contact.ID = model.ID;
                contact.FirstName = model.FirstName;
                contact.LastName = model.LastName;
                contact.UserId = AuthenticationManager.LoggedUser.ID;
                contact.Email = model.Email;
                contact.BirthDay = model.BirthDate;

                if (model.ID == 0)
                {
                    unitOfWork.ContactRepository.Insert(contact);
                }
                else
                {
                    unitOfWork.ContactRepository.Update(contact);
                }
                unitOfWork.Save();

                return RedirectToAction("Index");               
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

            Contact contact = unitOfWork.ContactRepository.GetById(id.Value);

            if (contact == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            ContactsDeleteVM model = new ContactsDeleteVM();
            model.ID = contact.ID;
            model.FirstName = contact.FirstName;
            model.LastName = contact.LastName;
            model.UserId = contact.ID;
            model.Email = contact.Email;
            model.PhotoFilePath = contact.PhotoFilePath;
            model.BirthDate = contact.BirthDay;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(ContactsDeleteVM model)
        {
            unitOfWork.ContactRepository.Delete(model.ID);
            string path = Server.MapPath(model.PhotoFilePath);

            DeleteExistingPhoto(path);

            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeletePhoto(int? contactId)
        {
            Contact contact = unitOfWork.ContactRepository.GetById(contactId.Value);

            if (!contactId.HasValue || contact == null)
            {
                return RedirectToAction("Index");
            }

            string path = Server.MapPath(contact.PhotoFilePath);

            DeleteExistingPhoto(path);

            contact.PhotoFilePath = "/Content/contacts_photos/default.png";
            unitOfWork.ContactRepository.Update(contact);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditGroups(int? id)
        {
            Contact contact = unitOfWork.ContactRepository.GetById(id.Value);

            if (!id.HasValue || contact == null)
            {
                return RedirectToAction("Index");
            }

            return View(new ContactGroupsIndexVM { ContactGroups = unitOfWork.ContactGroupRepository.GetByUserId(AuthenticationManager.LoggedUser.ID), Contact = contact });
        }

        [HttpPost]
        public ActionResult EditGroupConfirm(ContactGroupsIndexVM model)
        {
            Contact contact = unitOfWork.ContactRepository.GetById(model.ContactID);
            contact.ContactGroups.Clear();

            var groups = unitOfWork.ContactGroupRepository.GetByUserId(AuthenticationManager.LoggedUser.ID);
            foreach (var g in groups)
            {
                if ((Request.Form[g.ID.ToString()] != null) && (Request.Form[g.ID.ToString()] == "on"))
                {
                    contact.ContactGroups.Add(g);
                }
            }
            unitOfWork.ContactRepository.Update(contact);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        private void DeleteExistingPhoto(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}