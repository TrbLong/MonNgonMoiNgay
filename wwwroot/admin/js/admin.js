

//My Custom

//Hàm xử lý đẩy bài đăng
function setDayBaiDang(mabd, elm) {

	event.preventDefault();

    $.ajax({
        url: 'DayBaiDang',
        type: 'POST',
        data: { id: mabd },
        success: function (data) {
			if (data.tt) {
                $(elm).addClass('disabled')
                getThongBao('success', 'Thành công', 'đã đẩy thành công')
            }
            else {
                alert('warning! - Bạn đã đẩy bài đăng này rồi !')
            }
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về server')
        }
    })
}
//Hàm xử lý ẩn bài đăng
function setAnBaiDang(mabd, elm) {

    event.preventDefault();

    $.ajax({
        url: 'AnBaiDang',
        type: 'POST',
        data: { id: mabd },
        success: function (data) {
            if (data.tt) {
                $(elm).addClass('disabled')
                getThongBao('success', 'Thành công', 'Bạn có thể xem lại trong mục bài đăng của tôi')
            }
            else {
                alert('warning! - Bạn đã ẩn bài đăng này rồi !')
            }
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về server')
        }
    })
}

