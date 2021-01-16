using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QueryBuilder1._0;

namespace QueryBuilder1._0.Controllers
{
    public class kayttajatsController : Controller
    {
        private QUERY_BILDER_DBEntities db = new QUERY_BILDER_DBEntities();

        // GET: kayttajats
        public ActionResult Index()
        {
            return View(db.kayttajat.ToList());
        }

        // GET: kayttajats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kayttajat kayttajat = db.kayttajat.Find(id);
            if (kayttajat == null)
            {
                return HttpNotFound();
            }
            return View(kayttajat);
        }

        // GET: kayttajats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: kayttajats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(kayttajat kayttajat, string kayttajaNimi)
        {
            if (ModelState.IsValid)
            {
                //bool onOlemassa = false;

                //var olemassaKauttaja = from x in db.kayttajat
                //                       where x.kayttaja_nimi.ToUpper() == kayttajaNimi.ToUpper()
                //                       select x.kayttaja_nimi;

                if (db.kayttajat.Any(u => u.kayttaja_nimi == kayttajaNimi) == false)
                {
                    kayttajat UusiKayttaja = new kayttajat();

                    UusiKayttaja.id = db.kayttajat.Max(u => u.id) + 1;
                    UusiKayttaja.kayttaja_nimi = kayttajaNimi;


                    db.kayttajat.Add(UusiKayttaja);
                    db.SaveChanges();
                    return RedirectToAction("Create", "kyselies");
                }
                else
                {
                    TempData["Huomautus2"] = "Samalla nimellä löytyy jo käyttäjä. Käytä olemassa olevaa käyttäjää tai anna uusi nimi!";

                    return RedirectToAction("Create");
                }

            }

            return View(kayttajat);
        }

        // GET: kayttajats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kayttajat kayttajat = db.kayttajat.Find(id);
            if (kayttajat == null)
            {
                return HttpNotFound();
            }
            return View(kayttajat);
        }

        // POST: kayttajats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,kayttaja_nimi")] kayttajat kayttajat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kayttajat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kayttajat);
        }

        // GET: kayttajats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kayttajat kayttajat = db.kayttajat.Find(id);
            if (kayttajat == null)
            {
                return HttpNotFound();
            }
            return View(kayttajat);
        }

        // POST: kayttajats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kayttajat kayttajat = db.kayttajat.Find(id);
            db.kayttajat.Remove(kayttajat);
            db.SaveChanges();
            return RedirectToAction("Index");
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
