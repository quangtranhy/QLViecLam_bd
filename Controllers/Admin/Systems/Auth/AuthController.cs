using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLViecLam.Data;
using QLViecLam.Helper;
using QLViecLam.Models.Admin.Manages.ThongTinThiTruongLD;
using QLViecLam.Models.Admin.Systems;
using QLViecLam.Models.Admin.Systems.HeThongChung;
using QLViecLam.ViewModels.Admin.Systems;
using System.Security.Cryptography;

namespace QLViecLam.Controllers.Admin.Systems.Auth
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AuthController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("Login")]
        public IActionResult Login(string Username)
        {
            string? banquyen = "LifeSoftwave";

            ViewData["Title"] = "Đăng nhập hệ thống";
            ViewData["Username"] = Username;
            ViewData["BanQuyen"] = banquyen;
            return View("Views/Admin/Systems/Auth/Login.cshtml");
        }

        [HttpPost("SigIn")]
        public IActionResult SignIn(string username, string password)
        {
            if (username != null && password != null)
            {
                var model = _db.Users.FirstOrDefault(u => u.Username == username);

                if (model != null)
                {
                    if (model.Status == "Lock")
                    {
                        ModelState.AddModelError("error", "Tài khoản đã bị khóa. Liên hệ với quản trị hệ thống !!!");
                        ViewData["username"] = username;
                        ViewData["password"] = password;
                        return View("Views/Admin/Systems/Auth/Login.cshtml");
                    }
                    else
                    {
                        string md5_password = "";
                        using (MD5 md5Hash = MD5.Create())
                        {
                            string change = Funtions_Global.GetMd5Hash(md5Hash, password);
                            md5_password = change;
                        }
                        if (md5_password == model.Password)
                        {
                            model.CountLogin = 0;
                            _db.Users.Update(model);
                            _db.SaveChanges();


                            HttpContext.Session.SetString("SsAdmin", JsonConvert.SerializeObject(model));

                            //var sessionValue = HttpContext.Session.GetString("SsAdmin");

                            //if (!string.IsNullOrEmpty(sessionValue))
                            //{
                            //    // Giả sử rằng 'AdminModel' là kiểu dữ liệu thực sự của đối tượng 'model' mà bạn muốn deserialize.
                            //    var sessionObject = JsonConvert.DeserializeObject<Users>(sessionValue);
                            //    if (sessionObject != null)
                            //    {
                            //        // Trả về giá trị thuộc tính 'PhanLoaiTk' trong phản hồi HTTP 200 OK
                            //        return Ok(sessionObject);
                            //    }
                            //    else
                            //    {
                            //        // Nếu không thể deserialize, trả về một thông báo lỗi
                            //        return NotFound("Không thể deserialize session 'SsAdmin'.");
                            //    }
                            //}
                            //else
                            //{
                            //    // Nếu session không tồn tại, trả về một thông báo lỗi
                            //    return NotFound("Session 'SsAdmin' không tồn tại.");
                            //}

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            //int locklogin = _db.SystemInfo.OrderBy(t => t.Id).First().LoginLock;
                            int count = model.CountLogin + 1;
                            if (count == 5)
                            {
                                model.Status = "Lock";
                            }
                            else
                            {
                                model.CountLogin = count;
                            }
                            _db.Users.Update(model);
                            _db.SaveChanges();
                            ViewData["username"] = username;
                            ModelState.AddModelError("error", "Mật khẩu truy cập không đúng!!!(" + count + "/5)");
                            ViewData["Title"] = "Login";
                            return View("Views/Admin/Systems/Auth/Login.cshtml");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("error", "Tài khoản/Mật khẩu truy cập không đúng !!!");
                    ViewData["Title"] = "Login";
                    return View("Views/Admin/Systems/Auth/Login.cshtml");
                }
            }
            else
            {
                ModelState.AddModelError("error", "Tài khoản/Mật khẩu truy cập không được để trống !!!");
                ViewData["Title"] = "Login";
                return View("Views/Admin/Systems/Auth/Login.cshtml");
            }
        }

        [HttpGet("LogOut")]
        public IActionResult LogOut()
        {
            //HttpContext.Session.Remove("Permission");
            HttpContext.Session.Remove("SsAdmin");
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet("DanhSachTaiKhoan")]
        public IActionResult DanhSachTaiKhoan(string madb)
        {
            var user = _db.Users.Where(x => x.PhanLoaiTk == "1");

            var madb_list = _db.DmHanhChinh.Where(x => x.MaDb == madb || x.Parent == madb).Select(x => x.MaDb).ToList();
            var donvi = _db.DmDonvi.Where(x => madb_list.Contains(x.MaDiaBan));
            var model = (from u in user
                         join dv in donvi on u.MaDonVi equals dv.MaDonVi
                         select new VM_User_DonVi
                         {
                             Username = u.Username,
                             //Name = u.Name,
                             TenDv = dv.TenDv,
                             CapDo = u.CapDo,
                             Status = u.Status,
                         });

            var dshuyen = _db.DmHanhChinh.Where(x => x.CapDo == "T" || x.CapDo == "H");
            ViewData["dshuyen"] = dshuyen;
            ViewData["madb"] = madb;
            return View("Views/Admin/Systems/Auth/DanhSachTaiKhoan.cshtml", model);
        }

        [HttpGet("DangKy")]
        public IActionResult DangKy()
        {

            return View("Views/Admin/Systems/Auth/DangKy.cshtml");
        }

        [HttpPost("Store")]
        public IActionResult Store(string name, string email, string password, string confirm_password, string dkkd)
        {
            var model = _db.Users.Where(x => x.PhanLoaiTk == "2").Where(x => x.Email == email);
            if (model.Any())
            {
                ViewData["name"] = name;
                ViewData["email"] = email;
                ViewData["dkkd"] = dkkd;
                ViewData["password"] = password;
                ViewData["emaiError"] = "Email đã có người sử dụng, mời nhập Email khác!";
                ViewData["Title"] = "DangKy";
                return View("Views/Admin/Systems/Auth/DangKy.cshtml");
            }
            else
            {
                string md5_password = "";
                using (MD5 md5Hash = MD5.Create())
                {
                    string change = Funtions_Global.GetMd5Hash(md5Hash, password);
                    md5_password = change;
                }
                var data_user = new Users
                {
                    PhanLoaiTk = "2",
                    Name = name,
                    Email = email,
                    Username = email,
                    Password = md5_password,
                };
                _db.Users.Add(data_user);
                _db.SaveChanges();

                var user_new = _db.Users.Where(x => x.PhanLoaiTk == "2").FirstOrDefault(x => x.Email == email)!.Id;

                var data_company = new Company
                {
                    Name = name,
                    Email = email,
                    Dkkd = dkkd,
                    MaDv = dkkd,
                    User = user_new,
                };
                _db.Company.Add(data_company);
                _db.SaveChanges();


                return RedirectToAction("Index", "Home");
            }
           
        }
    }
}
