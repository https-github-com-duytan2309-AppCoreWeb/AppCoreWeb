var CheckoutController = function () {
    var cachedObj = {
        streets: [],
        wards: [],
        districts: [],
        provinces: []
    }
    this.initialize = function () {
        //$.when(
        //    loadGetAllStreet(),
        //    loadGetAllWard(),
        //    loadGetAllDistrict(),
        //    loadGetAllProvince())
        //    .done(function () {
        //        //registerEvents();
        //    });
        registerEvents();
    }

    function registerEvents() {
        //$('#frmCheckout').validate({
        //    errorClass: 'red',
        //    ignore: [],
        //    lang: 'vi',
        //    rules: {
        //        PaymentMethod: { required: true },
        //        CustomerName: { required: true },
        //        CustomerMobile: { required: true },
        //        Province: { required: true },
        //        District: { required: true },
        //        Ward: { required: true },
        //        Street: { required: true }
        //    }
        //    ,
        //    messages: {
        //        PaymentMethod: { required: "Vui Lòng Chọn Phương Thức Thanh Toán" },
        //        CustomerName: { required: "Vui Lòng Nhập Tên" },
        //        CustomerMobile: { required: "Vui Lòng Nhập Số Điện Thoại" },
        //        Province: { required: "Vui Lòng Nhập Tỉnh(Thành Phố)" },
        //        District: { required: "Vui Lòng Nhập Quận" },
        //        Ward: { required: "Vui Lòng Nhập Phường(Xã)" },
        //        Street: { required: "Vui Lòng Nhập Đường" }
        //    }
        //});

        $('#Province').keypress(function (e) {
            e.preventDefault();
            //Chưa Sửa
            $.ajax({
                type: "GET",
                url: "/Address/GetProvincesByKeyString",
                data: { KeyString: $('#Province').val() },
                processResults: function (data, page) {
                    return {
                        results: data.results,
                        pagination: {
                            more: data.more
                        }
                    };
                },
                dataType: "json",
                success: function (response) {
                    province = response;
                },
                error: function () {
                    tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                }
            });
        });

        $('body').on('click', '#Province', function (e) {
            e.preventDefault();
            var nameDistrict = document.getElementById("District").value;
            if (nameDistrict === "") {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetProvinces",
                    dataType: "json",
                    success: function (response) {
                        provinces = response;
                        var render = '';
                        var templateDetails = $('#template-table-province-details').html();

                        $.each(provinces, function (i, item) {
                            render += Mustache.render(templateDetails,
                                {
                                    Name: item.Name,
                                    Id: item.Id
                                });
                        });

                        $('#tbl-province-details').html(render);

                        $('.dropdown-menu a').click(function () {
                            var value = $(this).text();
                            document.getElementById("Province").value = value;
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
            else {
                //Chưa Sửa
                $.ajax({
                    type: "GET",
                    url: "/Address/GetProvincesByNameDistrict",
                    data: { NameDistrict: nameDistrict },
                    dataType: "json",
                    success: function (response) {
                        province = response;
                        $('.dropdown-menu a').click(function () {
                            document.getElementById("Province").value = province;
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
        });

        $('body').on('click', '#District', function (e) {
            e.preventDefault();
            var nameProvince = document.getElementById("Province").value;
            if (nameProvince === "") {
                //alert('You Must Have Not Provinces!!!');
                location.reload();
                //$.ajax({
                //    type: "GET",
                //    url: "/Address/GetDistricts",
                //    dataType: "json",
                //    success: function (response) {
                //        districts = response;
                //        var render = '';
                //        var templateDetails = $('#template-table-district-details').html();

                //        $.each(districts, function (i, item) {
                //            render += Mustache.render(templateDetails,
                //                {
                //                    Name: item.Name,
                //                    Id: item.Id
                //                });
                //        });

                //        $('#tbl-district-details').html(render);

                //        //$('.dropdown-menu a').click(function () {
                //        //    var value = $(this).text();
                //        //    document.getElementById("District").value = value;

                //        //});

                //        $('.dropdown-menu a').on({
                //            click: function () {
                //                var value = $(this).text();
                //                document.getElementById("District").value = value;
                //            }
                //        });
                //        $('#District').on({
                //            change: function () { alert('AAAAA'); }
                //        });
                //    },
                //    error: function () {
                //        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                //    }
                //});
            } else {
                $.ajax({
                    type: "GET",
                    data: { NameProvince: nameProvince },
                    url: "/Address/GetDistrictsByNameProvince",
                    dataType: "json",
                    success: function (response) {
                        districts = response;
                        var render = '';
                        var templateDetails = $('#template-table-district-details').html();

                        $.each(districts, function (i, item) {
                            render += Mustache.render(templateDetails,
                                {
                                    Name: item.Name,
                                    Id: item.Id
                                });
                        });

                        $('#tbl-district-details').html(render);

                        $('.dropdown-menu a').on({
                            click: function () {
                                var value = $(this).text();
                                document.getElementById("District").value = value;
                            }
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
        });

        $('body').on('click', '#Ward', function (e) {
            e.preventDefault();
            var nameDistrict = document.getElementById("District").value;

            if (nameDistrict === "") {
                location.reload();
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetWardsByNameDistrict",
                    data: { NameDistrict: nameDistrict },
                    dataType: "json",
                    success: function (response) {
                        wards = response;
                        var render = '';
                        var templateDetails = $('#template-table-ward-details').html();
                        $.each(wards, function (i, item) {
                            render += Mustache.render(templateDetails,
                                {
                                    Name: item.Name
                                });
                        });

                        $('#tbl-ward-details').html(render);

                        $('.dropdown-menu a').click(function () {
                            var value = $(this).text();
                            document.getElementById("Ward").value = value;
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
        });

        $('body').on('click', '#Street', function (e) {
            e.preventDefault();

            var nameWard = document.getElementById("Ward").value;

            if (nameWard === "") {
                alert("You must choose Province, District Or Ward");
                location.reload();
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetStreetsNameWard",
                    data: { NameWard: nameWard },
                    dataType: "json",
                    success: function (response) {
                        streets = response;
                        var render = '';
                        var templateDetails = $('#template-table-street-details').html();
                        $.each(streets, function (i, item) {
                            render += Mustache.render(templateDetails,
                                {
                                    Name: item.Name
                                });
                        });

                        $('#tbl-street-details').html(render);

                        $('.dropdown-menu a').click(function () {
                            var value = $(this).text();
                            document.getElementById("Street").value = value;
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
        });
    }

    function loadGetAllStreet() {
        return $.ajax({
            type: "GET",
            url: "/Address/GetStreets",
            dataType: "json",
            success: function (response) {
                cachedObj.streets = response;
                tedu.notify('Load Streets Success!!!', 'success');
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
            }
        });
    }

    function loadGetAllWard() {
        return $.ajax({
            type: "GET",
            url: "/Address/GetWards",
            dataType: "json",
            success: function (response) {
                cachedObj.wards = response;
                tedu.notify('Load Wards Success!!!', 'success');
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
            }
        });
    }

    function loadGetAllDistrict() {
        return $.ajax({
            type: "GET",
            url: "/Address/GetDistricts",
            dataType: "json",
            success: function (response) {
                cachedObj.districts = response;
                tedu.notify('Load Districts Success!!!', 'success');
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
            }
        });
    }

    function loadGetAllProvince() {
        return $.ajax({
            type: "GET",
            url: "/Address/GetProvinces",
            dataType: "json",
            success: function (response) {
                cachedObj.provinces = response;
                tedu.notify('Load Provinces Success!!!', 'success');
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
            }
        });
    }

    //function loadChangeDistrict() {
    //    $('body').on('change', '#District', function (e) {
    //        e.preventDefault();
    //        //alert('District has load!!');
    //        var nameDistrict = document.getElementById("District").value;
    //        $.ajax({
    //            type: "GET",
    //            url: "/Address/GetProvincesByNameDistrict",
    //            data: { NameDistrict: nameDistrict },
    //            dataType: "json",
    //            success: function (response) {
    //                province = response;
    //                document.getElementById("Province").value = province;
    //            },
    //            error: function () {
    //                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
    //            }
    //        });
    //    });
    //}
}