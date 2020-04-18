var productCategoryController = function () {
    this.initialize = function () {
        loadData(10);
        registerEvents();
    }
    function registerEvents() {
        $('#select-view-filter').change(function () {
            var data = $("#select-view-filter option:selected").val();
            loadData(data);
        });
    }

    function loadData(filter) {
        $.ajax({
            url: '/Product/GetAllFilter',
            data: {
                filter: parseInt(filter)
            },
            dataType: 'json',
            success: function (response) {
                var template = $('#list-template-A').html();
                var render = "";
                $.each(response, function (i, item) {
                    render += Mustache.render(template, {
                        IdA: item.Id,
                        ImagesA: item.Image,
                        urlA: "/" + item.SeoAlias + "-c." + item.Id + ".html",
                        NameA: item.Name,
                        DescriptionA: item.Description
                    });
                });
                $('#list-products-mustache-A').html(render);

                var templateB = $('#list-template-B').html();
                var renderB = "";
                $.each(response, function (i, item) {
                    renderB += Mustache.render(templateB, {
                        IdB: item.Id,
                        ImagesB: item.Image,
                        urlB: "/" + item.SeoAlias + "-c." + item.Id + ".html",
                        NameB: item.Name,
                        DescriptionB: item.Description
                    });
                });
                $('#list-products-mustache-B').html(renderB);

                tedu.notify("Load Success", "success");
            },
            error: function () {
                tedu.notify("Can not loading", "error");
            }
        });
    }
}