using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProvaCandidato.Controllers.Base;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Helper;

namespace ProvaCandidato.Controllers
{
    public class ClientesController : BaseController<ClientesController>
    {
        private ContextoPrincipal db = new ContextoPrincipal();

        private ClientesController clienteController;

        public ClientesController(ClientesController controller) : base(controller)
        {
            clienteController = controller;
        }
        public ClientesController() { }

        // GET: Clientes
        public ActionResult Index()
        {
            ViewBag.NomeEmpresa = ConfigurationManager.AppSettings["NomeEmpresa"];
            var clientes = db.Clientes.Include(c => c.Cidade).Where(c => c.Ativo == true);
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            cliente.ClienteObservacoes = db.ClienteObservacoes.Where(obs => obs.ClienteCodigo == cliente.Codigo).ToList();
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome");
            return View();
        }

        // POST: Clientes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            if (cliente.DataNascimento > DateTime.Now)
                ModelState.AddModelError("DataNascimento", "Data de nascimento não pode ser maior que a data de hoje.");

            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Cliente criado com sucesso");

                return RedirectToAction("Index");
            }

            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);

            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Cliente editado com sucesso");

                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            MessageHelper.DisplaySuccessMessage(this, "Cliente deletado com sucesso");

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

        public PartialViewResult AddUserPartialView(int? id)
        {
          
            Cliente cliente = db.Clientes.Find(id);
          
            return PartialView("ClientObservation", new ClienteObservacao { Cliente = cliente, ClienteCodigo = cliente.Codigo } );
        }
        [HttpPost]
        public ActionResult AddUserInfo([Bind(Include = "ClienteCodigo,Observacao")]  ClienteObservacao request)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                db.ClienteObservacoes.Add(request);
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Observação adicionada com sucesso");

                return RedirectToAction("Index");
            }
            return Json(new { result = isSuccess, responseText = "OK" });

        }
    }
}
