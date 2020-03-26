var BillController = function () {
    var cachedObj = {
        products: [],
        colors: [],
        sizes: [],
        paymentMethods: [],
        billStatuses: [],
        images: []
    }

    this.initialize = function () {
        $.when(loadBillStatus(),
            loadProducts(),
            loadPaymentMethod(),
            loadColors(),
            loadSizes())
            .done(function () {
                loadData();
            });

        registerEvents();
    }

    function registerEvents() {
        $('#txtFromDate, #txtToDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtCustomerName: { required: true },
                txtCustomerAddress: { required: true },
                txtCustomerMobile: { required: true },
                txtCustomerMessage: { required: true },
                ddlBillStatus: { required: true }
            }
        });
        $('#txt-search-keyword').keypress(function (e) {
            if (e.which === 13) {
                e.preventDefault();
                loadData();
            }
        });
        $("#btn-search").on('click', function () {
            loadData();
        });

        $("#btn-create").on('click', function () {
            resetFormMaintainance();
            $('#modal-detail').modal('show');
        });
        $("#ddl-show-page").on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });

        $('body').on('click', '.btn-view', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "GET",
                url: "/BillDetails/GetById",
                data: { id: that },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    var data = response;
                    $('#hidId').val(data.Id);
                    $('#txtCustomerName').val(data.CustomerName);
                    $('#txtCustomerAddress').val(data.CustomerAddress);
                    $('#txtCustomerMobile').val(data.CustomerMobile);
                    $('#txtCustomerMessage').val(data.CustomerMessage);
                    $('#ddlPaymentMethod').val(data.PaymentMethod);
                    $('#ddlCustomerId').val(data.CustomerId);
                    $('#ddlBillStatus').val(data.BillStatus);

                    var txtStatus = "";
                    if (data.Status === 0) {
                        txtStatus += "<h3>Đơn Hàng Đang Được Xét Duyệt</h3>";
                        $('#txtStatus').html(txtStatus);
                    }
                    else {
                        txtStatus += "<h3>Đơn Hàng Đã Được Chốt</h3>";
                        $('#txtStatus').html(txtStatus);
                    }

                    var billDetails = data.BillDetails;
                    if (data.BillDetails !== null && data.BillDetails.length > 0) {
                        var render = '';
                        var templateDetails = $('#template-table-bill-details').html();
                        $.each(billDetails, function (i, item) {
                            var products = getProductOptions(item.ProductId);
                            var colors = getColorOptions(item.ColorId, data.Status);
                            var sizes = getSizeOptions(item.SizeId, data.Status);
                            var images = getImageOptions(item.ProductId);
                            var quality = '';
                            if (data.Status === 0) {
                                quality += '<input type="number" class="txtQuantity" value="' + item.Quantity + '" />';
                            }
                            else {
                                quality += '<input type="number" class="txtQuantity" value="' + item.Quantity + '" disabled />';
                            }
                            render += Mustache.render(templateDetails,
                                {
                                    Id: item.Id,
                                    Products: products,
                                    Images: images,
                                    Colors: colors,
                                    Sizes: sizes,
                                    Quantity: quality,
                                    Status: data.BillStatus
                                });
                        });
                        $('#tbl-bill-details').html(render);
                    }
                    $('#modal-detail').modal('show');
                    tedu.stopLoading();
                },
                error: function (e) {
                    tedu.notify('Has an error in progress', 'error');
                    tedu.stopLoading();
                }
            });
        });

        $('#btnSave').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidId').val();
                var customerName = $('#txtCustomerName').val();
                var customerAddress = $('#txtCustomerAddress').val();
                var customerId = $('#ddlCustomerId').val();
                var customerMobile = $('#txtCustomerMobile').val();
                var customerMessage = $('#txtCustomerMessage').val();
                var paymentMethod = $('#ddlPaymentMethod').val();
                var billStatus = $('#ddlBillStatus').val();
                //bill detail

                var billDetails = [];
                $.each($('#tbl-bill-details tr'), function (i, item) {
                    billDetails.push({
                        Id: $(item).data('id'),
                        ProductId: $(item).find('select.ddlProductId').first().val(),
                        Quantity: $(item).find('input.txtQuantity').first().val(),
                        ColorId: $(item).find('select.ddlColorId').first().val(),
                        SizeId: $(item).find('select.ddlSizeId').first().val(),
                        BillId: id
                    });
                });

                $.ajax({
                    type: "POST",
                    url: "/BillDetails/SaveEntity",
                    data: {
                        Id: id,
                        BillStatus: billStatus,
                        CustomerAddress: customerAddress,
                        CustomerId: customerId,
                        CustomerMessage: customerMessage,
                        CustomerMobile: customerMobile,
                        CustomerName: customerName,
                        PaymentMethod: paymentMethod,
                        Status: 1,
                        BillDetails: billDetails
                    },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (response) {
                        tedu.notify('Save order successfully', 'success');
                        $('#modal-detail').modal('hide');
                        resetFormMaintainance();

                        tedu.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        tedu.notify('Không Lưu Được', 'error');
                        tedu.stopLoading();
                    }
                });
                return false;
            }
        });

        $('#btnAddDetail').on('click', function () {
            var template = $('#template-table-bill-details').html();
            var products = getProductOptions(null);
            var colors = getColorOptions(null);
            var sizes = getSizeOptions(null);
            var render = Mustache.render(template,
                {
                    Id: 0,
                    Products: products,
                    Colors: colors,
                    Sizes: sizes,
                    Quantity: 0,
                    Total: 0
                });
            $('#tbl-bill-details').append(render);
        });

        $('body').on('click', '.btn-delete-detail', function () {
            $(this).parent().parent().remove();
        });

        $("#btnExport").on('click', function () {
            var that = $('#hidId').val();
            $.ajax({
                type: "POST",
                url: "/BillDetails/ExportExcel",
                data: { billId: that },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    window.location.href = response;

                    tedu.stopLoading();
                }
            });
        });
    }

    function loadBillStatus() {
        return $.ajax({
            type: "GET",
            url: "/admin/bill/GetBillStatus",
            dataType: "json",
            success: function (response) {
                cachedObj.billStatuses = response;
                var render = "";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Value + "'>" + item.Name + "</option>";
                });
                $('#ddlBillStatus').html(render);
            }
        });
    }

    function loadPaymentMethod() {
        return $.ajax({
            type: "GET",
            url: "/admin/bill/GetPaymentMethod",
            dataType: "json",
            success: function (response) {
                cachedObj.paymentMethods = response;
                var render = "";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Value + "'>" + item.Name + "</option>";
                });
                $('#ddlPaymentMethod').html(render);
            }
        });
    }

    function loadProducts() {
        return $.ajax({
            type: "GET",
            url: "/BillDetails/GetAllProduct",
            dataType: "json",
            success: function (response) {
                cachedObj.products = response;
            },
            error: function () {
                tedu.notify('Has an error in progress', 'error');
            }
        });
    }

    function loadColors() {
        return $.ajax({
            type: "GET",
            url: "/BillDetails/GetColors",
            dataType: "json",
            success: function (response) {
                cachedObj.colors = response;
            },
            error: function () {
                tedu.notify('Has an error in progress', 'error');
            }
        });
    }

    function loadSizes() {
        return $.ajax({
            type: "GET",
            url: "/BillDetails/GetSizes",
            dataType: "json",
            success: function (response) {
                cachedObj.sizes = response;
            },
            error: function () {
                tedu.notify('Has an error in progress', 'error');
            }
        });
    }

    function getProductOptions(selectedId) {
        var products = "<div ";
        $.each(cachedObj.products, function (i, product) {
            if (selectedId === product.Id)
                products += 'type="text" value="' + product.Id + '" disabled >' + product.Name + '</div>';
        });

        return products;
    }

    function getImageOptions(selectedId) {
        var images = "";
        $.each(cachedObj.products, function (i, product) {
            if (selectedId === product.Id)
                images += '<img style="width: 60px; height: 30px" src="' + product.Image + '" />';
        });
        return images;
    }

    function getColorOptions(selectedId, status) {
        var colors = "";
        if (status === 0) {
            colors = "<select style='width: 150px'  class='form-control ddlColorId'>";
        }
        else {
            colors = "<select style='width: 150px'  class='form-control ddlColorId' disabled>";
        }

        $.each(cachedObj.colors, function (i, color) {
            if (selectedId === color.Id)
                colors += '<option style="width: 100px;" value="' + color.Id + '" selected="select">' + color.Name + '</option>';
            else
                colors += '<option style="width: 100px;" value="' + color.Id + '">' + color.Name + '</option>';
        });
        colors += "</select>";
        return colors;
    }

    function getSizeOptions(selectedId, status) {
        var sizes = "";
        if (status === 0) {
            sizes = "<select style='width: 180px' class='form-control ddlSizeId'>";
        }
        else {
            sizes = "<select style='width: 180px' class='form-control ddlSizeId' disabled>";
        }

        $.each(cachedObj.sizes, function (i, size) {
            if (selectedId === size.Id)
                sizes += '<option  value="' + size.Id + '" selected="select">' + size.Name + '</option>';
            else
                sizes += '<option  value="' + size.Id + '">' + size.Name + '</option>';
        });
        sizes += "</select>";
        return sizes;
    }

    function resetFormMaintainance() {
        $('#hidId').val(0);
        $('#txtCustomerName').val('');

        $('#txtCustomerAddress').val('');
        $('#txtCustomerMobile').val('');
        $('#txtCustomerMessage').val('');
        $('#ddlPaymentMethod').val('');
        $('#ddlCustomerId').val('');
        $('#ddlBillStatus').val('');
        $('#tbl-bill-details').html('');
    }

    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/BillDetails/GetAllPaging",
            data: {
                startDate: $('#txtFromDate').val(),
                endDate: $('#txtToDate').val(),
                keyword: $('#txtSearchKeyword').val(),
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
                        //var d = new Date(item.DateCreated);
                        render += Mustache.render(template, {
                            CustomerName: item.CustomerName,
                            CustomerAddress: item.CustomerAddress,
                            CustomerPhoneNumber: item.CustomerMobile,
                            CustomerMessage: item.CustomerMessage,
                            Id: item.Id,
                            PaymentMethod: getPaymentMethodName(item.PaymentMethod),
                            DateCreated: tedu.dateTimeFormatJson(item.DateCreated),
                            BillStatus: getBillStatusName(item.BillStatus)
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
                    $("#lbl-total-records").text('0');
                    $('#tbl-content').html('');
                }
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    }

    function getPaymentMethodName(paymentMethod) {
        var method = $.grep(cachedObj.paymentMethods, function (element, index) {
            return element.Value === paymentMethod;
        });
        if (method.length > 0)
            return method[0].Name;
        else return '';
    }

    function getBillStatusName(status) {
        status = $.grep(cachedObj.billStatuses, function (element, index) {
            return element.Value === status;
        });
        if (status.length > 0)
            return status[0].Name;
        else return '';
    }
    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                tedu.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
};