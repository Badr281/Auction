using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vente.Models;

namespace vente.Controllers
{
    [Authorize(Roles = "Admins")]
    public class RolesController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        public EntityState EntryState { get; private set; }

        // GET: Roles
        public ActionResult Index()
        {

            return View(db.Roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(role);
            }

        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole roles)
        {
            try
            {



                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Roles.Add(roles);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else return View(roles);

            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {


            var role = db.Roles.Find(id);
            if (!ModelState.IsValid)
            {

                return HttpNotFound();
            }
            else
            {
                return View(role);


            }




        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,Name")] IdentityRole role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(role);
            }
            catch
            {
                return View();
            }
        }

        //GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
            public ActionResult Delete(IdentityRole myrole)
            {
            if (ModelState.IsValid)
            {
                IdentityRole role = db.Roles.Find(myrole.Id);
                db.Roles.Remove(role);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
                  return View(myrole);

                  
            }

            

        }
}

