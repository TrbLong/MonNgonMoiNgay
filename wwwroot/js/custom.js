(function($) {
    "use strict";
	
	/* ..............................................
	   Loader 
	   ................................................. */
	$(window).on('load', function() {
		$('.preloader').fadeOut();
		$('#preloader').delay(550).fadeOut('slow');
		$('body').delay(450).css({
			'overflow': 'visible'
		});
	});

	/* ..............................................
	   Fixed Menu
	   ................................................. */

	$(window).on('scroll', function() {
		if ($(window).scrollTop() > 50) {
			$('.main-header').addClass('fixed-menu');
		} else {
			$('.main-header').removeClass('fixed-menu');
		}
	});

	/* ..............................................
	   Gallery
	   ................................................. */

	$('#slides-shop').superslides({
		inherit_width_from: '.cover-slides',
		inherit_height_from: '.cover-slides',
		play: 5000,
		animation: 'fade',
	});

	$(".cover-slides ul li").append("<div class='overlay-background'></div>");

	/* ..............................................
	   Map Full
	   ................................................. */

	$(document).ready(function() {
		$(window).on('scroll', function() {
			if ($(this).scrollTop() > 100) {
				$('#back-to-top').fadeIn();
			} else {
				$('#back-to-top').fadeOut();
			}
		});
		$('#back-to-top').click(function() {
			$("html, body").animate({
				scrollTop: 0
			}, 600);
			return false;
		});
	});

	/* ..............................................
	   Special Menu
	   ................................................. */

	var Container = $('.container');
	Container.imagesLoaded(function() {
		var portfolio = $('.special-menu');
		portfolio.on('click', 'button', function() {
			$(this).addClass('active').siblings().removeClass('active');
			var filterValue = $(this).attr('data-filter');
			$grid.isotope({
				filter: filterValue
			});
		});
		var $grid = $('.special-list').isotope({
			itemSelector: '.special-grid'
		});
	});

	/* ..............................................
	   BaguetteBox
	   ................................................. */

	baguetteBox.run('.tz-gallery', {
		animation: 'fadeIn',
		noScrollbars: true
	});

	/* ..............................................
	   Offer Box
	   ................................................. */

	$('.offer-box').inewsticker({
		speed: 3000,
		effect: 'fade',
		dir: 'ltr',
		font_size: 13,
		color: '#ffffff',
		font_family: 'Montserrat, sans-serif',
		delay_after: 1000
	});

	/* ..............................................
	   Tooltip
	   ................................................. */

	$(document).ready(function() {
		$('[data-toggle="tooltip"]').tooltip();

		//Danh sách bài đăng tại trang chủ được mặc định lọc theo bài đăng mới nhất
		var $grid = $('.special-list').isotope({
			itemSelector: '.special-grid'
		});
		$grid.isotope({
			filter: '.new-post'
		});
	});

	/* ..............................................
	   Owl Carousel Food Feed
	   ................................................. */

	$('.main-food').owlCarousel({
		loop: true,
		margin: 0,
		dots: false,
		autoplay: true,
		autoplayTimeout: 3000,
		autoplayHoverPause: true,
		navText: ["<i class='fas fa-arrow-left'></i>", "<i class='fas fa-arrow-right'></i>"],
		responsive: {
			0: {
				items: 2,
				nav: true
			},
			600: {
				items: 3,
				nav: true
			},
			1000: {
				items: 5,
				nav: true,
				loop: true
			}
		}
	});

	/* ..............................................
	   Featured Products
	   ................................................. */

	$('.featured-products-box').owlCarousel({
		loop: true,
		margin: 15,
		dots: false,
		autoplay: true,
		autoplayTimeout: 3000,
		autoplayHoverPause: true,
		navText: ["<i class='fas fa-arrow-left'></i>", "<i class='fas fa-arrow-right'></i>"],
		responsive: {
			0: {
				items: 1,
				nav: true
			},
			600: {
				items: 3,
				nav: true
			},
			1000: {
				items: 4,
				nav: true,
				loop: true
			}
		}
	});

	/* ..............................................
	   Scroll
	   ................................................. */

	$(document).ready(function() {
		$(window).on('scroll', function() {
			if ($(this).scrollTop() > 100) {
				$('#back-to-top').fadeIn();
			} else {
				$('#back-to-top').fadeOut();
			}
		});
		$('#back-to-top').click(function() {
			$("html, body").animate({
				scrollTop: 0
			}, 600);
			return false;
		});
	});
}(jQuery));

