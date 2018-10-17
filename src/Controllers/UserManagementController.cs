using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingNet.Models.Views;
using TrainingNet.Repositories.Interfaces;

namespace TrainingNet.Controllers
{
    [Route("[controller]"), Authorize]
    public class UserManagementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public UserManagementController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        [HttpGet("Users")]
        public ActionResult Users()
        {
            try
            {
                var users = UnitOfWork.ApplicationUserRepository.GetAll();
                if (users == null)
                    throw new NullReferenceException();
                var usersVM = users.Select(m => new UserViewModel
                {
                    UserName = m.UserName,
                    Email = m.Email,
                }).ToList();
                return View(usersVM);
            }
            catch(NullReferenceException)
            {
                return RedirectToAction("Users", "UserManagement");
            }
        }
    }
}
