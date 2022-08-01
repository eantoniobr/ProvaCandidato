using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Models;

namespace ProvaCandidato.Controllers
{
    public class CidadesController : SuperController<Cidade>
    {
        // GET: Cidades
        public ActionResult Index()
        {
            return View(db.Cidades.ToList());
        }

        // GET: Cidades/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var cidade = db.Cidades.Find(id);

            if (cidade == null)
                return HttpNotFound();

            return View(cidade);
        }

        // GET: Cidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cidades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cidade cidade)
        {
            if (!ModelState.IsValid)
                return View(cidade);

            db.Cidades.Add(cidade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Cidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Cidade cidade = db.Cidades.Find(id);

            if (cidade == null)
                return HttpNotFound();

            return View(cidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome")] Cidade cidade)
        {
            if(!ModelState.IsValid)
                return View(cidade);

                db.Entry(cidade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        // GET: Cidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Cidade cidade = db.Cidades.Find(id);

            if (cidade == null)
                return HttpNotFound();

            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cidade cidade = db.Cidades.Find(id);
            db.Cidades.Remove(cidade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
