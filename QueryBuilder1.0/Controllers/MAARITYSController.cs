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
    public class MAARITYSController : Controller
    {
        private QUERY_BILDER_DBEntities db = new QUERY_BILDER_DBEntities();

        // GET: MAARITYS
        public ActionResult Index()
        {
            var mAARITYS = db.MAARITYS.Include(m => m.VALINTAOPERAATTORI);
            return View(mAARITYS.ToList());
        }

        // GET: MAARITYS/Details/5
        public ActionResult Details(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAARITYS mAARITYS = db.MAARITYS.Find(id);
            if (mAARITYS == null)
            {
                return HttpNotFound();
            }
            return View(mAARITYS);
        }

        // GET: MAARITYS/Create
        public ActionResult Create()
        {
            ViewBag.VALINTA_ID = new SelectList(db.VALINTAOPERAATTORI, "ValintaOperaattori_ID", "VALINTAOPERAATTORI1");
            return View();
        }

        // POST: MAARITYS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MAARITYS_EHTO,VALINTA_ID")] MAARITYS mAARITYS)
        {
            if (ModelState.IsValid)
            {
                db.MAARITYS.Add(mAARITYS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VALINTA_ID = new SelectList(db.VALINTAOPERAATTORI, "ValintaOperaattori_ID", "VALINTAOPERAATTORI1", mAARITYS.VALINTA_ID);
            return View(mAARITYS);
        }

        // GET: MAARITYS/Edit/5
        public ActionResult Edit(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAARITYS mAARITYS = db.MAARITYS.Find(id);
            if (mAARITYS == null)
            {
                return HttpNotFound();
            }
            ViewBag.VALINTA_ID = new SelectList(db.VALINTAOPERAATTORI, "ValintaOperaattori_ID", "VALINTAOPERAATTORI1", mAARITYS.VALINTA_ID);
            return View(mAARITYS);
        }

        // POST: MAARITYS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MAARITYS_EHTO,VALINTA_ID")] MAARITYS mAARITYS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mAARITYS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VALINTA_ID = new SelectList(db.VALINTAOPERAATTORI, "ValintaOperaattori_ID", "VALINTAOPERAATTORI1", mAARITYS.VALINTA_ID);
            return View(mAARITYS);
        }

        // GET: MAARITYS/Delete/5
        public ActionResult Delete(double? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAARITYS mAARITYS = db.MAARITYS.Find(id);
            if (mAARITYS == null)
            {
                return HttpNotFound();
            }
            return View(mAARITYS);
        }

        // POST: MAARITYS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(double id)
        {
            MAARITYS mAARITYS = db.MAARITYS.Find(id);
            db.MAARITYS.Remove(mAARITYS);
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
