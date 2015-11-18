using System;
using System.Linq;
using System.Collections.Generic;

namespace MVCEx2.Models
{
	public class 客戶資料ViewRepository : EFRepository<客戶資料View>, I客戶資料ViewRepository
	{
		public override IQueryable<客戶資料View> All()
		{
			return base.All().OrderByDescending(x => x.刪除註記).OrderBy(x => x.Id);
		}

		public 客戶資料View FindByKey(int? id) 
		{
			return this.All().FirstOrDefault(x => x.Id == id);
		}

	}

	public interface I客戶資料ViewRepository : IRepository<客戶資料View>
	{

	}
}