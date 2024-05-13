using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Oakell.Controllers;

public class HomeController : AbpController
{
    public ActionResult Index()
    {
        // If you don't want to redirect to Swagger by default, comment out or remove the following line.
        // return Redirect("~/swagger");
        
        // Uncomment the following line if you want to return a view or another form of response.
        return Content("Welcome to the Oakell API!");
    }
}

