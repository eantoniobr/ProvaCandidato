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
using ProvaCandidato.Helper;
using ProvaCandidato.Models;

namespace ProvaCandidato.Controllers
{
    public class ClientesController : SuperController<Cliente>
    {
        // GET: Clientes
        public ActionResult Index(string pesquisar = null)
        {
            ViewBag.Pesquisar = pesquisar;

            var clientes = db.Clientes.Include(c => c.Cidade)
                .Where(c => c.Ativo); //Exibe somente os ativos

            if (!string.IsNullOrEmpty(pesquisar))
            {
                //Filtra clientes
                clientes = clientes.Where(c =>
                c.Nome.ToLower().Contains(pesquisar.ToLower()) //Pesquisa por Nome
                || c.Cidade.Nome.ToLower().Contains(pesquisar.ToLower()) //Pesquisa por Cidade
                );
            }

            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Cliente cliente = db.Clientes.Find(id);

            if (cliente == null)
                return HttpNotFound();

            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            if (cliente.DataNascimento.GetValueOrDefault() > DateTime.Now.Date)
            {
                ModelState.AddModelError(nameof(cliente.DataNascimento), "A data de nascimento não deve ser superior à data atual");
                return View(cliente);
            }

            cliente.Ativo = true;

            db.Clientes.Add(cliente);
            db.SaveChanges();

            MessageHelper.DisplayMessage(this, "Cliente cadastrado com sucesso!", success: true);

            return RedirectToAction("Index");
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Cliente cliente = db.Clientes.Find(id);

            if (cliente == null)
                return HttpNotFound();

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            if (cliente.DataNascimento.GetValueOrDefault() > DateTime.Now.Date)
            {
                ModelState.AddModelError(nameof(cliente.DataNascimento), "A data de nascimento não deve ser superior à data atual");
                return View(cliente);
            }

            db.Entry(cliente).State = EntityState.Modified;
            db.SaveChanges();

            MessageHelper.DisplayMessage(this, "Cliente alterado com sucesso!", success: true);

            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Cliente cliente = db.Clientes.Find(id);

            if (cliente == null)
                return HttpNotFound();

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);

            //Por boas práticas, não é recomendável deletar um registro do banco de dados,
            //o mesmo deve ser inativado para que possa se manter em histórico e rastreável
            cliente.Ativo = false;

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult GetCidadesOptions()
        {
            var model = new SelectList(db.Cidades, "Codigo", "Nome");
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
