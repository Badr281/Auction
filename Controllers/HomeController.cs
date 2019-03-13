using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using vente.Models;

namespace vente.Controllers
{
    public class HomeController : Controller
    {
        private IdentityDBEntities db = new IdentityDBEntities();

        // GET: Articles
        // Get data by protocol HttpPost
        public ActionResult Index(string Search, int Ca =1 )
        {

            string c = System.Web.HttpContext.Current.User.Identity.GetUserId();
            
      
            var test = (from s in db.Articles
                        where (s.UserId == c)
                        where (s.titre.Contains(Search))
                        
                       
                            select s
                           ).Distinct().ToList();
                ViewBag.Ca = new SelectList(db.Categories, "Id", "libelle");
                return View(test.ToList());          
        }

        // GET: Articles/Details/5
        // Detail of product
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
    
        public ActionResult Create()
        {
            db = new IdentityDBEntities();
            ViewBag.Idc = new SelectList(db.Categories, "Id", "libelle");
            //ViewBag.Idc = new SelectList(db.Categories, "Id", "libelle");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        // Create new article using HttpPost (Bind => Binding field on view)
        public ActionResult Create([Bind(Include = "Ida,description,image,Userid,Idc,titre")] Article article, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {

                if (image != null)
                {
                    article.image = image.FileName;
                }

                db = new IdentityDBEntities();
                // Add article to database  
                article.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                article.Idc = Convert.ToInt32(Request["Idc"]);
                db.Articles.Add(article);
                ViewBag.Idcc = new SelectList(db.Categories, "Id", "libelle");
                db.SaveChanges();
                return RedirectToAction("Index");


            }

            return View(article);



        }



        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idc = new SelectList(db.Categories, "Id", "libelle", article.Idc);

            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Edit current product by using EntryStade
        public ActionResult Edit([Bind(Include = "Ida,description,UserId,Idc,titre,image")] Article article, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    article.image = image.FileName;
                }
                try
                {
                    article.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    db.Entry(article).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception e) { Response.Write(e.Message); }
            }
            ViewBag.Idc = new SelectList(db.Categories, "Id", "libelle");

            return View(article);
        }

    }
}
