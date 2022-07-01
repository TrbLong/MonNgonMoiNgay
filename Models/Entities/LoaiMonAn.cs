using System;
using System.Collections.Generic;

namespace MonNgonMoiNgay.Models.Entities
{
    public partial class LoaiMonAn
    {
        public LoaiMonAn()
        {
            BaiDangs = new HashSet<BaiDang>();
        }

        public string MaLoai { get; set; } = null!;
        public string TenLoai { get; set; } = null!;

        public virtual ICollection<BaiDang> BaiDangs { get; set; }
    }
}
