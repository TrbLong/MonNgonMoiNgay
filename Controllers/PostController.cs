using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonNgonMoiNgay.Models.Entities;

namespace MonNgonMoiNgay.Controllers
{
    public class PostController : Controller
    {
        MonNgonMoiNgayContext db = new MonNgonMoiNgayContext();
        //Struct đếm sl yêu thích
        struct DemYT
        {
            public int sl;
            public string MaBd;
        };
        [Authorize]
        public IActionResult CreateNew()
        {
            ViewData["LoaiMonAn"] = db.LoaiMonAns.ToList();
            ViewData["TinhTP"] = db.TinhTps.ToList();
            return View();
        }

        //Thêm mới bài đăng
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> getCreateNew(string loai, string ten, int gia, string mota, string xp, string diachi, IList<IFormFile> images)
        {
            //Tạo mới bài đăng và thêm các thuộc tính cần thiết
            BaiDang newPost = new BaiDang();
            newPost.MaBd = newPost.setMa(User.Claims.ToList()[0].Value);
            newPost.MaLoai = loai;
            newPost.MaNd = User.Claims.ToList()[0].Value;
            newPost.ThoiGian = DateTime.Now;
            newPost.TenMon = ten;
            newPost.GiaTien = gia;
            newPost.MoTa = mota;
            newPost.MaXp = xp;
            newPost.DiaChi = diachi;
            newPost.TrangThai = 1;
            db.BaiDangs.Add(newPost);

            foreach (IFormFile img in images)
            {
                //Khai báo đường dẫn lưu file
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot\\Content\\FilesPost\\");
                bool basePathExists = Directory.Exists(basePath);

                //Nếu thư mục không có thì tạo mới
                if (!basePathExists) Directory.CreateDirectory(basePath);

                var fileName = newPost.MaBd + "-" + img.FileName;
                var filePath = Path.Combine(basePath, fileName);

                //Nếu file tồn tại thì thêm file vào server và cập nhật vào csdl
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }

                    HinhAnh newHA = new HinhAnh();
                    newHA.MaBd = newPost.MaBd;
                    newHA.UrlImage = fileName;
                    db.HinhAnhs.Add(newHA);
                }
            }

            db.SaveChanges();

