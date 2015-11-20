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
		//private CustomerEntities db = new CustomerEntities();

		客戶資料ViewRepository rps = RepositoryHelper.Get客戶資料ViewRepository();

		#region  客戶資料View

		// GET: 客戶資料View
		public ActionResult Index()
		{
			return View(rps.All());
		}

		public ActionResult Create()
		{
			ViewBag.客戶分類Id = new SelectList(rps.DBContex.客戶分類, "Id", "分類名稱", 0);
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,刪除註記,客戶分類Id")] 客戶資料 客戶資料)
		{
			if (ModelState.IsValid)
			{
				rps.Add客戶資料(客戶資料);
				rps.Commit();
				return RedirectToAction("Index");
				
			}
			ViewBag.客戶分類Id = new SelectList(rps.DBContex.客戶分類, "Id", "分類名稱", 0);
			return View();
		}

		public ActionResult Edit(int? id)
		{
			客戶資料 customer = rps.Find客戶資料(id);
			if (null != customer) 
			{
				ViewBag.客戶分類Id = new SelectList(rps.DBContex.客戶分類, "Id", "分類名稱", customer.客戶分類Id);
			}
			else
			{
				return HttpNotFound();
			}
			return View(customer);
		}

		// POST: 客戶資料/Edit/5
		// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
		// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,刪除註記,客戶分類Id")] 客戶資料 客戶資料)
		{
			if (ModelState.IsValid)
			{
				rps.Update客戶資料(客戶資料);
				rps.Commit();
				return RedirectToAction("Index", "客戶資料View");
				//return RedirectToAction("Index");
			}
			ViewBag.客戶分類Id = new SelectList(rps.DBContex.客戶分類, "Id", "分類名稱", 客戶資料.客戶分類Id);
			return View(客戶資料);
		}
		#endregion

		#region 客戶聯絡人

		public ActionResult 客戶聯絡人清單(int? clientId)
		{
			客戶資料 client = rps.Find客戶資料(clientId);
			if (null == client)
			{
				return HttpNotFound();
				
			}
			else 
			{
				ViewBag.ClientID = client.Id;
				ViewBag.ClientName = client.客戶名稱;
				return View(rps.Find客戶聯絡人清單(clientId));
			}
		}

		public ActionResult 新增客戶聯絡人(int? clientId)
		{
			客戶資料 client = rps.Find客戶資料(clientId);
			if (null == client)
			{
				return HttpNotFound();
			}
			else
			{

				ViewBag.客戶Id = new SelectList(rps.DBContex.客戶資料.Where(x => x.Id == clientId), "Id", "客戶名稱");
				ViewBag.ClientID = client.Id;
				ViewBag.ClientName = client.客戶名稱;
			}
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult 新增客戶聯絡人([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,刪除註記")] 客戶聯絡人 客戶聯絡人)
		{

			if (ModelState.IsValid)
			{
				rps.Add客戶聯絡人(客戶聯絡人);
				rps.Commit();
				return RedirectToAction("客戶聯絡人清單", new { clientId = 客戶聯絡人.客戶Id });

			}
			ViewBag.客戶Id = new SelectList(rps.DBContex.客戶資料.Where(x => x.Id == 客戶聯絡人.客戶Id), "Id", "客戶名稱");
			ViewBag.ClientID = 客戶聯絡人.客戶Id;
			ViewBag.ClientName = 客戶聯絡人.客戶資料.客戶名稱;
			return View(客戶聯絡人);
		}

		public ActionResult 編輯客戶聯絡人(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶聯絡人 客戶聯絡人 = rps.Find客戶聯絡人(id);
			if (客戶聯絡人 == null)
			{
				return HttpNotFound();
			}
			ViewBag.客戶Id = new SelectList(rps.DBContex.客戶資料.Where(x => x.Id == 客戶聯絡人.客戶Id), "Id", "客戶名稱");
			ViewBag.ClientID = 客戶聯絡人.客戶Id;
			ViewBag.ClientName = 客戶聯絡人.客戶資料.客戶名稱; 
			return View(客戶聯絡人);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult 編輯客戶聯絡人([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,刪除註記")] 客戶聯絡人 客戶聯絡人)
		{
			if (ModelState.IsValid)
			{
				rps.Update客戶聯絡人(客戶聯絡人);
				rps.Commit();
				return RedirectToAction("客戶聯絡人清單", "客戶資料View", new { clientId = 客戶聯絡人.客戶Id});
				//return RedirectToAction("Index");
			}
			ViewBag.客戶Id = new SelectList(rps.DBContex.客戶資料.Where(x => x.Id == 客戶聯絡人.客戶Id), "Id", "客戶名稱");
			ViewBag.ClientID = 客戶聯絡人.客戶Id;
			ViewBag.ClientName = rps.Find客戶資料(客戶聯絡人.客戶Id).客戶名稱; 
			return View(客戶聯絡人);
		}

		public ActionResult 刪除客戶聯絡人(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			客戶聯絡人 客戶聯絡人 = rps.Find客戶聯絡人(id);
			if (客戶聯絡人 == null)
			{
				return HttpNotFound();
			}
			ViewBag.客戶Id = new SelectList(rps.DBContex.客戶資料.Where(x => x.Id == 客戶聯絡人.客戶Id), "Id", "客戶名稱");
			ViewBag.ClientID = 客戶聯絡人.客戶Id;
			ViewBag.ClientName = 客戶聯絡人.客戶資料.客戶名稱; 
			return View(客戶聯絡人);
		}

		#endregion

		#region Instance Level

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				rps.DBContex.Dispose();
			}
			base.Dispose(disposing);
		}
		
		#endregion

	}
}
