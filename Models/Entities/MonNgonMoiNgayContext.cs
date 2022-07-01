using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MonNgonMoiNgay.Models.Entities
{
    public partial class MonNgonMoiNgayContext : DbContext
    {
        public MonNgonMoiNgayContext()
        {
        }

        public MonNgonMoiNgayContext(DbContextOptions<MonNgonMoiNgayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaiDang> BaiDangs { get; set; } = null!;
        public virtual DbSet<BaiDangDuocLuu> BaiDangDuocLuus { get; set; } = null!;
        public virtual DbSet<DayBaiDang> DayBaiDangs { get; set; } = null!;
        public virtual DbSet<HinhAnh> HinhAnhs { get; set; } = null!;
        public virtual DbSet<LoaiMonAn> LoaiMonAns { get; set; } = null!;
        public virtual DbSet<LoaiNd> LoaiNds { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<PhanHoi> PhanHois { get; set; } = null!;
        public virtual DbSet<QuanHuyen> QuanHuyens { get; set; } = null!;
        public virtual DbSet<ThongBao> ThongBaos { get; set; } = null!;
        public virtual DbSet<TinNhan> TinNhans { get; set; } = null!;
        public virtual DbSet<TinhTp> TinhTps { get; set; } = null!;
        public virtual DbSet<XaPhuong> XaPhuongs { get; set; } = null!;
        public virtual DbSet<YeuThichBaiDang> YeuThichBaiDangs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5SADCS6;Initial Catalog=MonNgonMoiNgay;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiDang>(entity =>
            {
                entity.HasKey(e => e.MaBd)
                    .HasName("PK__BaiDang__2E67755D7C31E509");

                entity.ToTable("BaiDang");

                entity.Property(e => e.MaBd)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_BD")
                    .IsFixedLength();

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(100)
                    .HasColumnName("Dia_Chi");

                entity.Property(e => e.GiaTien).HasColumnName("Gia_Tien");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Loai")
                    .IsFixedLength();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.MaXp)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ma_XP")
                    .IsFixedLength();

                entity.Property(e => e.MoTa)
                    .HasMaxLength(200)
                    .HasColumnName("Mo_Ta");

                entity.Property(e => e.TenMon)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_Mon");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.BaiDangs)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaiDang__Ma_Loai__3D5E1FD2");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BaiDangs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaiDang__Ma_ND__3E52440B");

                entity.HasOne(d => d.MaXpNavigation)
                    .WithMany(p => p.BaiDangs)
                    .HasForeignKey(d => d.MaXp)
                    .HasConstraintName("FK__BaiDang__Ma_XP__3F466844");
            });

            modelBuilder.Entity<BaiDangDuocLuu>(entity =>
            {
                entity.HasKey(e => new { e.MaBd, e.MaNd })
                    .HasName("PK__BaiDangD__5C815D86E63EA36F");

                entity.ToTable("BaiDangDuocLuu");

                entity.Property(e => e.MaBd)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_BD")
                    .IsFixedLength();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.MaBdNavigation)
                    .WithMany(p => p.BaiDangDuocLuus)
                    .HasForeignKey(d => d.MaBd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaiDangDu__Ma_BD__44FF419A");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.BaiDangDuocLuus)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaiDangDu__Ma_ND__45F365D3");
            });

            modelBuilder.Entity<DayBaiDang>(entity =>
            {
                entity.HasKey(e => new { e.MaBd, e.MaNd })
                    .HasName("PK__DayBaiDa__5C815D86259E9F78");

                entity.ToTable("DayBaiDang");

                entity.Property(e => e.MaBd)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_BD")
                    .IsFixedLength();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.MaBdNavigation)
                    .WithMany(p => p.DayBaiDangs)
                    .HasForeignKey(d => d.MaBd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DayBaiDan__Ma_BD__4CA06362");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.DayBaiDangs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DayBaiDan__Ma_ND__4D94879B");
            });

            modelBuilder.Entity<HinhAnh>(entity =>
            {
                entity.HasKey(e => new { e.MaBd, e.UrlImage })
                    .HasName("PK__HinhAnh__3C1736C5DEA280E7");

                entity.ToTable("HinhAnh");

                entity.Property(e => e.MaBd)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_BD")
                    .IsFixedLength();

                entity.Property(e => e.UrlImage)
                    .HasMaxLength(200)
                    .HasColumnName("Url_Image");

                entity.HasOne(d => d.MaBdNavigation)
                    .WithMany(p => p.HinhAnhs)
                    .HasForeignKey(d => d.MaBd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HinhAnh__Ma_BD__4222D4EF");
            });

            modelBuilder.Entity<LoaiMonAn>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiMonA__586312F9666BF458");

                entity.ToTable("LoaiMonAn");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Loai")
                    .IsFixedLength();

                entity.Property(e => e.TenLoai)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_Loai");
            });

            modelBuilder.Entity<LoaiNd>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiND__586312F946E049DB");

                entity.ToTable("LoaiND");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Loai")
                    .IsFixedLength();

                entity.Property(e => e.TenLoai)
                    .HasMaxLength(20)
                    .HasColumnName("Ten_Loai");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNd)
                    .HasName("PK__NguoiDun__2E628DB6A9394839");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D105341E21DCC1")
                    .IsUnique();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(100)
                    .HasColumnName("Dia_Chi");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GioiTinh).HasColumnName("Gioi_Tinh");

                entity.Property(e => e.HoLot)
                    .HasMaxLength(20)
                    .HasColumnName("Ho_Lot");

                entity.Property(e => e.ImgAvt)
                    .HasMaxLength(100)
                    .HasColumnName("Img_Avt");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Ma_Loai")
                    .IsFixedLength();

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("Mat_Khau");

                entity.Property(e => e.NgaySinh)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Sinh");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("datetime")
                    .HasColumnName("Ngay_Tao");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.Ten).HasMaxLength(7);

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.NguoiDungs)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK__NguoiDung__Ma_Lo__276EDEB3");
            });

            modelBuilder.Entity<PhanHoi>(entity =>
            {
                entity.HasKey(e => e.MaPh)
                    .HasName("PK__PhanHoi__2E629DF05E88F16C");

                entity.ToTable("PhanHoi");

                entity.Property(e => e.MaPh)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_PH")
                    .IsFixedLength();

                entity.Property(e => e.ChiMuc)
                    .HasMaxLength(50)
                    .HasColumnName("Chi_Muc");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.NoiDung)
                    .HasMaxLength(200)
                    .HasColumnName("Noi_Dung");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.PhanHois)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PhanHoi__Ma_ND__30F848ED");
            });

            modelBuilder.Entity<QuanHuyen>(entity =>
            {
                entity.HasKey(e => e.MaQh)
                    .HasName("PK__QuanHuye__2E61651117BBBB29");

                entity.ToTable("QuanHuyen");

                entity.Property(e => e.MaQh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ma_QH")
                    .IsFixedLength();

                entity.Property(e => e.MaTp)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Ma_TP")
                    .IsFixedLength();

                entity.Property(e => e.TenQh)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_QH");

                entity.HasOne(d => d.MaTpNavigation)
                    .WithMany(p => p.QuanHuyens)
                    .HasForeignKey(d => d.MaTp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__QuanHuyen__Ma_TP__35BCFE0A");
            });

            modelBuilder.Entity<ThongBao>(entity =>
            {
                entity.HasKey(e => e.MaTb)
                    .HasName("PK__ThongBao__2E62FB756742B83B");

                entity.ToTable("ThongBao");

                entity.Property(e => e.MaTb)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_TB")
                    .IsFixedLength();

                entity.Property(e => e.LienKet)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Lien_Ket");

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.NoiDung)
                    .HasMaxLength(200)
                    .HasColumnName("Noi_Dung");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.Property(e => e.TieuDe)
                    .HasMaxLength(50)
                    .HasColumnName("Tieu_De");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.ThongBaos)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ThongBao__Ma_ND__2E1BDC42");
            });

            modelBuilder.Entity<TinNhan>(entity =>
            {
                entity.ToTable("TinNhan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.LienKet)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Lien_Ket");

                entity.Property(e => e.NguoiGui)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Nguoi_Gui")
                    .IsFixedLength();

                entity.Property(e => e.NguoiNhan)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Nguoi_Nhan")
                    .IsFixedLength();

                entity.Property(e => e.NoiDung)
                    .HasMaxLength(500)
                    .HasColumnName("Noi_Dung");

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.Property(e => e.TrangThai).HasColumnName("Trang_Thai");

                entity.HasOne(d => d.NguoiGuiNavigation)
                    .WithMany(p => p.TinNhanNguoiGuiNavigations)
                    .HasForeignKey(d => d.NguoiGui)
                    .HasConstraintName("FK__TinNhan__Nguoi_G__2A4B4B5E");

                entity.HasOne(d => d.NguoiNhanNavigation)
                    .WithMany(p => p.TinNhanNguoiNhanNavigations)
                    .HasForeignKey(d => d.NguoiNhan)
                    .HasConstraintName("FK__TinNhan__Nguoi_N__2B3F6F97");
            });

            modelBuilder.Entity<TinhTp>(entity =>
            {
                entity.HasKey(e => e.MaTp)
                    .HasName("PK__TinhTP__2E62FB676FDD87AF");

                entity.ToTable("TinhTP");

                entity.Property(e => e.MaTp)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Ma_TP")
                    .IsFixedLength();

                entity.Property(e => e.TenTp)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_TP");
            });

            modelBuilder.Entity<XaPhuong>(entity =>
            {
                entity.HasKey(e => e.MaXp)
                    .HasName("PK__XaPhuong__2E62DAE2169EE195");

                entity.ToTable("XaPhuong");

                entity.Property(e => e.MaXp)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ma_XP")
                    .IsFixedLength();

                entity.Property(e => e.MaQh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("Ma_QH")
                    .IsFixedLength();

                entity.Property(e => e.TenXp)
                    .HasMaxLength(50)
                    .HasColumnName("Ten_XP");

                entity.HasOne(d => d.MaQhNavigation)
                    .WithMany(p => p.XaPhuongs)
                    .HasForeignKey(d => d.MaQh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__XaPhuong__Ma_QH__38996AB5");
            });

            modelBuilder.Entity<YeuThichBaiDang>(entity =>
            {
                entity.HasKey(e => new { e.MaBd, e.MaNd })
                    .HasName("PK__YeuThich__5C815D86A4DEC3A9");

                entity.ToTable("YeuThichBaiDang");

                entity.Property(e => e.MaBd)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("Ma_BD")
                    .IsFixedLength();

                entity.Property(e => e.MaNd)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Ma_ND")
                    .IsFixedLength();

                entity.Property(e => e.ThoiGian)
                    .HasColumnType("datetime")
                    .HasColumnName("Thoi_Gian");

                entity.HasOne(d => d.MaBdNavigation)
                    .WithMany(p => p.YeuThichBaiDangs)
                    .HasForeignKey(d => d.MaBd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__YeuThichB__Ma_BD__48CFD27E");

                entity.HasOne(d => d.MaNdNavigation)
                    .WithMany(p => p.YeuThichBaiDangs)
                    .HasForeignKey(d => d.MaNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__YeuThichB__Ma_ND__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
