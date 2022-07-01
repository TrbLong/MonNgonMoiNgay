using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MonNgonMoiNgay.Models.Entities
{
    public partial class NguoiDung
    {
        public NguoiDung()
        {
            BaiDangDuocLuus = new HashSet<BaiDangDuocLuu>();
            BaiDangs = new HashSet<BaiDang>();
            DayBaiDangs = new HashSet<DayBaiDang>();
            PhanHois = new HashSet<PhanHoi>();
            ThongBaos = new HashSet<ThongBao>();
            TinNhanNguoiGuiNavigations = new HashSet<TinNhan>();
            TinNhanNguoiNhanNavigations = new HashSet<TinNhan>();
            YeuThichBaiDangs = new HashSet<YeuThichBaiDang>();
        }

        public string MaNd { get; set; } = null!;
        public string? MaLoai { get; set; }
        public string? HoLot { get; set; }
        public string? Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int? GioiTinh { get; set; }
        public string? Sdt { get; set; }
        public string Email { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string? ImgAvt { get; set; }
        public string? DiaChi { get; set; }
        public bool TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual LoaiNd? MaLoaiNavigation { get; set; }
        public virtual ICollection<BaiDangDuocLuu> BaiDangDuocLuus { get; set; }
        public virtual ICollection<BaiDang> BaiDangs { get; set; }
        public virtual ICollection<DayBaiDang> DayBaiDangs { get; set; }
        public virtual ICollection<PhanHoi> PhanHois { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
        public virtual ICollection<TinNhan> TinNhanNguoiGuiNavigations { get; set; }
        public virtual ICollection<TinNhan> TinNhanNguoiNhanNavigations { get; set; }
        public virtual ICollection<YeuThichBaiDang> YeuThichBaiDangs { get; set; }

        MonNgonMoiNgayContext db = new MonNgonMoiNgayContext();
        public string setMaUser()
        {
            var nd = db.NguoiDungs.OrderByDescending(x => x.MaNd).FirstOrDefault();
            if (nd == null)
            {
                return "U000001";
            }
            int temp = int.Parse(Convert.ToString(nd.MaNd).Substring(1));
            string ma_user = "U" + Convert.ToString(1000000 + temp + 1).Substring(1);
            return ma_user;
        }

        public DateTime setNgayTao()
        {
            return DateTime.Now;
        }

        public string mahoaMatKhau(string pass)
        {
            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public string getTenHienThi()
        {
            var nd = db.NguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            if(String.IsNullOrEmpty(nd.HoLot) && String.IsNullOrEmpty(nd.Ten))
            {
                return "user" + nd.MaNd.Substring(1, nd.MaNd.Length - 1);
            }
            return nd.Ten;
        }
        public string getImage()
        {
            var nd = db.NguoiDungs.FirstOrDefault(x => x.MaNd == this.MaNd);
            if (nd.ImgAvt == null) return "/Content/Images/Resources/avt-default.png";
            return "/Content/Images/UserAvt/" + nd.ImgAvt;
        }
    }
}
