const changeForm = async (form) => {
    switch (form) {
        case 'login': {
            document.querySelector('.sign-up').style.display = 'none'
            document.getElementById('loading-text').style.display = 'block'
            document.getElementById('loading-text').innerHTML = ''

            new TypeIt('#loading-text', {
                speed: 50,
                waitUntilVisible: true
            }).type('Vui lòng chờ....', { delay: 500 })
                .go()

            setTimeout(async () => {
                document.querySelector('.box').style.display = 'block'
                document.getElementById('loading-text').style.display = 'none'
            }, 2000)
            break
        }

        case 'register': {
            document.querySelector('.box').style.display = 'none'
            document.getElementById('loading-text').style.display = 'block'
            document.getElementById('loading-text').innerHTML = ''

            new TypeIt('#loading-text', {
                speed: 50,
                waitUntilVisible: true
            }).type('Vui lòng chờ....', { delay: 500 })
                .go()

            setTimeout(async () => {
                document.querySelector('.sign-up').style.display = 'block'
                document.getElementById('loading-text').style.display = 'none'
            }, 2000)
            break
        }
    }
}

//Hàm đăng ký
function dangky() {
    $.getJSON('/Account/createAccount' + '?email=' + $('#registerEmail').val() + '&pass=' + $('#registerPass').val(), function (data) {
        if (data.tt) {
            window.location.href = "/Home/Index";
        }
        else {
            if (data.erro == "email") {
                document.getElementById('registerEmail').style.borderColor = 'red';
                document.getElementById('erroRegister').innerHTML = data.mess;
                $('#erroRegister').show('slow');
                $('#erroRegister').delay(5000).hide('slow');
                $('#erroRegister').focus();
            }
            else {
                document.getElementById('erroRegister').innerHTML = data.mess;
                $('#erroRegister').show('slow');
                $('#erroRegister').delay(5000).hide('slow');
            }
        }
    })
}

//Hàm đăng nhập
function dangnhap() {
    var remember = document.getElementById('loginRemember');
    $.getJSON('/Account/getLogin' + '?email=' + $('#loginEmail').val() + '&pass=' + $('#loginPass').val() + '&re=' + remember.checked, function (data) {
        if (data.tt) {
            window.location.href = $('#loginUrlReturn').val();
        }
        else {
            document.getElementById('erroLogin').innerHTML = data.mess;
            $('#erroLogin').show('slow');
            $('#erroLogin').delay(5000).hide('slow');
        }
    })
}

//Hàm đăng xuất
function dangxuat() {
    $.getJSON('/Account/Logout', function (data) {
        location.replace('/Account/Login');
    })
}

//Bắt sự kiện khi nhấn đăng nhập
$('#formLogin').on('submit', function () {
    event.preventDefault();
    dangnhap()
})

//Bắt sự kiện khi nhấn đăng ký
$('#formRegister').on('submit', function () {
    event.preventDefault();

    if (!passValidate('registerPass', 'registerRePass', 'erroRegister')) {
        return;
    }
    dangky()
})

//Kiểm tra nhập lại mật khẩu
function passValidate(id_pass, re_pass, kt_pass) {
    var pass = document.getElementById(id_pass);
    var pass_re = document.getElementById(re_pass);
    var tb = document.getElementById(kt_pass);
    if (pass.value == pass_re.value) {
        pass_re.style.borderColor = 'green';
        pass.style.borderColor = 'green';
        tb.style.display = 'none';
        return true;
    }
    else {
        pass_re.style.borderColor = 'red';
        pass.style.borderColor = 'red';
        tb.innerHTML = 'Xác nhận mật khẩu sai !';
        tb.style.display = 'block';
        $('#' + kt_pass).delay(5000).hide(1);
        return false;
    }
}

//Gọi API google để đăng nhập

var OAUTHURL = 'https://accounts.google.com/o/oauth2/auth?';
var VALIDURL = 'https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=';
var SCOPE = 'https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email';
var CLIENTID = '157130659490-m66pse93mn73j3lf1ndjql21hi2nprh2.apps.googleusercontent.com';
var REDIRECT = 'https://localhost:9090';
var TYPE = 'token';
var _url = OAUTHURL + 'scope=' + SCOPE + '&client_id=' + CLIENTID + '&redirect_uri=' + REDIRECT + '&response_type=' + TYPE;
var acToken;
var tokenType;
var expiresIn;
var user;

function loginWithGoogle() {
    var win = window.open(_url, "windowname1", 'width=500, height=500');
    var pollTimer = window.setInterval(function () {
        try {
            if (win.document.URL.indexOf(REDIRECT) != -1) {
                window.clearInterval(pollTimer);
                var url = win.document.URL;
                acToken = gup(url, 'access_token');
                tokenType = gup(url, 'token_type');
                expiresIn = gup(url, 'expires_in');

                win.close();
                validateToken(acToken);
            }
        }
        catch (e) {
        }
    }, 500);
}

function loginWithFacebook() {
    getThongBao('warning', 'Đang phát triển', 'Chức năng này đang trong quá trình phát triển, hiện tại không thể sử dụng !')
}

function gup(url, name) {
    namename = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\#&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(url);
    if (results == null)
        return "";
    else
        return results[1];
}

function validateToken(token) {
    getUserInfo();
    $.ajax(
        {
            url: VALIDURL + token,
            data: null,
            success: function (responseText) {
            },
            error: function () {
                getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu đến Google !')
            }
        });
}

function getUserInfo() {
    $.ajax({
        url: 'https://www.googleapis.com/oauth2/v1/userinfo?access_token=' + acToken,
        data: null,
        async: false,
        success: function (resp) {
            user = resp;
            var form_data = new FormData();

            form_data.append('hoten', user.given_name);
            form_data.append('email', user.email);

            //Lấy url ảnh chuyển thành file
            fetch(user.picture).then(async response => {
                const contentType = response.headers.get('content-type')
                const blob = await response.blob()
                const file = new File([blob], user.given_name + ".jpg", { contentType })

                //Chuyển file ảnh vào form data gửi về server
                form_data.append('img_avt', file);

                //Gọi ajax về server
                $.ajax({
                    url: '/Account/loginWithGoogle',
                    type: 'POST',
                    data: form_data,
                    contentType: false,
                    processData: false,
                    success: function () {
                        window.location.href = $('#loginUrlReturn').val();
                    },
                    error: function () {
                        getThongBao('error', 'Lỗi', 'Không thể gửi yêu cầu về máy chủ !')
                    }
                });
            })
        },
        error: function () {
            getThongBao('error', 'Lỗi', 'Không thể lấy thông tin người dùng từ Google !')
        }
    });
}