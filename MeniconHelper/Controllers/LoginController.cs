using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using MeniconHelper.App_LocalResources;

namespace MeniconHelper.Controllers
{
    /// <summary>
    /// Login Controller link to the view /Views/Login/Index.
    /// 
    /// Allow users to connect into MeniconHelper using "default_password" field in the database
    /// All password are hashed in MD5 using the GenerateMD5 function.
    /// </summary>
    public class LoginController : Controller
    {
        //Main view of the controller.
        [HttpGet]
        public ActionResult Index()
        {
            //Checking if a user is already connected and redirecting him if yes
            if (Session["User"]!=null)
                return RedirectToAction("../Home/Index");
            else
                return View();
        }


        //Allow to check in the database if user exist using a post method. 
        [HttpPost]
        public ActionResult Verify(person loginPerson)
        {
            //Connection to db using Entity Framework - MeniconHelperBDD.edmx to get the diagram
            using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
            {
                //Get all users in db
                foreach (person p in meniconHelperEntities.person)
                {
                    //Check if user exist
                    if (p.username == loginPerson.username)
                    {
                        //Check if password match with the user
                        
if (GenerateMD5(loginPerson.password_default) == p.password_default)
                        {
                            //Define a Session for the user and redirect to the main View
                            Session["User"] = p;
                            return RedirectToAction("../Home/Index");
                        }
                        else
                        {
                            ViewBag.Message = GlobalRes.incorrectPassword;
                            return View("Index");
                        }
                    }
                }
            }
            ViewBag.Message = GlobalRes.incorrectUsername;
            return View("Index");
        }

        //Hashing function using MD5. Function need the raw password in parameter and return the hashed password.
        public string GenerateMD5(string rawString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(rawString)).Select(s => s.ToString("x2")));
        }
    }
}