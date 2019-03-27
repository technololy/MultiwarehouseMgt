using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultiwarehouseMgt;
using MultiwarehouseMgt.Models;

namespace MultiwarehouseMgt.Controllers
{
    public class NewRequestTablesController : Controller
    {
        private Entities db = new Entities();
        private Services s = new Services();


        // GET: NewRequestTables
        public ActionResult Index()
        {
            return View(db.NewRequestTables.ToList());
        }

        // GET: NewRequestTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewRequestTable newRequestTable = db.NewRequestTables.Find(id);
            if (newRequestTable == null)
            {
                return HttpNotFound();
            }
            return View(newRequestTable);
        }

        // GET: NewRequestTables/Create
        public ActionResult Create()
        {
            ViewBag.BookName = new SelectList(db.Books, "Name", "Name");
            return View();
        }

        public JsonResult GetBookLocation(string name)
        {

            Book b = s.GetBooksByBookName(name);

            List<StatesTable> l = s.GetStates();

            List<string> f = l.Where(x => x.Id == b.Location).Select(x => x.State).ToList();

            return Json(f, JsonRequestBehavior.AllowGet);
        }


        // POST: NewRequestTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookName,LocationName,Quantity,DateAdded,AddedBy")] NewRequestTable newRequestTable)
        {
            if (!s.IsQuantityOk(newRequestTable.BookName,newRequestTable.Quantity))
            {
                return RedirectToAction("Index");

            }
            newRequestTable.DateAdded = DateTime.Now;
            newRequestTable.AddedBy = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.NewRequestTables.Add(newRequestTable);
                db.SaveChanges();
                s.ReduceQuantity(newRequestTable.BookName, newRequestTable.Quantity);
                return RedirectToAction("Index");
            }

            return View(newRequestTable);
        }

        // GET: NewRequestTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewRequestTable newRequestTable = db.NewRequestTables.Find(id);
            if (newRequestTable == null)
            {
                return HttpNotFound();
            }
            return View(newRequestTable);
        }

        // POST: NewRequestTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookName,LocationName,Quantity,DateAdded,AddedBy")] NewRequestTable newRequestTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newRequestTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newRequestTable);
        }

        // GET: NewRequestTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewRequestTable newRequestTable = db.NewRequestTables.Find(id);
            if (newRequestTable == null)
            {
                return HttpNotFound();
            }
            return View(newRequestTable);
        }

        // POST: NewRequestTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewRequestTable newRequestTable = db.NewRequestTables.Find(id);
            db.NewRequestTables.Remove(newRequestTable);
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
