using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Pages.Components.PageHeader;

public class PageHeaderViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string pageName)
    {
        return View("", pageName);
    }
}
