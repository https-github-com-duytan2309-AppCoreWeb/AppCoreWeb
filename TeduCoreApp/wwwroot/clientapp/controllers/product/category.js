var productCategoryController = function () {
    this.initialize = function () {
        loadData();
        registerEvents();
    }

    function registerEvents() {
        $("#select-view-filter").change(function () {
            var filter = $("select#select-view-filter option:checked").val();
            var data = $("select#select-view-name option:checked").val();
            loadData(filter, data);
        });

        $("#select-view-name").change(function () {
            var filter = $("select#select-view-filter option:checked").val();
            var data = $("select#select-view-name option:checked").val();
            if (data === "Name") {
                loadData(filter, data);
            }
        });
    }

    function loadData(/*isPageChanged,*/ filter, data) {
        $.ajax({
            type: 'GET',
            data: {
                filter: parseInt(filter),
                data: data
            },
            url: '/Product/GetAllPagingFilterOrName',
            dataType: 'json',
            success: function (response) {
                var templateA = $('#list-template-A').html();
                var renderA = "";
                var templateB = $('#list-template-B').html();
                var renderB = "";
                $.each(response, function (i, item) {
                    renderA += Mustache.render(templateA, {
                        IdA: item.Id,
                        NameA: item.Name,
                        ImagesA: item.Image,
                        SeoAliasA: item.SeoAlias,
                        DescriptionA: item.Description,
                        urlA: "/" + item.SeoAlias + "-c." + item.Id + ".html"
                    });
                });
                $.each(response, function (i, item) {
                    renderB += Mustache.render(templateB, {
                        IdB: item.Id,
                        NameB: item.Name,
                        ImagesB: item.Image,
                        SeoAliasB: item.SeoAlias,
                        DescriptionB: item.Description,
                        urlB: "/" + item.SeoAlias + "-c." + item.Id + ".html"
                    });
                });
                $('#list-products-mustache-A').html(renderA);
                $('#list-products-mustache-B').html(renderB);
                //wrapPaging(response.RowCount, function () {
                //    loadData();
                //}, isPageChanged);
            },
            error: function (status) {
                console.log(status);
                tedu.notify('Cannot loading data', 'error');
            }
        });
    }

    //function wrapPaging(recordCount, callBack, changePageSize) {
    //    var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
    //    //Unbind pagination if it existed or click change pagesize
    //    if ($('#paginationUL a').length === 0 || changePageSize === true) {
    //        $('#paginationUL').empty();
    //        $('#paginationUL').removeData("twbs-pagination");
    //        $('#paginationUL').unbind("page");
    //    }
    //    //Bind Pagination Event
    //    $('#paginationUL').twbsPagination({
    //        totalPages: totalsize,
    //        visiblePages: 7,
    //        first: 'Đầu',
    //        prev: 'Trước',
    //        next: 'Tiếp',
    //        last: 'Cuối',
    //        onPageClick: function (event, p) {
    //            tedu.configs.pageIndex = p;
    //            setTimeout(callBack(), 200);
    //        }
    //    });
    //}
}