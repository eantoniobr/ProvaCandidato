using ProvaCandidato.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProvaCandidato.Models
{
    public class SuperController<T> : Controller
    {
        public ContextoPrincipal db = new ContextoPrincipal();


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