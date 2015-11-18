using MVCEx2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEx2.Controllers
{
	public class 客戶聯絡人Controller : Controller
	{
		private 客戶聯絡人Repository rps = RepositoryHelper.Get客戶聯絡人Repository();

		// GET: 客戶聯絡人
		public ActionResult IndexByClient(int? id)
		{
			IEnumerable<客戶聯絡人> result = null;
			ViewBag.Title = string.Empty;
			客戶資料 client = null;

			if (null != id)
			{
				client = rps.DBContex.客戶資料.Find(id);
			}
			if (null != client)
			{

				ViewBag.Title = (null == client ? string.Empty : client.客戶名稱);

				ViewBag.ClientID = id;

				result = client.客戶聯絡人.Where(x => x.客戶Id == id && x.刪除註記 == false);

			}
			else
			{
				return RedirectToAction("Index", "客戶資料View");
			}
			ViewBag.Title = string.Format("{0}{1}{2}"
				, ViewBag.Title
				, (string.IsNullOrEmpty(ViewBag.Title) ? "" : "--")
				, "聯絡人資料");

			return View(result.ToList());
		}


		public ActionResult CreateByClient(int? id)
		{
			ViewBag.客戶Id = new SelectList(rps.DBContex.客戶資料.Where(x => x.Id == id), "Id", "客戶名稱");
			ViewBag.ClientID = id;
			return View();
		}

		// POST: 客戶聯絡人/Create
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateByClient(客戶聯絡人 客戶聯絡人)
		{

			if (ModelState.IsValid)
			{
				rps.Add(客戶聯絡人);
				rps.Commit();

				return RedirectToAction("IndexByClient", "客戶聯絡人", new { id = 客戶聯絡人.客戶Id });
			}
 			ViewBag.客戶Id = new SelectList(rps.DBContex.客戶資料.Where(x => x.Id == 客戶聯絡人.客戶Id), "Id", "客戶名稱");
			ViewBag.ClientID = 客戶聯絡人.客戶Id;
			return View(客戶聯絡人);
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