var ShipController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
    };

    function registerEvents() {
        $('#txtFromDate, #txtToDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        $(function () {
            $('#dateDeliveryTime').datetimepicker();
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
        $("#ddl-show-page").on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });

        $("#btn-create").on('click', function () {
            resetFormMaintainance();
            $('#modal-detail').modal('show');
        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            $.ajax({
                type: "GET",
                url: "/Admin/ShipCodes/GetById",
                data: { id: that },
                beforeSend: function () {
                    tedu.startLoading();
                },
                success: function (response) {
                    var data = response;
                    $('#hidId').val(data.Id);
                    $('#dateDeliveryTime').val(data.DeliveryTime);
                    $('#txtCarriers').val(data.Carriers);
                    $('#txtZipCode').val(data.ZipCode);
                    $('#txtTotal').val(data.Total);
                    $('#txtCollectionFee').val(data.CollectionFee);
                    $('#selectIdAddress').val(data.IdAddress);

                    $('#modal-detail').modal('show');
                    tedu.stopLoading();
                },
                error: function (e) {
                    tedu.notify('Has an error in progress', 'error');
                    tedu.stopLoading();
                }
            });
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            tedu.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/ShipCodes/Delete",
                    data: { id: that },
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (response) {
                        tedu.notify('Delete successful', 'success');
                        tedu.stopLoading();
                        loadData();
                    },
                    error: function (status) {
                        tedu.notify('Has an error in deleting progress', 'error');
                        tedu.stopLoading();
                    }
                });
            });
        });

        $('#btnSave').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidId').val();
                var carriers = $('#txtCarriers').val();
                var deliveryTime = $('#dateDeliveryTime').val();
                var collectionFee = $('#txtCollectionFee').val();
                var zipCode = $('#txtZipCode').val();
                var total = $('#txtTotal').val();
                var idAddress = $('#selectIdAddress').val();
                $.ajax({
                    type: "POST",
                    url: "/Admin/ShipCodes/SaveEntity",
                    data: {
                        Id: id,
                        Carriers: carriers,
                        DeliveryTime: deliveryTime,
                        CollectionFee: collectionFee,
                        ZipCode: zipCode,
                        Total: total,
                        IdAddress: idAddress
                    },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (response) {
                        tedu.notify('Save order successful', 'success');
                        $('#modal-detail').modal('hide');
                        resetFormMaintainance();

                        tedu.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        tedu.notify('Has an error in progress', 'error');
                        tedu.stopLoading();
                    }
                });
                return false;
            }
        });

        //$('body').on('click', '.btn-not-permission', function () {
        //    tedu.notifypermission('You not has pemission is this action', 'warning');
        //});
    }
    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/Admin/ShipCodes/GetAllPaging",
            data: {
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
                        render += Mustache.render(template, {
                            Id: item.Id,
                            Carriers: item.Carriers,
                            DeliveryTime: item.DeliveryTime,
                            CollectionFee: item.CollectionFee,
                            ZipCode: item.ZipCode,
                            //Id: item.Id,
                            IdAddress: item.IdAddress,
                            Total: item.Total
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
    function resetFormMaintainance() {
        $('#hidId').val(0);
        $('#dateDeliveryTime').val('');
        $('#txtCarriers').val('');
        $('#txtZipCode').val('');
        $('#txtTotal').val('');
        $('#txtCollectionFee').val('');
        $('#selectIdAddress').val('');
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