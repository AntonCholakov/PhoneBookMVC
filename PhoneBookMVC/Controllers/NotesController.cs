using PhoneBookMVC.DA;
using PhoneBookMVC.Entity;
using PhoneBookMVC.Filters;
using PhoneBookMVC.ViewModels.NotesVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Controllers
{
    [AuthorizeUser]
    public class NotesController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Notes
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return Redirect("~/Contacts");
            }

            NotesIndexVM model = new NotesIndexVM();
            model.Notes = unitOfWork.NoteRepository.GetByContactId(id.Value).ToList();
            model.ContactId = id.Value;
            model.Contact = unitOfWork.ContactRepository.GetById(id.Value);

            if (model.Contact == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int? contactId)
        {
            if (!contactId.HasValue)
            {
                return RedirectToAction("Index");
            }

            NotesCreateVM model = new NotesCreateVM();
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

            Note note = unitOfWork.NoteRepository.GetById(id.Value);

            if (note == null)
            {
                return Redirect("~/Error/PageNotFound");
            }
            
            NotesCreateVM model = new NotesCreateVM();
            model.Id = note.ID;
            model.Text = note.Text;
            model.DateCreated = note.DateCreated;
            model.DateLastEdit = note.DateLastEdit;
            model.ContactId = contactId.Value;
            
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(NotesCreateVM model)
        {
            if (ModelState.IsValid)
            {

                Note note = new Note { ID = model.Id, ContactId = model.ContactId, Text = model.Text };
                if (model.Id <= 0)
                {
                    note.DateCreated = DateTime.Now;
                    note.DateLastEdit = DateTime.Now;
                    unitOfWork.NoteRepository.Insert(note);
                }
                else
                {
                    note.DateLastEdit = DateTime.Now;
                    note.DateCreated = model.DateCreated;
                    unitOfWork.NoteRepository.Update(note);
                }
                unitOfWork.Save();

                return RedirectToAction("Index/" + note.ContactId);

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return Redirect("~/Contacts/Index");
            }

            Note note = unitOfWork.NoteRepository.GetById(id.Value);

            if (note == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            NotesCreateVM model = new NotesCreateVM();
            model.Id = note.ID;
            model.ContactId = note.ContactId;
            model.Text = note.Text;
            model.DateCreated = note.DateCreated;
            model.DateLastEdit = note.DateLastEdit;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(NotesCreateVM model)
        {
            unitOfWork.NoteRepository.Delete(model.Id);
            unitOfWork.Save();

            return RedirectToAction("Index/" + model.ContactId);
        }
    }
}