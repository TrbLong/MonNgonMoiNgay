using System;
using System.Collections.Generic;

namespace MonNgonMoiNgay.Models.Entities
{
    public partial class QuanHuyen
    {
        public QuanHuyen()
        {
            XaPhuongs = new HashSet<XaPhuong>();
        }

        public string MaQh { get; set; } = null!;
        public string MaTp { get; set; } = null!;
        public string? TenQh { get; set; }

        public virtual TinhTp MaTpNavigation { get; set; } = null!;
        public virtual ICollection<XaPhuong> XaPhuongs { get; set; }
    }
}
