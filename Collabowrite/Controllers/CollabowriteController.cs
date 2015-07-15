using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collabowrite.Controllers
{
    public class CollabowriteController : Controller
    {
        // GET: Collabowrite
        public ActionResult Index()
        {
            return RedirectToAction("Session", new { id = Guid.NewGuid() });
        }

        public ActionResult Session(Guid id)
        {
            return View(id);
        }
    }
}