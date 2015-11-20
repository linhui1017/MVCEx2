using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCEx2.Models
{
	public class 客戶分類Repository : EFRepository<客戶分類>, I客戶分類Repository
	{

	}

	public interface I客戶分類Repository : IRepository<客戶分類>
	{

	}
}