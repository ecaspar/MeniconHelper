using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using MeniconHelper.App_LocalResources;

namespace MeniconHelper.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            //Someone who isn't logged can't access to this view.
            if (Session["User"] != null)
            {
                //Get which user is logged.
                person p = (person)(Session["User"]);
                ViewBag.name = p.first_name + " " + p.last_name;
                ViewBag.person = p;

                if (p.id_role == 7)
                    ViewBag.Admin = true;
                else
                    ViewBag.Admin = false;

                return View();
            }
            else
                return View("../Login/Index");
        }

        [HttpPost]
        public ActionResult ChangePassword(person person, string newPassword, string confirmPassword)
        {
            if(person.password_default!=null && newPassword!=null && confirmPassword!=null)
            {
                using (MeniconHelperEntities meniconHelperEntities = new MeniconHelperEntities())
                {
                    if (meniconHelperEntities.Database.Exists())
                    {
                        person p = (person)(Session["User"]);
                        if (p.password_default == GenerateMD5(person.password_default))
                        {
                            if (newPassword == confirmPassword)
                            {
                                //Update password de p
                                person pswPerson = meniconHelperEntities.person.Single(x => x.id_person == p.id_person);
                                pswPerson.password_default = GenerateMD5(confirmPassword);
                                meniconHelperEntities.SaveChanges();
                            }
                            else
                            {
                                ViewBag.Message = GlobalRes.incorrectConfirm;
                                person per = (person)(Session["User"]);
                                ViewBag.name = per.first_name + " " + per.last_name;
                                ViewBag.person = per;
                                if (p.id_role == 7)
                                    ViewBag.Admin = true;
                                else
                                    ViewBag.Admin = false;
                                return View("../Account/Index");
                            }
                        }
                        else
                        {
                            ViewBag.Message = GlobalRes.incorrectPassword;
                            person per = (person)(Session["User"]);
                            ViewBag.name = per.first_name + " " + per.last_name;
                            ViewBag.person = per;
                            if (p.id_role == 7)
                                ViewBag.Admin = true;
                            else
                                ViewBag.Admin = false;
                            return View("../Account/Index");
                        }

                    }
                    else
                        ViewBag.Message = GlobalRes.dbOffline;
                }
            }
            return RedirectToAction("../Account/Index");
        }

        public string GenerateMD5(string rawString)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(rawString)).Select(s => s.ToString("x2")));
        }
    }
}