//My Custom
$(function () {

	//Định dạng số tiền
	$('#pstGiaTien').number(true, 0, ',', '.');

	//Khởi tạo mảng chứa file ảnh
	var listFile = [];

	//Xử lý gán ảnh được thêm từ input vào div hiển thị xem trước
	var imagesPreview = function (input, placeImagePreview) {
		if (input.files) {
			for (i = 0; i < input.files.length; i++) {
				var reader = new FileReader();

				reader.onload = function (event) {
					$($.parseHTML('<img>')).attr('src', event.target.result).attr('class', 'img-preview').appendTo(placeImagePreview);
				}
				reader.readAsDataURL(input.files[i]);

				listFile.push(input.files[i])
			}
		}
	};

	//Xử lý khi nhấn chọn ảnh
	$('#pstImg').on('change', function () {
		var inputFile = document.getElementById('pstImg');
		var fileArea = document.querySelector('.file-drop-area');
		var btnTitle = document.querySelector('.fake-btn');

		inputFile.title = "";
		imagesPreview(this, 'div.preview-images');

		//Nếu listFile không rỗng thì thực hiện các thao tác thêm ảnh và chỉnh kích thước
		if (listFile.length != 0) {
			btnTitle.innerHTML = '<i class="fas fa-plus"></i>';
			btnTitle.style.fontSize = '40px';
			
			fileArea.style.width = '120px';
			fileArea.style.height = '100px';
			fileArea.style.margin = '5px';

			document.querySelector('.btn-remove').style.display = 'block';
        }
	});

	//Hàm xử lý xóa hết ảnh đã chọn
	function clearImgChoose() {
		var fileArea = document.querySelector('.file-drop-area');
		var btnTitle = document.querySelector('.fake-btn');

		//Làm rỗng listFile
		listFile = [];

		btnTitle.innerHTML = 'Kéo thả hoặc chọn ảnh';
		btnTitle.style.fontSize = '20px';

		//Chỉnh kích thước cho vùng thêm ảnh
		fileArea.style.width = '100%';
		fileArea.style.height = '200px';
		fileArea.style.margin = '0 0 20px 0';

		//Ẩn nút xóa và làm rỗng div xem trước ảnh
		document.querySelector('.btn-remove').style.display = 'none';
		document.querySelector('.preview-images').innerHTML = '';
    }

	//Xử lý xóa hết ảnh đã chọn
	$('.btn-remove').on('click', function () {
		clearImgChoose();
	})

	//Xử lý hiển thị quận/huyện sau khi chọn tỉnh
	$('#pstTP').on('change', function () {
		var tinhTP = document.getElementById('pstTP');
		var quanHuyen = document.getElementById('pstQH');
		var xaPhuong = document.getElementById('pstXP');

		$.ajax({
			url: '/Post/getQuanHuyen',
			type: 'POST',
			data: { ma: tinhTP.value },
			success: function (data) {

				//Gán các giá trị của select quận/huyện và xã/phường về mặc định
				quanHuyen.innerHTML = "";
				$('#pstQH').append($('<option>').val('0').text('Chọn Quận/Huyện'));
				xaPhuong.innerHTML = "";
				$('#pstXP').append($('<option>').val('0').text('Chọn Xã/Phường'));

				//Chạy vòng lặp để gán giá trị quận/huyện từ kết quả trả về
				if (data.tt) {
					for (var i = 0; i < data.qh.length; i++) {
						$('#pstQH').append($('<option>').val(data.qh[i].maQh).text(data.qh[i].tenQh));
					}
				}
			},
			error: function () {
				getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
			}
		})
	})

	//Xử lý hiển thị xã/phường sau khi chọn quận/huyện
	$('#pstQH').on('change', function () {
		var quanHuyen = document.getElementById('pstQH');
		var xaPhuong = document.getElementById('pstXP');

		$.ajax({
			url: '/Post/getXaPhuong',
			type: 'POST',
			data: { ma: quanHuyen.value },
			success: function (data) {

				//Gán giá trị của select xã/phường về mặc định
				xaPhuong.innerHTML = "";
				$('#pstXP').append($('<option>').val('0').text('Chọn Xã/Phường'));

				//Chạy vòng lặp để gán giá trị xã/phường từ kết quả trả về
				if (data.tt) {
					for (var i = 0; i < data.xp.length; i++) {
						$('#pstXP').append($('<option>').val(data.xp[i].maXp).text(data.xp[i].tenXp));
					}
				}
			},
			error: function () {
				getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
			}
		})
	})

	//Xử lý tạo bài đăng mới
	$('#frmCreatePost').on('submit', function () {
		event.preventDefault();

		var loaiMon = document.getElementById('pstLoai');
		var tenMon = document.getElementById('pstTenMon');
		var giaTien = document.getElementById('pstGiaTien');
		var moTa = document.getElementById('pstMoTa');
		var tinhTP = document.getElementById('pstTP');
		var quanHuyen = document.getElementById('pstQH');
		var xaPhuong = document.getElementById('pstXP');
		var diaChi = document.getElementById('pstDiaChi');
		var form_data = new FormData();

		//Kiểm tra ảnh món ăn ít nhất phải có 1 ảnh
		if (listFile.length == 0) {
			getThongBao('error', 'Lỗi', 'Vui lòng chọn ít nhất 1 ảnh!')
			return;
		}

		//Kiểm tra loại món đã được chọn
		if (loaiMon.value == "0") {
			getThongBao('error', 'Lỗi', 'Vui lòng chọn loại món!')
			return;
		}

		//Kiểm tra vị trí đã được thêm đủ
		if (tinhTP.value == "0" || quanHuyen.value == "0" || xaPhuong.value == "0") {
			getThongBao('error', 'Lỗi', 'Vui lòng chọn khu vực!')
			return;
		}

		form_data.append('loai', loaiMon.value);
		form_data.append('ten', tenMon.value);
		form_data.append('gia', $('#pstGiaTien').val());
		form_data.append('mota', moTa.value);
		form_data.append('xp', xaPhuong.value);
		form_data.append('diachi', diaChi.value);

		//Gán từng ảnh trong listFile vào form data
		for (var i = 0; i < listFile.length; i++) {
			form_data.append('images', listFile[i]);
		}

		//Gọi ajax xử lý tạo post
		$.ajax({
			url: '/Post/getCreateNew',
			type: 'POST',
			data: form_data,
			contentType: false,
			processData: false,
			success: function () {

				//Làm rỗng list ảnh để thêm lần sau không bị chồng ảnh và làm rỗng các input nhập vào
				loaiMon.value = "0";
				tenMon.value = null;
				giaTien.value = null;
				moTa.value = null;
				tinhTP.value = "0";
				quanHuyen.value = "0";
				xaPhuong.value = "0";
				diaChi.value = null;
				clearImgChoose();

				window.location.href = "/";
			},
			error: function () {
				getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
			}
		})
	})
});

