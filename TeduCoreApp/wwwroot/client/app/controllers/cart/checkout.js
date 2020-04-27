var CheckoutController = function () {
    var cachedObj = {
        streets: [],
        wards: [],
        districts: [],
        provinces: [],
        paymentMethods: []
    };

    this.initialize = function () {
        //$.when(
        //    loadGetAllStreet(),
        //    loadGetAllWard(),
        //    loadGetAllDistrict(),
        //    loadGetAllProvince(),
        //    loadDataShipCode(),
        //    LoadShipCodeIdAdress())
        //    .done(function () {
        //    });

        registerEvents();
    };

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
                                var valueProvince = $(this).text();
                                document.getElementById("Province").value = valueProvince;
                                document.getElementById("Street").value = "";
                                document.getElementById("Ward").value = "";
                                document.getElementById("District").value = "";
                                $("#Street").attr("disabled", "disabled");
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
                var nameProvince = document.getElementById("Province").value;
                document.getElementById("Ward").value;
                if (nameProvince === "") {
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
                                    document.getElementById("Ward").value = "";
                                    $("#Street").removeAttr("disabled");
                                    ReloadInputProvince();
                                    LoadShipCodeIdAdress(value, nameProvince);
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }
                else {
                    if (nameProvince !== "") {
                        $.ajax({
                            type: "GET",
                            url: "/Address/GetDistrictsByKeyStringAndNameProvince",
                            data: { KeyString: $(this).val(), NameProvince: nameProvince },
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
                                        document.getElementById("Ward").value = "";
                                        $("#Street").removeAttr("disabled");
                                        LoadShipCodeIdAdress(value, nameProvince);
                                    }
                                });
                            },
                            error: function () {
                                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                            }
                        });
                    }
                }
            }
        );

        $('#Ward').keypress(
            function () {
                var nameProvince = document.getElementById("Province").value;
                var nameDistrict = document.getElementById("District").value;
                if (nameProvince === "" && nameDistrict === "") {
                    $.ajax({
                        type: "GET",
                        url: "/Address/GetWardsByKeyString",
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
                                    var valueWard = $(this).text();
                                    document.getElementById("Ward").value = valueWard;
                                    ReloadInputDistrict();
                                    ReloadInputProvince();
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }
                else {
                    if (nameDistrict !== "") {
                        $.ajax({
                            type: "GET",
                            url: "/Address/GetWardsByKeyStringAndNameDistrict",
                            data: { KeyString: $(this).val(), NameDistrict: nameDistrict },
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
                                        var valueWard = $(this).text();
                                        document.getElementById("Ward").value = valueWard;
                                    }
                                });
                            },
                            error: function () {
                                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                            }
                        });
                    }
                    else if (nameProvince !== "") {
                        $.ajax({
                            type: "GET",
                            url: "/Address/GetWardsByKeyStringAndNameProvince",
                            data: { KeyString: $(this).val(), NameProvince: nameProvince },
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
                                        ReloadInputDistrict();
                                        $("#Street").removeAttr("disabled");
                                    }
                                });
                            },
                            error: function () {
                                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                            }
                        });
                    }
                }
            }
        );

        $('#Street').keypress(
            function () {
                var nameWard = document.getElementById("Ward").value;
                var nameDistrict = document.getElementById("District").value;
                //if (nameWard !== "") {
                //    $.ajax({
                //        type: "GET",
                //        url: "/Address/GetStreetsByKeyStringAndByNameWard",
                //        data: { KeyString: $(this).val(), NameWard: nameWard },
                //        dataType: "json",
                //        success: function (response) {
                //            streets = response;
                //            var render = '';
                //            var templateDetails = $('#template-table-street-details').html();

                //            $.each(streets, function (i, item) {
                //                render += Mustache.render(templateDetails,
                //                    {
                //                        Name: item.Name,
                //                        Id: item.Id
                //                    });
                //            });

                //            $('#tbl-ward-details').html(render);

                //            $('#tbl-street-details a').on({
                //                click: function () {
                //                    var valueStreet = $(this).text();
                //                    document.getElementById("Street").value = valueStreet;
                //                }
                //            });
                //        },
                //        error: function () {
                //            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                //        }
                //    });
                //}
                //else
                if (nameDistrict !== "") {
                    $.ajax({
                        type: "GET",
                        url: "/Address/GetStreetsByKeyStringAndByNameDistrict",
                        data: { KeyString: $(this).val(), NameDistrict: nameDistrict },
                        dataType: "json",
                        success: function (response) {
                            streets = response;
                            var render = '';
                            var templateDetails = $('#template-table-street-details').html();

                            $.each(streets, function (i, item) {
                                render += Mustache.render(templateDetails,
                                    {
                                        Name: item.Name,
                                        Id: item.Id
                                    });
                            });

                            $('#tbl-street-details').html(render);

                            $('#tbl-street-details a').on({
                                click: function () {
                                    var valueStreet = $(this).text();
                                    document.getElementById("Street").value = valueStreet;
                                    ReloadInputWard();
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

        $("#Province").click(function () {
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
                            var valueProvince = $(this).text();
                            document.getElementById("Province").value = valueProvince;
                            document.getElementById("Street").value = "";
                            document.getElementById("Ward").value = "";
                            document.getElementById("District").value = "";
                            $("#Street").attr("disabled", "disabled");
                        }
                    });
                },
                error: function () {
                    tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                }
            });
        });

        $("#District").click(function () {
            var nameProvince = document.getElementById("Province").value;
            if (nameProvince === "") {
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
                                var valueDistrict = $(this).text();
                                document.getElementById("District").value = valueDistrict;
                                document.getElementById("Ward").value = "";
                                $("#Street").removeAttr("disabled");
                                ReloadInputProvince();
                                LoadShipCodeIdAdress();
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
                                    var valueDistrict = $(this).text();
                                    document.getElementById("District").value = valueDistrict;
                                    document.getElementById("Ward").value = "";
                                    $("#Street").removeAttr("disabled");
                                    LoadShipCodeIdAdress();
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

        $("#Ward").click(function () {
            var nameDistrict = document.getElementById("District").value;
            var nameProvince = document.getElementById("Province").value;
            if (nameDistrict === "" && nameProvince === "") {
                $("#Ward").attr("placeholder", "Vui Lòng Nhập Vài Từ Tìm ...");
            }
            else {
                if (nameDistrict !== "") {
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

                            $('#tbl-ward-details a').on({
                                click: function () {
                                    var valueWard = $(this).text();
                                    document.getElementById("Ward").value = valueWard;
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }
                else if (nameProvince !== "") {
                    $.ajax({
                        type: "GET",
                        url: "/Address/GetWardsByNameProvince",
                        data: { NameProvince: nameProvince },
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

                            $('#tbl-ward-details a').on({
                                click: function () {
                                    var valueWard = $(this).text();
                                    document.getElementById("Ward").value = valueWard;
                                    ReloadInputDistrict();
                                    $("#Street").removeAttr("disabled");
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

        $("#Street").click(function () {
            //var nameWard = document.getElementById("Ward").value;
            var nameDistrict = document.getElementById("District").value;
            if (nameWard !== "") {
                $.ajax({
                    type: "GET",
                    url: "/Address/GetStreetsByNameDistrict",
                    data: {
                        NameDistrict: nameDistrict,
                    },
                    dataType: "json",
                    success: function (response) {
                        streets = response;
                        var rep = (response + '').length;
                        if (rep === 0) {
                            alert('Không có Dữ Liệu Tìm Thấy! Vui Lòng Nhập Địa Chỉ Đường Vào Form');
                        }
                        else {
                            var render = '';
                            var templateDetails = $('#template-table-street-details').html();
                            $.each(streets, function (i, item) {
                                render += Mustache.render(templateDetails,
                                    {
                                        Name: item.Name
                                    });
                            });

                            $('#tbl-street-details').html(render);

                            $('#tbl-street-details a').on({
                                click: function () {
                                    var valueStreet = $(this).text();
                                    document.getElementById("Street").value = valueStreet;
                                }
                            });
                        }
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
            //else {
            //    $.ajax({
            //        type: "GET",
            //        url: "/Address/GetStreetsByNameDistrict",
            //        data: {
            //            NameDistrict: nameDistrict
            //        },
            //        dataType: "json",
            //        success: function (response) {
            //            //dataStreet = response;
            //            ////xử khi không có dữ liệu if Ressponce == Null
            //            //var rep = (response + '').length;
            //            //if (rep === 0) {
            //            //    alert('Không có Dữ Liệu Tìm Thấy! Vui Lòng Nhập Địa Chỉ Đường Vào Form Hoặc Nhập Thêm Tên Phường(Xã) Để Tìm Kiếm');
            //            //}
            //            //else {
            //            var render = '';
            //            var templateDetails = $('#template-table-street-details').html();
            //            $.each(response, function (i, item) {
            //                render += Mustache.render(templateDetails,
            //                    {
            //                        Name: item.Name
            //                    });
            //            });

            //            $('#tbl-street-details').html(render);

            //            $('#tbl-street-details a').on({
            //                click: function () {
            //                    var valueStreet = $(this).text();
            //                    document.getElementById("Street").value = valueStreet;
            //                    ReloadInputWard();
            //                }
            //            });
            //            //}
            //        },
            //        error: function () {
            //            tedu.notify('Có lỗi trong quá trình lấy dữ liệu Đường', 'error');
            //        }
            //    });
            //}
        });
    }

    function ReloadInputDistrict() {
        var inputWard = document.getElementById("Ward").value;
        if (inputWard !== "") {
            $.ajax({
                type: "GET",
                data: { NameWard: inputWard },
                url: "/Address/GetDistrictsByNameWard",
                dataType: "json",
                success: function (response) {
                    document.getElementById("District").value = response.Name;
                },
                error: function () {
                    tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                }
            });
        }
    }

    function ReloadInputProvince() {
        var inputDistrict = document.getElementById("District").value;
        var inputWard = document.getElementById("Ward").value;

        if (inputWard !== "") {
            $.ajax({
                type: "GET",
                data: { NameWard: inputWard },
                url: "/Address/GetProvinceByNameWard",
                dataType: "json",
                success: function (response) {
                    var value = response;
                    document.getElementById("Province").value = value.Name;
                },
                error: function () {
                    tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                }
            });
        }

        if (inputDistrict !== "") {
            $.ajax({
                type: "GET",
                data: { NameDistrict: inputDistrict },
                url: "/Address/GetProvinceByNameDistrict",
                dataType: "json",
                success: function (response) {
                    var value = response;
                    document.getElementById("Province").value = value.Name;
                },
                error: function () {
                    tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                }
            });
        }
    }

    function ReloadInputWard() {
        var inputStreet = document.getElementById("Street").value;
        if (inputStreet !== "") {
            $.ajax({
                type: "GET",
                data: { NameStreet: inputStreet },
                url: "/Address/GetWardByNameStreet",
                dataType: "json",
                success: function (response) {
                    var value = response;
                    document.getElementById("Ward").value = value.Name;
                },
                error: function () {
                    tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                }
            });
        }
    }

    function loadGetAllStreet() {
        return $.ajax({
            type: "GET",
            url: "/Address/GetStreets",
            dataType: "json",
            success: function (response) {
                cachedObj.streets = response;
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
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
            }
        });
    }

    function loadDataShipCode() {
        $.ajax({
            url: '/ShipCodes/GetShipCodes',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var templateShipCode = $('#template-table-shipcode').html();
                var render = "";

                $.each(response, function (i, item) {
                    render += Mustache.render(templateShipCode,
                        {
                            Id: item.Id,
                            Carriers: item.Carriers,
                            DiliveryTime: item.DeliveryTime,
                            CollectionFee: item.CollectionFee,
                            ZipCode: item.ZipCode,
                            Total: item.Total
                        });
                });
                $('#template-shipcode').html(render);

                $('.dropdown-menu a').on({
                    click: function () {
                        var value = $(this).text();
                        document.getElementById("Carriers").value = value;

                        var that = $(this).data('id');
                        GetShipCodeById(that);
                    }
                });
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
            }
        });
    }
    //Load Total with Id ShipCode
    function LoadShipCodeIdAdress() {
        var nameProvince = document.getElementById("Province").value;
        var nameDistrict = document.getElementById("District").value;
        $.ajax({
            url: '/ShipCodes/GetShipCodeIdAdress',
            type: 'GET',
            dataType: 'json',
            data: {
                NameProvince: nameProvince,
                NameDistrict: nameDistrict
            },
            success: function (response) {
                document.getElementById("CollectionFee").value = response.CollectionFee;
                document.getElementById("ZipCode").value = response.ZipCode;
                document.getElementById("DiliveryTime").value = tedu.formattedDate(response.DeliveryTime);
                LoadShipCodeTotalAndCarres(nameProvince, nameDistrict);
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu load phương thức giao hàng', 'error');
            }
        });
    }

    function LoadShipCodeTotalAndCarres(nameProvince, nameDistrict) {
        $.ajax({
            url: '/ShipCodes/GetListShipCodeIdAdress',
            type: 'GET',
            dataType: 'json',
            data: {
                NameProvince: nameProvince,
                NameDistrict: nameDistrict
            },
            success: function (response) {
                var templateShipCode = $('#template-table-shipcode').html();
                var render = "";
                $.each(response, function (i, item) {
                    render += Mustache.render(templateShipCode,
                        {
                            IdCarriers: item.Id,
                            Carriers: item.Carriers
                        });
                });
                $('#template-shipcode').html(render);

                $("#template-shipcode a").click(function () {
                    GetShipCodeByIdAddress($(this).data("id"));
                });
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu load phương thức giao hàng', 'error');
            }
        });
    }

    function GetShipCodeByIdAddress(idShipCode) {
        $.ajax({
            url: '/ShipCodes/GetShipCodeById',
            type: 'GET',
            dataType: 'json',
            data: {
                IdShipCode: idShipCode,
            },
            success: function (response) {
                document.getElementById("ShipCodeId").value = response.Id;
                document.getElementById("Total").value = response.Total;

                //document.getElementById("Carriers").value = response.Carriers;
                $("#Carriers").val(response.Carriers);
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu get Id', 'error');
            }
        });
    }
};