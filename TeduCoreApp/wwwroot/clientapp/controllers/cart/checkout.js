var CheckoutController = function () {
    var cachedObj = {
        streets: [],
        wards: [],
        districts: [],
        provinces: [],
        paymentMethods: []
    };

    this.initialize = function () {
        $.when(
            loadGetAllStreet(),
            loadGetAllWard(),
            loadGetAllDistrict(),
            loadGetAllProvince(),
            loadDataShipCode(),
            LoadShipCodeIdAdress())
            .done(function () {
            });

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
                                var value = $(this).text();
                                document.getElementById("Province").value = value;
                                /*ReloadInputStreet();*/
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
                var nameWard = document.getElementById("Ward").value;
                if (nameProvince === "" && nameWard === "") {
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
                                    ReloadInputProvince();
                                    ReloadInputStreet();
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }
                if (nameProvince !== "" && nameWard === "") {
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
                                    ReloadInputProvince();
                                    ReloadInputStreet();
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }

                if (nameWard !== "") {
                    $.ajax({
                        type: "GET",
                        url: "/Address/GetDistrictsByNameWard",
                        data: { NameWard: nameWard },
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
                                    ReloadInputProvince();
                                    ReloadInputStreet();
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
                                    var value = $(this).text();
                                    document.getElementById("Ward").value = value;
                                    ReloadInputDistrict();
                                    ReloadInputProvince();
                                    ReloadInputStreet();
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }

                if (nameProvince !== "" && nameDistrict === "") {
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
                                    ReloadInputProvince();
                                    ReloadInputStreet();
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }

                if (nameProvince === "" && nameDistrict !== "") {
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
                                    var value = $(this).text();
                                    document.getElementById("Ward").value = value;
                                    ReloadInputDistrict();
                                    ReloadInputProvince();
                                    ReloadInputStreet();
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
                        url: "/Address/GetWardsByKeyStringAndNameDistrictAndNameProvince",
                        data: { KeyString: $(this).val(), NameDistrict: nameDistrict, NameProvince: nameProvince },
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
                                    ReloadInputProvince();
                                    ReloadInputStreet();
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

        $('#Street').keypress(
            function () {
                var nameWard = document.getElementById("Ward").value;
                var nameDistrict = document.getElementById("District").value;
                var nameProvince = document.getElementById("Province").value;
                if (nameWard !== "") {
                    $.ajax({
                        type: "GET",
                        url: "/Address/GetStreetsByKeyStringByWard",
                        data: { KeyString: $(this).val(), NameWard: nameWard },
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
                                    ReloadInputWard();
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
                                $('#Province').val(value);
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
                                ReloadInputProvince();
                                ReloadInputStreet();
                                LoadShipCodeIdAdress(value, nameProvince);
                            }
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            } else {
                if (nameProvince !== "" && nameWard === "") {
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
                                    ReloadInputProvince();
                                    ReloadInputStreet();
                                    LoadShipCodeIdAdress(value, nameProvince);
                                }
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                }
                else if (nameWard !== "" && nameProvince === "") {
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
                                    ReloadInputProvince();
                                    ReloadInputStreet();
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
        });

        $('body').on('click', '#Ward', function (e) {
            e.preventDefault();
            var nameDistrict = document.getElementById("District").value;
            var nameProvince = document.getElementById("Province").value;
            if (nameDistrict === "" && nameProvince === "") {
                $("#Ward").attr("placeholder", "Vui Lòng Nhập Vài Từ Tìm ...");
            }
            else {
                if (nameProvince !== "" && nameDistrict !== "") {
                    $.ajax({
                        type: "GET",
                        url: "/Address/GetWardsByNameDistrictAndNameProvince",
                        data: {
                            NameProvince: nameProvince,
                            NameDistrict: nameDistrict
                        },
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
                                ReloadInputDistrict();
                                ReloadInputProvince();
                                ReloadInputStreet();
                            });
                        },
                        error: function () {
                            tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                        }
                    });
                } else {
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

                                $('.dropdown-menu a').click(function () {
                                    var value = $(this).text();
                                    document.getElementById("Ward").value = value;
                                    ReloadInputDistrict();
                                    ReloadInputProvince();
                                    ReloadInputStreet();
                                });
                            },
                            error: function () {
                                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                            }
                        });
                    }

                    if (nameProvince !== "") {
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

                                $('.dropdown-menu a').click(function () {
                                    var value = $(this).text();
                                    document.getElementById("Ward").value = value;
                                    ReloadInputDistrict();
                                    ReloadInputProvince();
                                    ReloadInputStreet();
                                });
                            },
                            error: function () {
                                tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                            }
                        });
                    }
                }
            }
        });

        $('body').on('click', '#Street', function (e) {
            e.preventDefault();

            var nameWard = document.getElementById("Ward").value;
            var nameDistrict = document.getElementById("District").value;
            var nameProvince = document.getElementById("Province").value;

            if (nameDistrict !== "" && nameWard !== "") {
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
                            ReloadInputWard();
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
                            ReloadInputWard();
                        });
                    },
                    error: function () {
                        tedu.notify('Có lỗi trong xử lý yêu cầu', 'error');
                    }
                });
            }
        });

        //$('#District').on('change', function (e) {
        //    e.preventDefault();
        //    var nameProvince = document.getElementById("Province").value;
        //    var nameDistrict = document.getElementById("District").value;
        //    LoadShipCodeIdAdress(nameDistrict, nameProvince);
        //});
    }

    function ReloadInputDistrict() {
        var inputWard = document.getElementById("Ward").value;
        if (inputWard !== "") {
            $.ajax({
                type: "GET",
                data: { NameWard: inputWard },
                url: "/Address/GetDistrictByWard",
                dataType: "json",
                success: function (response) {
                    var value = response;
                    document.getElementById("District").value = value.Name;
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
                url: "/Address/GetProvinceByWard",
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
                url: "/Address/GetProvinceByDistrict",
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

    function ReloadInputStreet() {
        var inputDistrict = document.getElementById("District").value;
        var inputWard = document.getElementById("Ward").value;

        if (inputDistrict !== "") {
            $("#Street").removeAttr("disabled");
        }

        if (inputWard !== "") {
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
                //tedu.notify('Load Streets Success!!!', 'success');
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
                //tedu.notify('Load Wards Success!!!', 'success');
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
                //tedu.notify('Load Districts Success!!!', 'success');
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
                //    tedu.notify('Load Provinces Success!!!', 'success');
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

    function LoadShipCodeIdAdress() {
        var nameProvince = document.getElementById("Province").value;
        var nameDistrict = document.getElementById("District").value;
        if (nameDistrict !== "") {
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
    }

    //Load Total with Id ShipCode
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
                        GetShipCodeByIdAddress(that);
                    }
                });
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu load phương thức giao hàng', 'error');
            }
        });
    }

    function GetShipCodeById(idShipCode) {
        $.ajax({
            url: '/ShipCodes/GetShipCodeById',
            type: 'GET',
            dataType: 'json',
            data: {
                id: idShipCode,
            },
            success: function (response) {
                var data = response;
                document.getElementById("ShipCodeId").value = data.Id;
                document.getElementById("CollectionFee").value = data.CollectionFee;
                document.getElementById("ZipCode").value = data.ZipCode;
                document.getElementById("DiliveryTime").value = tedu.formattedDate(data.DeliveryTime);
                document.getElementById("Total").value = data.Total;
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu get Id', 'error');
            }
        });
    }

    function GetShipCodeByIdAddress(idShipCode) {
        $.ajax({
            url: '/ShipCodes/GetShipCodeById',
            type: 'GET',
            dataType: 'json',
            data: {
                id: idShipCode,
            },
            success: function (response) {
                var data = response;
                document.getElementById("ShipCodeId").value = data.Id;
                document.getElementById("Total").value = data.Total;
            },
            error: function () {
                tedu.notify('Có lỗi trong xử lý yêu cầu get Id', 'error');
            }
        });
    }
};