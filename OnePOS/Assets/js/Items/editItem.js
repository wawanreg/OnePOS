
var itemsData = {
    
    itemStorage: {
        value: ""
    },
    itemBrandType: {
        value: ""
    },
    itemVendor: {
        value: ""
    },
    itemBrandCategory: {
        value: ""
    }
};


//Add Branch Location
function functionEditFormItem() {
   
    var thisSalePrice = $('.items-form-container .item-sale-price input');
    thisSalePrice.val(numbro(thisSalePrice.val()).format('0,0'));

    var thisBuyPrice = $('.items-form-container .item-buy-price input');
    thisBuyPrice.val(numbro(thisBuyPrice.val()).format('0,0'));

    var thisQuantity = $('.items-form-container .item-stock input');
    thisQuantity.val(numbro(thisQuantity.val()).format('0,0'));
}