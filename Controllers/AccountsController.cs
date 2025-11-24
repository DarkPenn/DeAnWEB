using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnWEB.Models;
using DeAnWEB.Models.ViewModel;
using System.Web.Security;
using System.Runtime.Remoting.Messaging;


namespace DeAnWEB.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        private MyStoreEntities db = new MyStoreEntities();
       
        public ActionResult ProfileInfo()
        {
            if (!Request.IsAuthenticated)
                return RedirectToAction("Login","Accounts");

            var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);

            var model = new ProfileVM
            {
                Username = user.Username,
                Role = user.UserRole,

                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CustomerPhone = customer.CustomerPhone,
                CustomerAddress = customer.CustomerAddress,
                CustomerID = customer.CustomerID
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateProfile(ProfileVM model)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.Find(model.CustomerID);

                customer.CustomerName = model.CustomerName;
                customer.CustomerPhone = model.CustomerPhone;
                customer.CustomerEmail = model.CustomerEmail;
                customer.CustomerAddress = model.CustomerAddress;

                db.SaveChanges();

                return RedirectToAction("ProfileInfo");
            }

            return View(model);
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }
        //POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                //kiểm tra tên đăng nhập đã tồn tại chưa
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại:");
                    return View(model);
                }

                //Nếu chưa tồn tại thì tạo bản ghi thông tin tài khoản trong bảng User
                var user = new DeAnWEB.Models.User
                {
                    Username = model.Username,
                    Password = model.Password,//nên mã hóa mật khẩu trước khi lưu
                    UserRole = "C"
                };
                db.Users.Add(user);
                //và tạo bản ghi thông tin khách hàng trong Customer
                var customer = new DeAnWEB.Models.Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,
                };
                //db.Customers.Add(customer);
                ////lưu thông tin tài khoản và thông tin khách hàng vào CSDL
                //db.SaveChanges();
                try
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Entity: {0}, State: {1}",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: {0}, Error: {1}",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

                return RedirectToAction("Index", "Homes");
            }
            return View(model);
        }

        //GET: Account/Login
        public ActionResult Login()
        { return View(); }
        //POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username
                    && u.Password == model.Password
                    && u.UserRole == "C");
                if (user != null)
                {
                    //Lưu trạng thái đăng nhập vào session
                    Session["Username"] = user.Username;
                    Session["UserRole"] = user.UserRole;

                    //Lưu thông tin xác thực người dùng vào cookie
                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    return RedirectToAction("Index", "Homes");
                }
                else { ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng."); }
            }
            return View(model);
        }

        //GET: Account/LogOut
        public ActionResult Logout()
        {
            // Xóa session
            Session.Clear();
            Session.Abandon();

            // Xóa cookie xác thực
            FormsAuthentication.SignOut();

            // Chuyển về trang đăng nhập
            return RedirectToAction("Login", "Accounts");
        }

        //GET: Account/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }
        //POST: Accout/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Lấy user từ username nhập
            var user = db.Users.SingleOrDefault(u => u.Username == model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập không tồn tại.");
                return View(model);
            }

            // Kiểm tra mật khẩu cũ
            if (user.Password != model.OldPassword)
            {
                ModelState.AddModelError("", "Mật khẩu cũ không đúng");
                return View(model);
            }

            // Cập nhật mật khẩu mới
            user.Password = model.NewPassword;
            db.SaveChanges();

            ViewBag.Message = "Đổi mật khẩu thành công!";
            return RedirectToAction("Login","Accounts");
        }
        
    }
}