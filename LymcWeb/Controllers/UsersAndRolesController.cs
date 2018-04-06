using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LymcWeb.Data;
using LymcWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LymcWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersAndRolesController : Controller
    {

        private ApplicationDbContext db;

        private UserManager<ApplicationUser> userManager;


        public UsersAndRolesController(ApplicationDbContext _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
        // GET: Management
        public IActionResult Index()
        {
            return View();
        }

        // GET: Roles
        public IActionResult IndexRoles()
        {
            return View(db.Roles.ToList());
        }

        // Get: User & Roles
        public IActionResult IndexUsers()
        {
            ViewBag.Roles = db.Roles.ToList();
            var users = db.Users.ToList();
           
            foreach (var user in users)
            {
                user.RoleNames = userManager.GetRolesAsync(user).Result;
            }

            return View(users);
        }

        // GET: Role/Create
        public IActionResult AddRoleToUser(string id)
        {
            ViewBag.roles = db.Roles.ToList();
            AddRoleToUserViewModel viewModel = new AddRoleToUserViewModel()
            {
                UserId = id,
                RoleName = null
            };
            return View(viewModel);
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser([Bind(include: "UserId, RoleName")] AddRoleToUserViewModel viewModel)
        {
            var uId = viewModel.UserId;
            var rName = viewModel.RoleName;
            if (ModelState.IsValid)
            {
                var user = userManager.FindByIdAsync(uId).Result;
                var result = await userManager.AddToRoleAsync(user, rName);

                
                db.SaveChanges();

               
                return RedirectToAction("EditUser", new { id = viewModel.UserId });
            }

            return View();
        }

        // GET: Role/Create
        public IActionResult CreateRole()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRole([Bind(include: "Id, Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("IndexRoles");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public IActionResult EditRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRole([Bind(include: "Id, Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var dbRole = db.Roles.Where(r => r.Id == role.Id).FirstOrDefault();

                db.Roles.Attach(dbRole);
                dbRole.Name = role.Name;
                db.SaveChanges();
                return RedirectToAction("IndexRoles");
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public IActionResult EditUser(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ApplicationUser user = db.Users.Find(id);
            user.RoleNames = userManager.GetRolesAsync(user).Result;

            if (user == null)
            {
                return NotFound();
            }
            
            return View(user);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser([Bind(include: "Id,Roles")] IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexUsers");
            }
            return View(user);
        }

        // GET: role/Delete/5
        public async Task<IActionResult> DeleteRoleFromUser(string uId, string roleId)
        {
            if (uId == null || roleId == null)
            {
                return BadRequest();
            }

            var user = db.Users.Where(u => u.Id == uId).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            await userManager.RemoveFromRoleAsync(user, 
                db.Roles.Where(r => r.Name == roleId).FirstOrDefault().Name);

            db.SaveChanges();

            return RedirectToAction("EditUser", new { id = uId });
        }

        // GET: role/Delete/5
        public IActionResult DeleteRole(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Boats/Delete/5
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            IdentityRole role = db.Roles.Find(id);
            if (role.Name == "Admin")
            {
                ModelState.AddModelError("Admin", "Admin cannot be removed.");
            }
            else
            {
                db.Roles.Remove(role);
                db.SaveChanges();
            }
            return RedirectToAction("IndexRoles");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}