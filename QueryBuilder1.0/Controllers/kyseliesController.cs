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
    public class kyseliesController : Controller
    {
        private QUERY_BILDER_DBEntities db = new QUERY_BILDER_DBEntities();

        // GET: kyselies
        public ActionResult Index()
        {
            return View(db.kysely.ToList());
        }

        // GET: kyselies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kysely kysely = db.kysely.Find(id);
            if (kysely == null)
            {
                return HttpNotFound();
            }
            return View(kysely);
        }

        // GET: kyselies/Create
        public ActionResult Create()
        {
            ViewBag.Valintaoperaattori = new SelectList(db.VALINTAOPERAATTORI, "ValintaOperaattori_ID", "VALINTAOPERAATTORI1");
            ViewBag.Maaritys = new SelectList(db.MAARITYS, "ID", "MAARITYS_EHTO");

            ViewBag.Ehto = new SelectList(db.EHTO, "ID", "EHTO1");

            ViewBag.Operaattori = new SelectList(db.OPERAATTORI, "ID", "OPERAATTORI1");


            return View();
        }

        // POST: kyselies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(kysely kysely)
        {
            if (ModelState.IsValid)
            {
                //Valintaoperaattorin muodostus

                int kayttajanValintaString = int.Parse(Request.Form["ValintaOperaattori_ID"]);

                int kayttajanValinta = db.VALINTAOPERAATTORI.Where(a => a.ValintaOperaattori_ID == kayttajanValintaString)
                .Select(a => (int)a.ValintaOperaattori_ID).FirstOrDefault();

                string valinta = db.SQL_VALINTAOPERAATTORI.Where(a => a.ValintaOperaattori_ID == kayttajanValinta)
                .Select(a => a.SQL).FirstOrDefault();

                //Määritys
                int kayttajanMaaritysString= int.Parse(Request.Form["Maaritys_ID"]);

                int kayttajanMaaritys = db.MAARITYS.Where(a => a.ID == kayttajanMaaritysString)
                .Select(a => (int)a.ID).FirstOrDefault();

                string maaritys = db.SQL_MAARITYS.Where(a => a.SQLValintaOperaattori_ID == kayttajanMaaritys)
                .Select(a => a.SQL_MAARITYS1).FirstOrDefault();


                //Ehto
                int kayttajanEhtoString = int.Parse(Request.Form["Ehto_ID"]);

                int kayttajanEhto = db.EHTO.Where(a => a.ID == kayttajanEhtoString)
                .Select(a => (int)a.ID).FirstOrDefault();

                string ehto = db.SQL_EHTO.Where(a => a.EHTO_ID == kayttajanEhto)
                .Select(a => a.SQL_EHTO1).FirstOrDefault();


                //Operaattori
                int kayttajanOperaattoriString = int.Parse(Request.Form["Operaattori_ID"]);

                int kayttajanOperaattori = db.OPERAATTORI.Where(a => a.ID == kayttajanOperaattoriString)
                .Select(a => (int)a.ID).FirstOrDefault();

                string operaattori = db.SQL_OPERAATTORI.Where(a => a.OPERAATTORI_ID == kayttajanOperaattori)
                .Select(a => a.SQL_OPERAATTORI1).FirstOrDefault();

              

                //Kyselyn muodostus

                string query = valinta + " " + maaritys + " " + "FROM" + " " + ehto + " " + operaattori;



                // Luodaan kyselystä uusi instanssi kysely -luokkaan
                kysely KäyttäjänKysely = new kysely();

                KäyttäjänKysely.id = db.kysely.Max(u => u.id) + 1;
                KäyttäjänKysely.kysely1 = query;


                // Tallennetaan instanssi modelin kysely -luokkaan (tauluun)
                db.kysely.Add(KäyttäjänKysely);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kysely);
        }

        // GET: kyselies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kysely kysely = db.kysely.Find(id);
            if (kysely == null)
            {
                return HttpNotFound();
            }
            return View(kysely);
        }

        // POST: kyselies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,kysely1")] kysely kysely)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kysely).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kysely);
        }

        // GET: kyselies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kysely kysely = db.kysely.Find(id);
            if (kysely == null)
            {
                return HttpNotFound();
            }
            return View(kysely);
        }

        // POST: kyselies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kysely kysely = db.kysely.Find(id);
            db.kysely.Remove(kysely);
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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public string LuoKysely ()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Valintaoperaattorin muodostus

        //        string kayttajanValintaString = Request.Form["ValintaOperaattori_ID"];

        //        int kayttajanValinta = db.VALINTAOPERAATTORI.Where(a => a.VALINTAOPERAATTORI1 == kayttajanValintaString)
        //        .Select(a => (int)a.ValintaOperaattori_ID).FirstOrDefault();

        //        string valinta = db.SQL_VALINTAOPERAATTORI.Where(a => a.ValintaOperaattori_ID == kayttajanValinta)
        //        .Select(a => a.SQL).FirstOrDefault();



        //        //Kyselyn muodostus

        //        string query = valinta;

        //        return valinta;
        //    }
        //}
    }
}
