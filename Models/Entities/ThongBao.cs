using System;
using System.Collections.Generic;

namespace MonNgonMoiNgay.Models.Entities
{
    public partial class ThongBao
    {
        public string MaTb { get; set; } = null!;
        public string MaNd { get; set; } = null!;
        public string TieuDe { get; set; } = null!;
        public string? NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public bool TrangThai { get; set; }
        public string? LienKet { get; set; }

        public virtual NguoiDung MaNdNavigation { get; set; } = null!;
    }
}
