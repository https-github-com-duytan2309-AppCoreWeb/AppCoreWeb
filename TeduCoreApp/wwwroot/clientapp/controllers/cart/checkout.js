﻿var CheckoutController = function () {
    var cachedObj = {
        streets: [],
        wards: [],
        districts: [],
        provinces: []
    }
    this.initialize = function () {
        $.when(
            loadGetAllStreet(),
            loadGetAllWard(),
            loadGetAllDistrict(),
            loadGetAllProvince())
            .done(function () {
                //registerEvents();
            });
        registerEvents();
    }

    function registerEvents() {
        $('#Province').keypress(
            function () {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetProvincesByKeyString",
                    data: { KeyString: $(this).val() },
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

                        $('.dropdown-menu a').on({
                            click: function () {
                                var value = $(this).text();
                                document.getElementById("Province").value = value;
                                ReloadInputStreet();
                            }
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
        );
        $('#District').keypress(
            function () {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetDistrictsByKeyString",
                    data: { KeyString: $(this).val() },
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
                                ReloadInputStreet();
                            }
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
        );

        $('#Ward').keypress(
            function () {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetStreetsByKeyString",
                    data: { KeyString: $(this).val() },
                    dataType: "json",
                    success: function (response) {
                        wards = response;
                        var render = '';
                        var templateDetails = $('#template-table-ward-details').html();

                        $.each(wards, function (i, item) {
                            render += Mustache.render(templateDetails,
                                {
                                    Name: item.Name,
                                    Id: item.Id
                                });
                        });

                        $('#tbl-ward-details').html(render);

                        $('.dropdown-menu a').on({
                            click: function () {
                                var value = $(this).text();
                                document.getElementById("Ward").value = value;
                                ReloadInputStreet();
                            }
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
        );

        $('#Street').keypress(

            function () {
                var nameWard = document.getElementById("Ward").value;
                var nameDistrict = document.getElementById("District").value;
                var nameProvince = document.getElementById("Province").value;
                if (nameWard !== "" && nameDistrict !== "") {
                    $.ajax({
                        type: "GET",
                        url: "/Address/GetStreetsByKeyStringByDistrictWard",
                        data: { KeyString: $(this).val(), NameWard: nameWard, NameDistrict: nameDistrict },
                        dataType: "json",
                        success: function (response) {
                            wards = response;
                            var render = '';
                            var templateDetails = $('#template-table-street-details').html();

                            $.each(wards, function (i, item) {
                                render += Mustache.render(templateDetails,
                                    {
                                        Name: item.Name,
                                        Id: item.Id
                                    });
                            });

                            $('#tbl-ward-details').html(render);

                            $('.dropdown-menu a').on({
                                click: function () {
                                    var value = $(this).text();
                                    document.getElementById("Street").value = value;
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }

                if (nameProvince !== "" && nameDistrict !== "") {
                    $.ajax({
                        type: "GET",
                        url: "/Address/GetStreetsByKeyStringByProvinceDistrict",
                        data: { KeyString: $(this).val(), NameProvince: nameProvince, NameDistrict: nameDistrict },
                        dataType: "json",
                        success: function (response) {
                            wards = response;
                            var render = '';
                            var templateDetails = $('#template-table-street-details').html();

                            $.each(wards, function (i, item) {
                                render += Mustache.render(templateDetails,
                                    {
                                        Name: item.Name,
                                        Id: item.Id
                                    });
                            });

                            $('#tbl-ward-details').html(render);

                            $('.dropdown-menu a').on({
                                click: function () {
                                    var value = $(this).text();
                                    document.getElementById("Street").value = value;
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }
            }
        );

        $('body').on('click', '#Province', function (e) {
            e.preventDefault();
            var data = document.getElementById("Province").value;
            if (data === "") {
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

                        $('.dropdown-menu a').on({
                            click: function () {
                                var value = $(this).text();
                                document.getElementById("Province").value = value;
                                ReloadInputStreet();
                            }
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
            var nameWard = document.getElementById("Ward").value;

            if (nameProvince === "" && nameWard === "") {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetDistricts",
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
                                ReloadInputStreet();
                            }
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            } else {
                if (nameProvince !== "") {
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
                                    ReloadInputStreet();
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }
                else if (nameWard !== "") {
                    $.ajax({
                        type: "GET",
                        data: { NameWard: nameWard },
                        url: "/Address/GetDistrictsByNameWard",
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
            }
        });

        $('body').on('click', '#Ward', function (e) {
            e.preventDefault();
            var nameDistrict = document.getElementById("District").value;

            if (nameDistrict === "") {
                $("#Ward").attr("placeholder", "Vui Lòng Nhập Vài Từ Tìm ...");
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
                            ReloadInputStreet();
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
            var nameDistrict = document.getElementById("District").value;
            var nameProvince = document.getElementById("Province").value;

            if (nameWard !== "" && nameDistrict !== "") {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetStreetsByNameWardAndNameDistrict",
                    data: {
                        NameWard: nameWard,
                        NameDistrict: nameDistrict
                    },
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
            else if (nameProvince !== "" && nameDistrict !== "") {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetStreetsByNameProvinceAndNameDistrict",
                    data: {
                        NameProvince: nameProvince,
                        NameDistrict: nameDistrict
                    },
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

    function ReloadInputStreet() {
        var inputProvince = document.getElementById("Province").value;
        var inputDistrict = document.getElementById("District").value;
        var inputWard = document.getElementById("Ward").value;

        if (inputProvince !== "" && inputDistrict !== "") {
            $("#Street").removeAttr("disabled");
        }

        if (inputWard !== "" && inputDistrict !== "") {
            $("#Street").removeAttr("disabled");
        }
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
}