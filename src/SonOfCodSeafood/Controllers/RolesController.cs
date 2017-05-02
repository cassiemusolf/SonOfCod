using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SonOfCodSeafood.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using SonOfCodSeafood.ViewModels;

namespace SonOfCodSeafood.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_db.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
            {
            _db.Roles.Add(new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole(Request.Form["RoleName"]));
            _db.SaveChanges();
            return RedirectToAction("Index");
            }

        public IActionResult Edit(string roleName)
        {
            ViewData["roleName"] = roleName;
            return View();
        }
        [HttpPost]
        public IActionResult Edit()
        {
            var role = _db.Roles.FirstOrDefault(m => m.Name == Request.Form["role-name"]);
            role.Name = Request.Form["edit-role"];
            _db.Roles.Update(role);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //[Authorize]
        //public IActionResult Assign()
        //{
        //    ViewBag.Users = new SelectList(_userManager.Users.ToList());
        //    return View();
        //}

        //[AllowAnonymous]
        //public IActionResult AddToUser(string username, string roleName)
        //{
        //    var user = GetUser(username);
        //    var thing = _userManager.AddToRoleAsync(user, roleName).Result;
        //    return View("Index");
        //}

        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Admin")]
        //public IActionResult GetRoles(string username)
        //{
        //    var user = GetUser(username);
        //    ViewBag.User = user;
        //    var userRoles = _userManager.GetRolesAsync(user).Result;
        //    ViewBag.RolesForThisUser = userRoles;
        //    var preroles = _db.Roles.ToList();
        //    var roles = new List<string>();
        //    foreach (var r in preroles)
        //    {
        //        var add = true;
        //        foreach (var userRole in userRoles)
        //        {
        //            if (userRole == r.Name)
        //            {
        //                add = false;
        //            }
        //        }
        //        if (add)
        //        {
        //            roles.Add(r.Name);
        //        }
        //    }
        //    ViewBag.Users = new SelectList(_userManager.Users.ToList());
        //    ViewBag.Roles = roles;
        //    return View("Assign");
        //}

        //public ApplicationUser GetUser(string username)
        //{
        //    return _userManager.Users.FirstOrDefault(m => m.UserName == username);
        //}

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Assign()
        {
            ViewBag.Name = new SelectList(_db.Roles.ToList(), "Name", "Name");
            ViewBag.UserName = new SelectList(_db.Users.ToList(), "UserName", "UserName");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(RegisterViewModel model, ApplicationUser UserName)
        {
            var userId = _db.Users.Where(i => i.UserName == UserName.UserName).Select(s => s.Id);
            //string updateId = "";
            //foreach (var i in userId)
            //{
            //    updateId = i.ToString();
            //}

            await this._userManager.AddToRoleAsync(UserName, model.RoleId);

            return RedirectToAction("Index", "Home");
        }
    }
}
