using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEx2.Models;

namespace MVCEx2.Controllers
{
	public class 客戶資料ViewController : Controller
	{
		private 客戶資料ViewRepository rps = RepositoryHelper.Get客戶資料ViewRepository();

		// GET: 客戶資料View
		public ActionResult Index()
		{
			return View(rps.All());
		}

		// GET: 客戶資料View/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			客戶資料View 客戶資料View = rps.FindByKey(id);

			if (客戶資料View == null)
			{
				return HttpNotFound();
			}
			return View(客戶資料View);
		}



		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				rps.DBContex.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
