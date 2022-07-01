using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonNgonMoiNgay.Areas.Admin.Models;
using MonNgonMoiNgay.Models.Entities;

namespace MonNgonMoiNgay.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        MonNgonMoiNgayContext db = new MonNgonMoiNgayContext();
        public IActionResult Index()
        {
            //ViewData["listbd"] = db.BaiDangs.Where(s => s.MaNd == User.Claims.ToList()[0].Value).ToList();
            List<BaiDang> bai = db.BaiDangs.Where(s => s.MaNd == User.Claims.ToList()[0].Value).ToList();
            return View(bai);
        }

        //Chức năng đẩy bài đăng của người dùng
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DayBaiDang(string id)
        {
            var baidang = await db.BaiDangs.FirstOrDefaultAsync(x => x.MaBd == id);
            var daybaidang = await db.DayBaiDangs.FirstOrDefaultAsync(x => x.MaBd == id && x.MaNd == User.Claims.ToList()[0].Value);

            if (baidang != null && daybaidang == null)
            {
                DayBaiDang day = new DayBaiDang();
                day.MaBd = baidang.MaBd;
                day.MaNd = User.Claims.ToList()[0].Value;
                day.ThoiGian = DateTime.Now;

                db.DayBaiDangs.Add(day);
                db.SaveChanges();

                return Json(new { tt = true });
            }
            return Json(new { tt = false });
        }
        [Authorize]
        [HttpPost]
        public IActionResult listday()
        {
            List<DayBaiDang> daybaidang = db.DayBaiDangs.Where(s=>s.ThoiGian.CompareTo(DateTime.Now)<=7).ToList();
            return View(daybaidang);
        }

        //Chức năng ẩn bài đăng của người dùng
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AnBaiDang(string id)
        {
            var baidang = await db.BaiDangs.FirstOrDefaultAsync(x => x.MaBd == id);

            if (baidang != null && baidang.TrangThai != 0)
            {
                baidang.TrangThai = 0;

                db.SaveChanges();

                return Json(new { tt = true });
            }
            else if(baidang != null && baidang.TrangThai == 0)
            {
                baidang.TrangThai = 1;
                db.SaveChanges();
                return Json(new { tt = true });
            }
            return Json(new { tt = false });
        }

        public async Task<IActionResult> List(string? q, int? p, string? l)
        {
            ViewBag.Loai = l;
            ViewData["LoaiNd"] = db.LoaiNds.ToList();
            var nd = from u in db.NguoiDungs select u;

            //Lọc người dùng theo họ tên
            if (!String.IsNullOrEmpty(q))
            {
                nd = nd.Where(s => string.Concat(s.HoLot, " ", s.Ten).Contains(q));
            }

            //Select trả về khác rỗng và khác 01 (01 là trường hợp admin không hiển thị, thay vào đó là hiển thị tất cả)
            if (!String.IsNullOrEmpty(l) && l != "01")
            {
                nd = nd.Where(s => s.MaLoai == l);
            }

            //Số lượng người dùng được trả về trên một trang
            int pageSize = 1;

            //Chờ đợi xử lý phân trang rồi mới trả về view
            //Các tham số của phân trang như sau:
            //      nd.AsNoTracking() là danh sách người dùng chỉ xem
            //      p là trang muốn hiển thị, ở đây nếu không nhập thì ngầm hiểu trang hiển thị là 1 tức là trang đầu
            //      pageSize là số số lượng người hiển thị trên trang
            return View(await PaginatedList<NguoiDung>.CreateAsync(nd.AsNoTracking(), p ?? 1, pageSize));
        }
    }
}
