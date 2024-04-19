using Jobportalproject.BusinessLayer;
using Jobportalproject.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jobportalproject.Controllers
{
    public class SuperAdminController : Controller
    {
        static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        string AuthApiKey = configuration.GetSection("AuthApiKey").GetSection("AuthApiKey").Value;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddJobDetails()
        {
            
            return View();
        }
        public ActionResult jobdetailsInsert(jobobjects jobobj)
        {
            string encryptKey = EncryptDecryptBL.Encrypt(AuthApiKey);
            string Result = JobportalApi.AddJobdetails(jobobj, encryptKey);
            if(Result.ToString()!= "AccessDenied")
            {
                return RedirectToAction("Index", "SuperAdmin");
            }
            return Ok("Error");
        }

    }
}
