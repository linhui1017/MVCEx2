using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCEx2
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "CustomerRoute",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "客戶資料View", action = "Index", id = UrlParameter.Optional },
				 constraints: new
				 {
					 controller = "客戶資料View"

				 }
			);
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);


		}
	}
}