            return Json(new { tt = true });
        }

        //Xử lý trả về quận huyện theo mã tỉnh
        [HttpPost]
        [Authorize]
        public IActionResult getQuanHuyen(string ma)
        {
            var qh = db.QuanHuyens.Where(x => x.MaTp == ma).ToList();

            return qh.Count() != 0 ? Json(new { tt = true, qh }) : Json(new { tt = false });
        }

        //Xử lý trả về xã phường theo mã quận huyện
        [HttpPost]
        [Authorize]
        public IActionResult getXaPhuong(string ma)
        {
            var xp = db.XaPhuongs.Where(x => x.MaQh == ma).ToList();

            return xp.Count() != 0 ? Json(new { tt = true, xp }) : Json(new { tt = false });
        }

        //Hiển thị trang detail bài đăng
        [Authorize]
        public IActionResult Detail(string id)
        {
            var baidang = db.BaiDangs.FirstOrDefault(x => x.MaBd == id);

            if (baidang == null)
            {
                return NotFound();
            }

            ViewData["PostSimilar"] = db.BaiDangs.Where(x => x.ThoiGian.AddDays(7) >= DateTime.Now && x.TrangThai == 1).OrderByDescending(x => x.ThoiGian).ToList();
            return View(baidang);
        }

        //Chức năng lưu lại bài đăng của người dùng
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LuuBaiDang(string id)
        {
            var baidang = await db.BaiDangs.FirstOrDefaultAsync(x => x.MaBd == id);
            var saved = await db.BaiDangDuocLuus.FirstOrDefaultAsync(x => x.MaBd == id && x.MaNd == User.Claims.ToList()[0].Value);

            if(baidang != null && saved == null)
            {
                BaiDangDuocLuu save = new BaiDangDuocLuu();
                save.MaBd = baidang.MaBd;
                save.MaNd = User.Claims.ToList()[0].Value;
                save.ThoiGian = DateTime.Now;

                db.BaiDangDuocLuus.Add(save);
                db.SaveChanges();

                return Json(new { tt = true });
            }
            return Json(new { tt = false });
        }

        //Chức năng yêu thích bài đăng của người dùng
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> YeuThichBaiDang(string id)
        {
            var baidang = await db.BaiDangs.FirstOrDefaultAsync(x => x.MaBd == id);
            var yt = await db.YeuThichBaiDangs.FirstOrDefaultAsync(x => x.MaBd == id && x.MaNd == User.Claims.ToList()[0].Value);

            if (baidang != null && yt == null)
            {
                YeuThichBaiDang newYT = new YeuThichBaiDang();
                newYT.MaBd = baidang.MaBd;
                newYT.MaNd = User.Claims.ToList()[0].Value;
                newYT.ThoiGian = DateTime.Now;

                db.YeuThichBaiDangs.Add(newYT);
                db.SaveChanges();

                return Json(new { tt = true });
            }
            return Json(new { tt = false });
        }

        //Chức năng phản hồi của người dùng
        [Authorize]
        public IActionResult CreatePhanHoi()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult PhanHoi(string td, string nd)
        {
            try
            {
                PhanHoi phhoi = new PhanHoi();
                phhoi.MaPh = phhoi.setMaPh(User.Claims.ToList()[0].Value);
                phhoi.MaNd = User.Claims.ToList()[0].Value;
                phhoi.ChiMuc = td;
                phhoi.NoiDung = nd;
                phhoi.ThoiGian = DateTime.Now;

                db.PhanHois.Add(phhoi);
                db.SaveChanges();

                return Json(new { tt = true });
            }
            catch (Exception ex)
            {
                return Json(new { tt = false });
            }
            
        }
        [Authorize]
        public IActionResult listBaiDang()
        {
            ViewData["PostNew"] = db.BaiDangs.Where(x => x.ThoiGian.AddDays(7) >= DateTime.Now && x.TrangThai == 1).OrderByDescending(x => x.ThoiGian).ToList();
            ViewData["PostVote"] = (from bd in db.BaiDangs
                                    join dbd in db.DayBaiDangs on bd.MaBd equals dbd.MaBd
                                    join nd in db.NguoiDungs on dbd.MaNd equals nd.MaNd
                                    where nd.MaLoai == "01" && nd.MaLoai == "03"
                                    orderby dbd.ThoiGian descending
                                    select bd).ToList();

            //Xử lý hiển thị top 10 bài đăng được yêu thích nhất
            var list = (from bd in db.BaiDangs
                        join yt in db.YeuThichBaiDangs on bd.MaBd equals yt.MaBd
                        select bd).ToList();

            List<BaiDang> result = new List<BaiDang>();
            List<DemYT> slyt = new List<DemYT>();

            //Chạy lặp gán mã bài đăng và số lượt yt vào danh sách slyt
            foreach (var bd in list)
            {
                var temp = db.YeuThichBaiDangs.Where(x => x.MaBd == bd.MaBd).ToList().Count();
                slyt.Add(new DemYT { MaBd = bd.MaBd, sl = temp });
            }

            //Sắp xếp lượt yêu thích từ cao đến thấp
            slyt.OrderByDescending(x => x.sl);

            //Chạy lặp gán bài đăng vào result
            foreach (var yt in slyt)
            {
                var temp = db.BaiDangs.FirstOrDefault(x => x.MaBd == yt.MaBd);
                result.Add(temp);
            }

            //Gán result vào ViewData để trả về View
            ViewData["PostLike"] = result.Take(10).ToList();
            return View();
        }
    }
}
