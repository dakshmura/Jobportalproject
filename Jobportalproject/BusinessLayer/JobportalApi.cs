using Jobportalproject.Models;
using Nancy.Json;
using System.Net;
using System.Text;

namespace Jobportalproject.BusinessLayer
{
    public class JobportalApi
    {
        static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        static string JobPortalAPIURl = configuration.GetSection("JobPortalAPIURl").GetSection("JobPortalAPIURl").Value;
        public static string UserRegistration(UserObjects signUpobj, string encryptKey)
        {
            string apiUrl = JobPortalAPIURl + "JobPortal/Registeration";
            var Signupinput =
              new
              {
                  Encryptedkey = encryptKey,
                  FirstName = signUpobj.FirstName,
                  LastName = signUpobj.LastName,
                  EmailId = signUpobj.EmailId,
                  Password = signUpobj.Password,
                  UserName = signUpobj.UserName,
              };
            string inputJson = (new JavaScriptSerializer()).Serialize(Signupinput);
            WebClient clientnew = new WebClient();
            WebRequest WebReq = WebRequest.Create(apiUrl);
            WebReq.Method = "POST";
            clientnew.Headers["Content-type"] = "application/json";
            clientnew.Encoding = Encoding.UTF8;
            string RESULT = clientnew.UploadString(apiUrl, inputJson);
            return RESULT;
        }
        public static string Emailidcheck(UserObjects userobj, string encryptKey)
        {
            try
            {
                string apiUrl = JobPortalAPIURl + "JobPortal/EmailIdValidate";
                var EmailChk =
                    new
                    {
                        Encryptedkey = encryptKey,
                        EmailId = userobj.EmailId
                    };
                string inputJson = (new JavaScriptSerializer()).Serialize(EmailChk);
                WebClient clientnew = new WebClient();
                WebRequest WebReq = WebRequest.Create(apiUrl);
                WebReq.Method = "POST";
                clientnew.Headers["Content-type"] = "application/json";
                clientnew.Encoding = Encoding.UTF8;
                string RESULT = clientnew.UploadString(apiUrl, inputJson);
                return RESULT;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static string UserLogin(UserObjects userobj, string encryptKey)
        {
            string apiUrl = JobPortalAPIURl + "JobPortal/UserLogin";
            string UserName = userobj.UserName;
            string Password = userobj.Password;
            string SignIn = EncryptDecryptBL.Encrypt(encryptKey + "~" + UserName + "~" + Password);
            var input = new
            {
                EncryptObjSignin = SignIn
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            WebClient clientnew = new WebClient();
            WebRequest WebReq = WebRequest.Create(apiUrl);
            WebReq.Method = "POST";
            clientnew.Headers["Content-type"] = "application/json";
            clientnew.Encoding = Encoding.UTF8;
            string RESULT = clientnew.UploadString(apiUrl, inputJson);
            return RESULT;
        }
        public static string AddJobdetails(jobobjects jobobj, string encryptKey)
        {
            string apiUrl = JobPortalAPIURl + "SuperAdmin/AddJobDetails";
            var Signupinput =
              new
              {
                  Encryptedkey = encryptKey,
                  Title = jobobj.Title,
                  Description = jobobj.Description,
                  Location = jobobj.Location,
                  Salary = jobobj.Salary,
              };
            string inputJson = (new JavaScriptSerializer()).Serialize(Signupinput);
            WebClient clientnew = new WebClient();
            WebRequest WebReq = WebRequest.Create(apiUrl);
            WebReq.Method = "POST";
            clientnew.Headers["Content-type"] = "application/json";
            clientnew.Encoding = Encoding.UTF8;
            string RESULT = clientnew.UploadString(apiUrl, inputJson);
            return RESULT;
        }

    }
}
