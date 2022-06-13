using System.Web;
using System.Web.Mvc;

namespace KiemTra_Trần_Tiến_An
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
