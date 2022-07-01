using System;
using System.Collections.Generic;

namespace MonNgonMoiNgay.Models.Entities
{
    public partial class HinhAnh
    {
        public string MaBd { get; set; } = null!;
        public string UrlImage { get; set; } = null!;

        public virtual BaiDang MaBdNavigation { get; set; } = null!;
    }
}
