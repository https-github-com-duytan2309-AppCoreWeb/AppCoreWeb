var CompanyController = function () {
    this.initialize = function () {
        registerEvents();
    }
    function registerEvents() {

        $('#btn-edit-contact').on('click', function () {
            
            var textName = $('#name').val();
            var textPhone = $('#phone').val();
            var textEmail = $('#email').val();
            var textWebsite = $('#website').val();
            var textAddress = $('#address').val();
            var textOther = $('#other').val();

            $.ajax({
                type: "POST",
                url: "/Admin/Company/SaveEntityContact",
                data: {
                    Id: "Default",
                    Name: textName,
                    Email: textEmail,
                    Website: textWebsite,
                    Phone: textPhone,
                    Address: textAddress,
                    Other: textOther
                },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    tedu.notify('Lưu thành công!', 'success');
                    tedu.stopLoading();
                },
                error: function (e) {
                    tedu.notify('Has an error in progress', 'error');
                    tedu.stopLoading();
                }
            });

        });
    }
}