
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

    var thisQuantity = $('.items-form-container .item-quantity input');
    thisQuantity.val(numbro(thisQuantity.val()).format('0,0'));


    //submit data to db
    $('.edit-item-input-btn input').click(function (evt) {
        evt.preventDefault();

        itemsData.itemStorage.value = [];
        $('.items-form-container .item-storage select').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemStorage.value.push($(this).val());
            } else {
                itemsData.itemStorage.value.push(" ");
            }
        });

        itemsData.itemBrandType.value = [];
        $('.items-form-container .item-brand-type select').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemBrandType.value.push($(this).val());
            } else {
                itemsData.itemBrandType.value.push(" ");
            }
        });

       

        itemsData.itemVendor.value = [];
        $('.items-form-container .item-vendor select').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemVendor.value.push($(this).val());
            } else {
                itemsData.itemVendor.value.push(" ");
            }
        });

        itemsData.itemBrandCategory.value = [];
        $('.items-form-container .item-brand-category select').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemBrandCategory.value.push($(this).val());
            } else {
                itemsData.itemBrandCategory.value.push(" ");
            }
        });


        document.getElementById("ItemStorage").value = itemsData.itemStorage.value.join('|');
        document.getElementById("ItemBrandType").value = itemsData.itemBrandType.value.join('|');
        document.getElementById("ItemVendor").value = itemsData.itemVendor.value.join('|');
        document.getElementById("ItemBrandCategory").value = itemsData.itemBrandCategory.value.join('|');

        $('#EditItemPost').submit();
        

        
    });
}