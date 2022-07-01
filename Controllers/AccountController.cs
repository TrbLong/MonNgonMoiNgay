using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonNgonMoiNgay.Models;
using MonNgonMoiNgay.Models.Entities;
using System.Security.Claims;

namespace MonNgonMoiNgay.Controllers
{
    public class AccountController : Controller
    {
        MonNgonMoiNgayContext db = new MonNgonMoiNgayContext();

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {

            LoginModel objLoginModel = new LoginModel();
            objLoginModel.ReturnUrl = string.IsNullOrEmpty(ReturnUrl) ? "/" : ReturnUrl;
            return View(objLoginModel);
        }

        //Xác thực đăng nhập
        [AllowAnonymous]
        public async Task<IActionResult> getLogin(string email, string pass, bool re)
        {
            NguoiDung temp = new NguoiDung();

            var user = db.NguoiDungs.Where(x => x.Email == email && x.MatKhau == temp.mahoaMatKhau(pass)).FirstOrDefault();
            if (user != null)
            {
                //Tạo list lưu chủ thể đăng nhập
                var claims = new List<Claim>() {
                        new Claim("MaNd", user.MaNd),
                        new Claim("LoaiNd", user.MaLoai == "01" ? "Admin" : user.MaLoai == "02" ? "User" : user.MaLoai == "03" ? "NguoiBanHang" : "NguoiDung"),
                        new Claim("Email", user.Email),
                        new Claim("ImgAvt", user.getImage())
                    };

                //Tạo Identity để xác thực và xác nhận
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                //Gọi hàm đăng nhập bất đồng bộ của HttpContext
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = re
                });

                return Json(new { tt = true });
            }
            return Json(new { tt = false, mess = "Email hoặc mật khẩu không chính xác !" });
        }

        //Hàm đăng xuất
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            //Gọi hàm đăng xuất của HttpContext
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //return Json(new { tt = true, mess = "Đăng xuất thành công !" });
            return LocalRedirect("/");
        }

        //Kiểm tra email có tồn tại trên hệ thống chưa
        [AllowAnonymous]
        public async Task<IActionResult> ktEmail(string email)
        {
            var temp = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Email == email);

            return temp != null ? Json(new { email = false }) : Json(new { email = true });
        }

        //Đăng nhập với tài khoản google
        [AllowAnonymous]
        public async Task<IActionResult> loginWithGoogle(string hoten, string email, IFormFile img_avt)
        {
            var user = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                await createAccount(email, "userloginwithgoogle" + email);

                var userLogin = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Email == email);
                if (userLogin != null)
                {
                    userLogin.HoLot = hoten.Substring(0, hoten.LastIndexOf(' '));
                    userLogin.Ten = hoten.Substring(hoten.LastIndexOf(' ') + 1);

                    //Khai báo đường dẫn lưu file
                    var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\Images\\UserAvt\\");
                    bool basePathExists = Directory.Exists(basePath);

                    //Nếu thư mục không có thì tạo mới
                    if (!basePathExists) Directory.CreateDirectory(basePath);

                    var fileName = "avt-" + userLogin.MaNd + "-" + DateTime.Now.Millisecond + ".jpg";
                    var filePath = Path.Combine(basePath, fileName);

                    //Nếu file tồn tại thì thêm file vào server và cập nhật vào csdl
                    if (!System.IO.File.Exists(filePath))
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await img_avt.CopyToAsync(stream);
                        }

                        userLogin.ImgAvt = fileName;
                    }

                    db.SaveChanges();
                }

                return Json(new { tt = true, mess = "Đăng nhập thành công !" });
            }
            await getLogin(email, "userloginwithgoogle" + email, false);
            return Json(new { tt = true, mess = "Đăng nhập thành công !" });
        }

        //Tạo mới tài khoản
        [AllowAnonymous]
        public async Task<IActionResult> createAccount(string email, string pass)
        {
            if (email == "" || pass == "")
            {
                return Json(new { tt = false, erro = "form", mess = "Chưa nhập đủ thông tin !<br>Tên, email và mật khẩu là bắt buộc." });
            }
            var emailCheck = await db.NguoiDungs.FirstOrDefaultAsync(x => x.Email == email);
            if (emailCheck != null)
            {
                return Json(new { tt = false, erro = "email", mess = "Email đã tồn tại trên hệ thống !" });
            }

            NguoiDung newUser = new NguoiDung();
            newUser.MaNd = newUser.setMaUser();
            newUser.MaLoai = "02";
            newUser.Email = email;
            newUser.MatKhau = newUser.mahoaMatKhau(pass);
            newUser.TrangThai = true;
            newUser.NgayTao = newUser.setNgayTao();

            db.NguoiDungs.Add(newUser);
            db.SaveChanges();

            await getLogin(newUser.Email, pass, false);
            return Json(new { tt = true, mess = "Đăng ký tài khoản thành công !" });
        }
    }
}
