using PhoneBookMVC.DA;
using PhoneBookMVC.Entity;
using PhoneBookMVC.Filters;
using PhoneBookMVC.Hasher;
using PhoneBookMVC.Models;
using PhoneBookMVC.ViewModels.UsersVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Controllers
{
    public class AccountController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (AuthenticationManager.LoggedUser != null)
            {
                return Redirect("~/Account/Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(UsersLoginVM model)
        {
            if (AuthenticationManager.LoggedUser != null)
            {
                return Redirect("~/Account/Index");
            }

            if (ModelState.IsValid)
            {
                AuthenticationManager.AuthenticateUser(model.Username, model.Password);
                if (AuthenticationManager.LoggedUser != null)
                {
                    // Successful log
                    return Redirect("/Contacts");
                }
                ModelState.AddModelError("", "Invalid data! Try again!");
            }

            // Unsuccessfull log
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (AuthenticationManager.LoggedUser != null)
            {
                return Redirect("~/Account/Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(UsersDetailsVM model)
        {
            if (AuthenticationManager.LoggedUser != null)
            {
                return Redirect("~/Account/Index");
            }

            if (ModelState.IsValid)
            {
                if (unitOfWork.UserRepository.GetByUsername(model.Username) != null)
                {
                    ModelState.AddModelError("Username", "Username Already Exists");
                    return View(model);
                }

                var passPhrase = PasswordHasher.Hash(model.Password);
                
                User user = new User();
                user.Email = model.Email;
                user.Role = Entity.User.UserRoleEnum.User;
                user.Username = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Hash = passPhrase.Hash;
                user.Salt = passPhrase.Salt;
                unitOfWork.UserRepository.Insert(user);
                unitOfWork.Save();

                return Redirect("Login");
            }
            return View(model);
        }

        [HttpGet]
        [AuthorizeUser]
        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            return Redirect("Login");
        }

        [HttpGet]
        [AuthorizeUser(Roles="admin")]
        public ActionResult ManageUsers()
        {
            UsersManageUsersVM model = new UsersManageUsersVM();
            model.Users = unitOfWork.UserRepository.GetAll().ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("ManageUsers");
            }

            User user = unitOfWork.UserRepository.GetById(id.Value);

            if (user == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            UsersEditVM model = new UsersEditVM();
            model.ID = user.ID;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Salt = user.Salt;
            model.Hash = user.Hash;
            model.Role = user.Role;
            model.Username = user.Username;
            model.Email = user.Email;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UsersEditVM model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.ID = model.ID;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Hash = model.Hash;
                user.Salt = model.Salt;
                user.Role = model.Role;
                user.Username = model.Username;
                user.Email = model.Email;

                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
                return RedirectToAction("ManageUsers");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("ManageUsers");
            }
           
            User user = unitOfWork.UserRepository.GetById(id.Value);

            if (user == null)
            {
                return Redirect("~/Error/PageNotFound");
            }

            UsersDetailsVM model = new UsersDetailsVM();
            model.ID = user.ID;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Hash = user.Hash;
            model.Salt = user.Salt;
            model.Role = user.Role;
            model.Username = user.Username;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(UsersDetailsVM model)
        {
            unitOfWork.UserRepository.Delete(model.ID);
            unitOfWork.Save();
            return RedirectToAction("ManageUsers");
        }
    }
}