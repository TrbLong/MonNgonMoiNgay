using System;
using System.Collections.Generic;

namespace MonNgonMoiNgay.Models.Entities
{
    public partial class PhanHoi
    {
        public string MaPh { get; set; } = null!;
        public string MaNd { get; set; } = null!;
        public string ChiMuc { get; set; } = null!;
        public string? NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;

        MonNgonMoiNgayContext db = new MonNgonMoiNgayContext();
        public string setMaPh(string maND)
        {
            var last = (from b in db.PhanHois
                        where b.MaNd == maND
                        orderby b.MaPh descending
                        select b).FirstOrDefault();
            if (last == null)
            {
                return maND + "-01";
            }
            int temp = int.Parse(Convert.ToString(last.MaPh).Substring(6));
            string ma = maND + "-" + Convert.ToString(1000 + temp + 1).Substring(1);
            return ma;
        }
    }
}
