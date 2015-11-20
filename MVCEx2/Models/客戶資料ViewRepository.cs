using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using Omu.ValueInjecter;

namespace MVCEx2.Models
{
	public class 客戶資料ViewRepository : EFRepository<客戶資料View>, I客戶資料ViewRepository
	{
		public override IQueryable<客戶資料View> All()
		{
			return base.All().OrderBy(x => x.Id).OrderBy(x => x.刪除註記);
		}

		public 客戶資料View FindByKey(int? id) 
		{
			return this.All().FirstOrDefault(x => x.Id == id);
		}

		public void Add客戶資料(客戶資料 data) 
		{

			this.DBContex.客戶資料.Add(data);
		
		}

		public void Add客戶聯絡人(客戶聯絡人 data)
		{

			this.DBContex.客戶聯絡人.Add(data);

		}

		public 客戶資料 Find客戶資料(int? id) 
		{
			return this.DBContex.客戶資料.Find(id);
		}

		public void Update客戶資料(客戶資料 data)
		{
			if (null != data) 
			{

				客戶資料 dbItem = Find客戶資料(data.Id);
				if (null != dbItem) 
				{
					dbItem.InjectFrom(data);
				
				
				}
			}
		}

		public List<客戶聯絡人> Find客戶聯絡人清單(int? clientId) 
		{
			return this.DBContex.客戶聯絡人.Where(x => x.客戶Id == clientId
				& x.刪除註記 == false).ToList();
		}
	
		public 客戶聯絡人 Find客戶聯絡人(int? id)
		{
			return this.DBContex.客戶聯絡人.Find(id);
		}


		public void Update客戶聯絡人(客戶聯絡人 data)
		{
			if (null != data)
			{

				客戶聯絡人 dbItem = Find客戶聯絡人(data.Id);
				if (null != dbItem)
				{
					dbItem.InjectFrom(data);


				}
			}
		}

	}

	public interface I客戶資料ViewRepository : IRepository<客戶資料View>
	{

	}
}