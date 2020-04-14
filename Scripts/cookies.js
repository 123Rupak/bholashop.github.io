$(".addtocart").click(function () {
    var productID = $(this).attr('data-id');
    $.cookie('CartProduct', productID);
    alert("Product added to Cart");
});