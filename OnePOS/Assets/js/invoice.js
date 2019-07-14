

function checkQuantity(elmt) {
    var quantity = unNumbro($(elmt).val());
    
    if (quantity == 0)
        $(elmt).val(quantity);

    var vMaxStrg = unNumbro($(elmt).siblings(".max-storage").val());
    var vOrStock = unNumbro($(elmt).siblings(".original-stock").val());
    //console.log("vMaxStrg=" + vMaxStrg + " vOrStock=" + vOrStock);

    var discount = unNumbro($(elmt).parents().next(".item-discount").children("input").val());
    var price = unNumbro($(elmt).parents().prev(".item-price").children(".sale-price").val());
    var target = $(elmt).parents().next().next(".item-total-price").children(".price-item-discount");

    //console.log("Quantity=" + quantity + " discount=" + discount + " price=" + price);

    if (quantity <= vOrStock) {
        $(elmt).siblings(".field-warning").hide();

        target.val(calcuatePricePerLine(discount, quantity, price));
        
        recalculateTotalTransaction();
    } else if (quantity > vMaxStrg) {
        $(elmt).siblings(".field-warning").show();

    } else {

        $(elmt).siblings(".field-warning").hide();

        target.val(calcuatePricePerLine(discount, quantity, price));
        
        recalculateTotalTransaction();
    }
}

function checkDiscount(elmt) {
    var discount = unNumbro($(elmt).val());
    if (discount == 0)
        $(elmt).val(discount);

    var price = unNumbro($(elmt).parents().prev().prev(".item-price").children(".sale-price").val());
    var quantity = unNumbro($(elmt).parents().prev(".item-quantity").children(".quantity-value").val());
    var target = $(elmt).parents().next(".item-total-price").children(".price-item-discount");
    //var hiddenTarget = $(elmt).parents().next(".item-total-price").children(".hidden-price-item-discount");

    target.val(calcuatePricePerLine(discount, quantity, price));

    recalculateTotalTransaction();
}

function getTotalItem()
{
    var tempNum = 0;
    $(".price-item-discount").each(function () {
        tempNum += unNumbro($(this).val());
    });

    return tempNum;
}

function checkInvoiceDiscount(discount) {
    var invoiceDisc = $(discount).val();
    if (invoiceDisc == 0)
        $(discount).val(0);

    var invoiceTrans = getTotalItem();
    var reduceValue = unNumbro($(".adjust-invoice input").val());

    $(".invoice-total-trans input").val(calculateInvoice(invoiceDisc, invoiceTrans, reduceValue));


}

function adjustInvoiceValue(reduce) {
    var reduceValue = $(reduce).val();
    if (reduceValue == 0)
        $(reduce).val(0);

    var invoiceTrans = getTotalItem();
    var invoiceDisc = unNumbro($(".invoice-discount input").val());

    $(".invoice-total-trans input").val(calculateInvoice(invoiceDisc, invoiceTrans, reduceValue));
}

function calcuatePricePerLine(discount, quantity, price) {
    //console.log("Test Quantity=" + quantity + " discount=" + parseInt(discount) + " price=" + String(price));
    var finalPrice = 0;

    var tmpPrice = price * quantity;
    //console.log("tmpPrice=" + tmpPrice);

    var dscPrice = (tmpPrice * discount) / 100;
    //console.log("dscPrice=" + dscPrice);

    finalPrice = tmpPrice - dscPrice;
    //console.log("finalPrice=" + finalPrice);
    
    return setNumbro(finalPrice);
}

function recalculateTotalTransaction() {
    var tempNum = getTotalItem();

    var discount = unNumbro($(".invoice-discount input").val());
    var reduceValue = unNumbro($(".adjust-invoice input").val());

    $(".invoice-total-trans input").val(calculateInvoice(discount, tempNum, reduceValue));
}

function calculateInvoice(discount, invoiceValue, reduceValue) {
    var discountValue = (invoiceValue * discount) / 100;
    var finalValue = invoiceValue - discountValue - reduceValue;
    //console.log("discountValue=" + discountValue + " invoiceValue=" + invoiceValue + " reduceValue=" + reduceValue);

    return setNumbro(finalValue);
}


$(".edit-invoice-btn").click(function (evt) {
    evt.preventDefault();
    console.log("trigger");

    //console.log(finalTotalPayment.val());
    //$(".sale-price").each(function(e) {
    //    var finalCrtValue = $(this);
    //    finalCrtValue.val(unNumbro(finalCrtValue.val()));
    //});

    //$(".price-item-discount").each(function (e) {
    //    var finalCrtValue = $(this);
    //    finalCrtValue.val(unNumbro(finalCrtValue.val()));
    //});


    ////remove decimal before return the value to controller
    //var finalTotalPayment = $(".invoice-total-trans input");
    //finalTotalPayment.val(unNumbro(finalTotalPayment.val()));

    $('#EditInvoicePost').submit();
});





