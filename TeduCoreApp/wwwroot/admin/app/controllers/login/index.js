var loginController = function () {
    this.initialize = function () {
        registerEvents();
    }

    var registerEvents = function () {
        $('#frmLogin').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'en',
            rules: {
                userName: {
                    required: true
                },
                password: {
                    required: true
                }
            }
        });
        $('#btnLogin').on('click', function (e) {
            if ($('#frmLogin').valid()) {
                e.preventDefault();
                var user = $('#txtUserName').val();
                var password = $('#txtPassword').val();
                login(user, password);
            }
        });
    }

    function login(user, pass) {
        $.ajax({
            type: 'POST',
            data: {
                Email: user,
                Password: pass
            },
            dateType: 'json',
            url: '/Admin/Login/Authen',
            success: function (res) {
                if (res.Success) {
                    tedu.notify('Login Successfully!', 'success');
                    window.location.href = "/admin-trang-chu.html";
                }
                else {
                    tedu.notify('Login failed', 'error');
                }
            }
        })
    }
}