//Hàm xử lý lưu bài đăng
function setLuuBaiDang(mabd, elm) {

	event.preventDefault();

    $.ajax({
        url: '/Post/LuuBaiDang',
        type: 'POST',
        data: { id: mabd },
        success: function (data) {
			if (data.tt) {
				$(elm).addClass('disabled')
                getThongBao('success', 'Thành công', 'Bạn có thể xem lại trong phần bài đăng đã lưu !')
            }
            else {
                getThongBao('warning', 'Thông báo', 'Bạn đã lưu bài đăng này rồi !')
            }
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
        }
    })
}

//Hàm xử lý yêu thích bài đăng
function setYeuThichBaiDang(mabd, elm) {

	event.preventDefault();

	$.ajax({
		url: '/Post/YeuThichBaiDang',
		type: 'POST',
		data: { id: mabd },
		success: function (data) {
			if (data.tt) {
				$(elm).addClass('disabled')
				getThongBao('success', 'Thành công', 'Bạn có thể xem lại trong phần bài đăng đã thích !')
			}
			else {
				getThongBao('warning', 'Thông báo', 'Bạn đã thích bài đăng này rồi !')
			}
		},
		error: function () {
			getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
		}
	})
}

//hàm phản hồi
$('#frmCreateRepost').on('submit', function () {
	event.preventDefault();

	var tieude = document.getElementById('pstTieuDe');
	var noidung = document.getElementById('pstNoiDung');
	var form_data = new FormData();


	//Kiểm tra mục cần phản hồi
	if (tieude.value == "0") {
		getThongBao('error', 'Lỗi', 'Vui lòng nhập mục cần phản hồi!')
		return;
	}


	form_data.append('td', tieude.value);
	form_data.append('nd', noidung.value);


	//Gọi ajax xử lý tạo repost
	$.ajax({
		url: '/Post/PhanHoi',
		type: 'POST',
		data: form_data,
		contentType: false,
		processData: false,
		success: function (form_data) {
			if (form_data.tt) {
				getThongBao('success', 'Thành công', 'đã gửi phản hồi thành công !')
				tieude.value = null;
				noidung.value = null;
				window.location.href = "/";
			}
			else {
				alert('thất bại')
            }
			
		},
		error: function () {
			getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
		}
	})
})

// xử lý tìm kiêm món ăn theo tên
$('#search-post').on('submit', function () {
	event.preventDefault();
	window.location.href = '/Home/Search?d=' + $('#inp-search-post').val();
})
$('#btn-search-post').on('click', function () {
	event.preventDefault();
	window.location.href = '/Home/Search?d=' + $('#inp-search-post').val();
})