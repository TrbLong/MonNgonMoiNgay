using System;
using System.Collections.Generic;

namespace MonNgonMoiNgay.Models.Entities
{
    public partial class TinhTp
    {
        public TinhTp()
        {
            QuanHuyens = new HashSet<QuanHuyen>();
        }

        public string MaTp { get; set; } = null!;
        public string? TenTp { get; set; }

        public virtual ICollection<QuanHuyen> QuanHuyens { get; set; }
    }
}
