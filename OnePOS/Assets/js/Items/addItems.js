
var itemsData = {
    itemName: {
        value: ""
    },
    itemSalePrice: {
        value: ""
    },
    itemBuyPrice: {
        value: ""
    },
    itemLocation: {
        value: ""
    },
    itemBrandType: {
        value: ""
    },
    itemQuantity: {
        value: ""
    },
    itemVendor: {
        value: ""
    }
};


//Add Branch Location
function functionAddFormItem() {
    
    var termTemplate = '<div class="panel panel-default items-form-container">' + $('.items-group-list .panel-default').html() + '</div>';

    // add form item
    $('.add-item-btn').click(function (evt) {
        evt.preventDefault();
        $('.items-group-list').append(termTemplate);

    });

    // remove form item
    $('.items-group-list').on('click', '.close-item-button', function (evt) {
        evt.preventDefault();
        
        if ($('.items-group-list .items-form-container').siblings().length > 1) {
            $(this).closest('.items-form-container').remove();
        }

    });

    //submit data to db
    $('.add-item-input-btn input').click(function (evt) {
        evt.preventDefault();

        
        itemsData.itemName.value = [];
        $('.items-form-container .item-name input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemName.value.push($(this).val());
            }
        });

        itemsData.itemSalePrice.value = [];
        $('.items-form-container .item-sale-price input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemSalePrice.value.push(unNumbro($(this).val()));
            }
        });

        itemsData.itemBuyPrice.value = [];
        $('.items-form-container .item-buy-price input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemBuyPrice.value.push(unNumbro($(this).val()));
            }
        });

        itemsData.itemLocation.value = [];
        $('.items-form-container .item-location input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemLocation.value.push($(this).val());
            } else {
                itemsData.itemLocation.value.push(" ");
            }
        });

        itemsData.itemBrandType.value = [];
        $('.items-form-container .item-brand-type input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemBrandType.value.push($(this).val());
            } else {
                itemsData.itemBrandType.value.push(" ");
            }
        });

        itemsData.itemQuantity.value = [];
        $('.items-form-container .item-quantity input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemQuantity.value.push(unNumbro($(this).val()));
            } else {
                itemsData.itemQuantity.value.push(" ");
            }
        });

        itemsData.itemVendor.value = [];
        $('.items-form-container .item-vendor input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemVendor.value.push($(this).val());
            } else {
                itemsData.itemVendor.value.push(" ");
            }
        });

        document.getElementById("ItemName").value = itemsData.itemName.value.join('|');
        document.getElementById("ItemSalePrice").value = itemsData.itemSalePrice.value.join('|');
        document.getElementById("ItemBuyPrice").value = itemsData.itemBuyPrice.value.join('|');
        document.getElementById("ItemLocation").value = itemsData.itemLocation.value.join('|');
        document.getElementById("ItemBrandType").value = itemsData.itemBrandType.value.join('|');
        document.getElementById("ItemQuantity").value = itemsData.itemQuantity.value.join('|');
        document.getElementById("ItemVendor").value = itemsData.itemVendor.value.join('|');

        $('#AddItemsPost').submit();
        

        
    });
}