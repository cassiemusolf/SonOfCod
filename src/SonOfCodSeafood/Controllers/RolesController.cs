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

namespace SonOfCodSeafood.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _db;


        public RolesController(ApplicationDbContext db)
        {
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
    }
    }
