var ProfileController = function () {
    this.initialize = function () {
        //loadData();
        registerEvents();
    };

    function registerEvents() {
        $("#UploadAvatar").ejUploadbox({
            saveUrl: "/Files/PostUploadProfilePicture",
            uploadName: "file",
            allowDragAndDrop: true,
            multipleFilesSelection: false,
            success: function (args) {
                tedu.notify("upload image success", "success");
                location.reload();
            },
            asyncUpload: true,
            buttonText: { browse: "browse", upload: "upload", cancel: "cancel", close: "close" }
        });

        $("#UploadAvatars").ejUploadbox({
            saveUrl: "/Files/PostUploadProfilePictures",
            uploadName: "files",
            allowDragAndDrop: true,
            multipleFilesSelection: true,
            success: function (args) {
                tedu.notify("upload image success", "success");
                location.reload();
            },
            asyncUpload: true,
            buttonText: { browse: "browse", upload: "upload", cancel: "cancel", close: "close" }
        });

        $('body').on('click', '#saveProfile', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "POST",
                url: "/Admin/User/SaveEntity",
                data: {
                    Id: that,
                    FullName: $('#txtfull_name').val(),
                    Email: $('#txtemail').val(),
                    UserName: $('#txtuser_name').val(),
                    PhoneNumber: $('#txtphone').val()
                },
                beforeSend: function () {
                },
                success: function (response) {
                    tedu.notify("upload data success", "success");
                    location.reload();
                },
                error: function (e) {
                    tedu.notify('Has an error in progress', 'error');
                    tedu.stopLoading();
                }
            });
        });

        $('body').on('click', '#resetProfile', function (e) {
            e.preventDefault();
            location.reload();
        });

        $("#form-change-pass").ajaxSubmit({
            type: "POST",
            url: '/Manage/ChangePassword',
            data: {
                OldPassword: $("#OldPassword").val(),
                NewPassword: $("#NewPassword").val(),
                ConfirmPassword: $("#ConfirmPassword").val(),
                StatusMessage: "Status Message"
            },
            success: function (response) {
                alert("Cập nhật mật khẩu thành công!");
            },
            error: function (e) {
                alert("Cập nhật mật khẩu thất bại!");
            }
        });
    }

    function checkPass(Id, passOld) {
        //var result = false;
        $.ajax({
            type: "POST",
            url: "/Admin/User/CheckPassOld",
            data: {
                id: Id,
                pass: passOld
            },
            success: function (response) {
                //result = response;
                tedu.notify('Kểm tra mật khẩu thành công!!!', 'success');
            },
            error: function (e) {
                tedu.notify('Lỗi trong quá trình kiểm tra mật khẩu!!!', 'error');
            }
        });
        //return result;
    }
    function changePassword(passOld, passNew, passComfirm) {
        $.ajax({
            type: "POST",
            url: "/Manage/ChangePassword",
            data: {
                id: Id,
                OldPassword: passOld,
                NewPassword: passNew,
                ConfirmPassword: passComfirm,
                StatusMessage: "Status Message"
            },
            success: function (response) {
                tedu.notify('Thây đổi mật khẩu thành công!!!', 'error');
            },
            error: function (e) {
                tedu.notify('Lỗi trong quá trình thây đổi mật khẩu!!!', 'error');
            }
        });

        return check;
    }
    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/user/GetAllPaging",
            data: {
                categoryId: $('#ddl-category-search').val(),
                keyword: $('#txt-search-keyword').val(),
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                if (response.RowCount > 0) {
                    $.each(response.Results, function (i, item) {
                        $(function () {
                            $("#Uploadbox_" + item.Id).ejUploadbox({
                                asyncUpload: true
                            });
                        });
                        render += Mustache.render(template, {
                            FullName: item.FullName,
                            Id: item.Id,
                            UserName: item.UserName,
                            Avatar: item.Avatar === undefined ? '<img src="/admin-side/images/user.png" width=25 />' : '<img src="' + item.Avatar + '" width=25 />',
                            DateCreated: tedu.dateTimeFormatJson(item.DateCreated),
                            Status: tedu.getStatus(item.Status)
                        });
                    });

                    $("#lbl-total-records").text(response.RowCount);
                    if (render !== undefined) {
                        $('#tbl-content').html(render);
                    }
                    wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);
                }
                else {
                    $('#tbl-content').html('');
                }
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    }
};