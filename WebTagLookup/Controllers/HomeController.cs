using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTagLookup.Models;

namespace WebTagLookup.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Pet License Information Database";
            return View();
        }

        [HttpPost]
        public ActionResult TagSearch(TagLookup tl)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                ViewData.Model = tl.getTagInfo();

                return PartialView("_TagInfo");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}