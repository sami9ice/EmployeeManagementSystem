using EmpMgtSys.Context;
using EmpMgtSys.Models;
using EmpMgtSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmpMgtSys.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Start()
        {
          
            return View();
        }
        public ActionResult Index()
          {
            var emplyeelist = db.Employees.ToList();
            return View(emplyeelist);
        }

  
        [HttpGet]
        public ActionResult Login()
        {
           
                return View();

            
        }
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                var usr = db.UserAccounts.Where(u => u.StaffID == model.StaffId && u.Password == model.Password).FirstOrDefault();
                if (usr != null)
                {
                    var Role = db.UsersInRole.Where(a => a.UserID == usr.UserID).ToList();
                    foreach (var item in Role)
                    {
                      

                        if (item.Role.RoleName == "Admin")
                        {
                            Session["Admin"] = "Admin";
                            ViewBag.ResultMessage = "Login Successfully !";

                            return RedirectToAction("Index", "Home");
                        }
                        if (item.Role.RoleName == "User")
                        {
                            Session["User"] = "User";
                            ViewBag.ResultMessage = "Login Successfully !";

                            return RedirectToAction("Index", "Home");
                        }



                    }



                }


                if (usr != null)
                {

                    var checkexistance = (from reg in db.UserAccounts where reg.Password == model.Password select reg);
                    if (checkexistance.Count() == 0)
                    {
                        ModelState.AddModelError("", "Invalid Username/Password");
                        return View();
                    }
  
                }


                ModelState.AddModelError("", "Not Assigned a Role Yet or Staff Id Does not exist, Contact Your Admin.");


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(returnUrl);

            }



            return View(returnUrl);

        }

        public ActionResult Register()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserAccount user)
        {
            try
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var checkexistance = (from reg in db.UserAccounts where reg.StaffID == user.StaffID select reg);
                    if (checkexistance.Count() > 0)
                    {
                        ModelState.AddModelError("", "StaffID already exists, Try another one");
                        return View();
                    }

                    var checkexistance1 = (from reg in db.UserAccounts where reg.Email == user.Email select reg);
                    if (checkexistance1.Count() > 0)
                    {
                        ModelState.AddModelError("", "Email already exists, Try another one");
                        return View();
                    }

                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        string emailRegex = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(user.Email))
                        {
                            ModelState.AddModelError("Email", "Please Enter Correct Email Address");
                            return View();

                        }

                        else
                        {
                            user.DateCreated = DateTime.Now;

                            context.UserAccounts.Add(user);
                            context.SaveChanges();

                            TempData["Success"] = "Registered Successfully";
                            ViewBag.ResultMessage = "Registered Successfully !";


                            return RedirectToAction("Index", "Home", new { area = "Profile" });
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email cannot be empty");



                        return RedirectToAction("Register", "Home", new { area = "Profile" });
                    }


                }



            }
            catch (Exception ex)
            {

                return RedirectToAction("Register", "Home", new { area = ex.Message });
            }
          
               




            //return View(user);

        }

        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                
                db.Employees.Add(employee);
                //  db.Employees.GetAll().ToList();
                 db.SaveChanges(); 
                TempData["Success"] = "Added Successfully!";

                return RedirectToAction("Index");
            }
            else
            {
                return View(employee);
            }

        }
        public ActionResult RoleCreate()

        {


            return View(new Role());

        }



        [HttpPost]

        public ActionResult RoleCreate(Role role)
        {

            if (ModelState.IsValid)

            {
                bool RoleCreate = db.Roles.Any(x => x.RoleName == role.RoleName && x.RoleId == role.RoleId);

                if (RoleCreate == true)
                {
                    ModelState.AddModelError("RoleName", "RoleName already exists, Try another one");
                    return View(role);
                }


                else

                {


                    //Roles.CreateRole(role.RoleName);
                    db.Roles.Add(role);
                    db.SaveChanges();


                    TempData["Success"] = "Registered Successfully";


                    return RedirectToAction("RoleCreate", "Home");

                }

            }

            else

            {

                ModelState.AddModelError("Error", "Please enter Username and Password");

                {

                    return View(role);

                }

            }


        }


        public ActionResult RoleAddToUser()

        {

            AssignRoleVM objvm = new AssignRoleVM();

            objvm.RolesList = GetAll_Roles();

            objvm.Userlist = GetAll_Users();

            return View(objvm);

        }



        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult RoleAddToUser(AssignRoleVM objvm)

        {

            if (objvm.RoleName == "0")

            {

                ModelState.AddModelError("RoleName", "Please select RoleName");

            }

            if (objvm.UserID == "0")

            {

                ModelState.AddModelError("Username", "Please select Username");

            }

            if (ModelState.IsValid)

            {

                if (Get_CheckUserRoles(objvm.UserID) == true)


                {


                    ViewBag.ResultMessage = "This user already has the role specified !";

                }

                else

                {
                   
                    var result = GetUserName_BY_UserID(objvm.UserID);

                    var RoleId = (from UP in db.Roles

                                  where UP.RoleName == objvm.RoleName

                                  select UP.RoleId).SingleOrDefault();

                    UsersInRoles usersInRole = new UsersInRoles();
                    usersInRole.RoleId = RoleId;
                    usersInRole.UserID = result;

                    db.UsersInRole.Add(usersInRole);
                    db.SaveChanges();
                   // Roles.AddUserToRole(result, objvm.RoleName);



                    ViewBag.ResultMessage = "User has been mapped to role successfully !";

                }

                objvm.RolesList = GetAll_Roles();

                objvm.Userlist = GetAll_Users();

                return View(objvm);

            }

            else

            {

                objvm.RolesList = GetAll_Roles();

                objvm.Userlist = GetAll_Users();

            }

            return View(objvm);

        }

        private bool Get_CheckUserRoles(string userID)
        {
           

                var data = (from WR in db.UsersInRole

                            join R in db.Roles on WR.RoleId equals R.RoleId

                            where WR.Role.RoleName == userID

                            orderby R.RoleId

                            select new

                            {

                                WR.UserID

                            }).Count();



                if (data > 0)

                {

                    return true;

                }

                else

                {

                    return false;

                }

            
        }
        public List<SelectListItem> GetAll_Roles()

        {

            List<SelectListItem> listrole = new List<SelectListItem>();

            listrole.Add(new SelectListItem { Text = "select", Value = "0" });

            

                foreach (var item in db.Roles)

                {

                    listrole.Add(new SelectListItem { Text = item.RoleName, Value = item.RoleName });

                }

         

            return listrole;


        }

        public List<SelectListItem> GetAll_Users()

        {

            List<SelectListItem> listuser = new List<SelectListItem>();

            listuser.Add(new SelectListItem { Text = "Select", Value = "0" });



           

                foreach (var item in db.UserAccounts)

                {

                    listuser.Add(new SelectListItem { Text = item.StaffID, Value = item.StaffID });

                }

            

            return listuser;

        }
        public int GetUserName_BY_UserID(string UserName)

        {

           

                var result = (from UP in db.UserAccounts

                              where UP.StaffID == UserName

                              select UP.UserID).SingleOrDefault();

                return result;






            



        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}