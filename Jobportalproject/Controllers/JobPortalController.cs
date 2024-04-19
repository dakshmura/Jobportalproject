using Jobportalproject.BusinessLayer;
using Jobportalproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.Text;

namespace Jobportalproject.Controllers
{
    public class JobPortalController : Controller
    {
        static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        string AuthApiKey = configuration.GetSection("AuthApiKey").GetSection("AuthApiKey").Value;
        static string JobPortalAPIURl = configuration.GetSection("JobPortalAPIURl").GetSection("JobPortalAPIURl").Value;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserRegistration()
        {
            return View();
        }
        public IActionResult UserLogin()
        {
            return View();
        }
       
        public async Task<IActionResult> Jobavilablepage()
        {
            try
            {
                List<jobobjects> secondstep = new List<jobobjects>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(JobPortalAPIURl + "JobPortal/ListJobInfo"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        if (apiResponse.ToString() != "No Records Found")
                        {
                            secondstep = JsonConvert.DeserializeObject<List<jobobjects>>(apiResponse);
                            TempData["showtable"] = "showing table";
                        }
                        else
                        {
                            TempData["msg"] = "No Records Found";
                        }
                    }
                }
                return View(secondstep);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

            [HttpPost]
            public IActionResult Login(UserObjects userObj)
            {
                string result = JobportalApi.UserLogin(userObj, AuthApiKey);
                if (result != "UserName or Password Wrong" && result != "AccessDenied")
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Username or password is incorrect" });
                }
            }
        
        [HttpPost]
        public JsonResult UserRegistration(UserObjects userObj)
        {
            string encryptKey = EncryptDecryptBL.Encrypt(AuthApiKey);
            string Result=JobportalApi.UserRegistration(userObj,encryptKey);
            if(Result.ToString()!= "AccessDenied")
            {
                return Json("Success");
            }
            return Json("Error");
        }
        public ActionResult EmailIdCheck(UserObjects userObj)
        {
            string encryptKey = EncryptDecryptBL.Encrypt(AuthApiKey);
            string RESULT = JobportalApi.Emailidcheck(userObj,encryptKey);
            
                if (RESULT.ToString() == "true")
                {
                    return Json("true");
                }
            else
            {
                return Json("false");
            }
            
           
        }
      

    }
}
