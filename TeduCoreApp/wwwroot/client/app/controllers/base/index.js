var BaseController = function () {
    this.initialize = function () {
        registerEvents();
    }

    function registerEvents() {
        $('body').on('click', '.add-to-cart', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/AddToCart',
                type: 'post',
                data: {
                    productId: id,
                    quantity: 1,
                    color: 0,
                    size: 0
                },
                success: function (response) {
                    tedu.notify(resources["AddCartOK"], 'success');
                    loadHeaderCart();
                    location.reload();
                }
            });
        });

        $('body').on('click', '.remove-cart', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            $.ajax({
                url: '/Cart/RemoveFromCart',
                type: 'post',
                data: {
                    productId: id
                },
                success: function (response) {
                    tedu.notify(resources["RemoveCartOK"], 'success');
                    loadHeaderCart();

                    //location.reload();
                }
            });
        });

        //$('body').on('click', '#remove-cart-all', function (e) {
        //    e.preventDefault();
        //    var id = $(this).data('id');
        //    $.ajax({
        //        url: '/Cart/ClearCart',
        //        type: 'get',
        //        data: {
        //            productId: id
        //        },
        //        success: function (response) {
        //            tedu.notify(resources["Clear All"], 'success');
        //            ClearCart();
        //            location.reload();
        //        }
        //    });
        //});
    }

    function loadHeaderCart() {
        $("#headerCart").load("/AjaxContent/HeaderCart");
    }

    //function ClearCart() {
    //    $.ajax({
    //        url: '/Cart/ClearCart',
    //        type: 'get',
    //        data: {
    //        },
    //        success: function (response) {
    //            //location.reload();
    //        }
    //    });
    //}

    //function AddToCart() {
    //    $('body').on('click', '.add-to-cart', function (e) {
    //        e.preventDefault();
    //        var id = $(this).data('id');
    //        $.ajax({
    //            url: '/Cart/AddToCart',
    //            type: 'post',
    //            data: {
    //                productId: id,
    //                quantity: 1,
    //                color: 0,
    //                size: 0
    //            },
    //            success: function (response) {
    //                tedu.notify(resources["AddCartOK"], 'success');
    //                loadHeaderCart();
    //                location.reload();
    //            }
    //        });
    //    });
    //}
}