using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultiwarehouseMgt;

namespace MultiwarehouseMgt.Controllers
{
    public class RequestTablesController : Controller
    {
        private Entities db = new Entities();

        // GET: RequestTables
        public ActionResult Index()
        {
            var requestTables = db.RequestTables.Include(r => r.Book).Include(r => r.StatesTable);
            return View(requestTables.ToList());
        }

        // GET: RequestTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestTable requestTable = db.RequestTables.Find(id);
            if (requestTable == null)
            {
                return HttpNotFound();
            }
            return View(requestTable);
        }

        // GET: RequestTables/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Books, "Id", "Name");
            ViewBag.Id = new SelectList(db.StatesTables, "Id", "State");
            return View();
        }

        // POST: RequestTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookName,Location,RequestedBy,Daterequested")] RequestTable requestTable)
        {
            if (ModelState.IsValid)
            {
                db.RequestTables.Add(requestTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Books, "Id", "Name", requestTable.Id);
            ViewBag.Id = new SelectList(db.StatesTables, "Id", "State", requestTable.Id);
            return View(requestTable);
        }

        // GET: RequestTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestTable requestTable = db.RequestTables.Find(id);
            if (requestTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Books, "Id", "Name", requestTable.Id);
            ViewBag.Id = new SelectList(db.StatesTables, "Id", "State", requestTable.Id);
            return View(requestTable);
        }

        // POST: RequestTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookName,Location,RequestedBy,Daterequested")] RequestTable requestTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Books, "Id", "Name", requestTable.Id);
            ViewBag.Id = new SelectList(db.StatesTables, "Id", "State", requestTable.Id);
            return View(requestTable);
        }

        // GET: RequestTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestTable requestTable = db.RequestTables.Find(id);
            if (requestTable == null)
            {
                return HttpNotFound();
            }
            return View(requestTable);
        }

        // POST: RequestTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestTable requestTable = db.RequestTables.Find(id);
            db.RequestTables.Remove(requestTable);
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
