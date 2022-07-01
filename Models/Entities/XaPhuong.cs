using System;
using System.Collections.Generic;

namespace MonNgonMoiNgay.Models.Entities
{
    public partial class XaPhuong
    {
        public XaPhuong()
        {
            BaiDangs = new HashSet<BaiDang>();
        }

        public string MaXp { get; set; } = null!;
        public string MaQh { get; set; } = null!;
        public string? TenXp { get; set; }

        public virtual QuanHuyen MaQhNavigation { get; set; } = null!;
        public virtual ICollection<BaiDang> BaiDangs { get; set; }
    }
}
