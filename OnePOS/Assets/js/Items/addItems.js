
var itemsData = {
    itemName: {
        value: "",
        status: "",
        flag: false
    },
    itemSalePrice: {
        value: "",
        status: "",
        flag: false
    },
    itemBuyPrice: {
        value: "",
        status: "",
        flag: false
    },
    itemStorage: {
        value: "",
        status: "",
        flag: false
    },
    itemBrandType: {
        value: ""
    },
    itemStock: {
        value: "",
        status: "",
        flag: false
    },
    itemVendor: {
        value: "",
        status: "",
        flag: false
    },
    itemBrandCategory: {
        value: "",
        status: "",
        flag: false
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
        itemsData.itemName.status = [];
        $('.items-form-container .item-name input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemName.value.push($(this).val());
                $(this).siblings(".field-warning").hide();

                itemsData.itemName.status.push(true);
                itemsData.itemName.flag = true;
            } else {
                $(this).siblings(".field-warning").show();

                itemsData.itemName.status.push(false);
                itemsData.itemName.flag = false;
            }
        });

        itemsData.itemSalePrice.value = [];
        itemsData.itemSalePrice.status = [];
        $('.items-form-container .item-sale-price input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemSalePrice.value.push(unNumbro($(this).val()));
                $(this).siblings(".field-warning").hide();

                itemsData.itemSalePrice.status.push(true);
                itemsData.itemSalePrice.flag = true;
            } else {
                $(this).siblings(".field-warning").show();

                itemsData.itemSalePrice.status.push(false);
                itemsData.itemSalePrice.flag = false;
            }
        });

        itemsData.itemBuyPrice.value = [];
        itemsData.itemBuyPrice.status = [];
        $('.items-form-container .item-buy-price input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemBuyPrice.value.push(unNumbro($(this).val()));
                $(this).siblings(".field-warning").hide();

                itemsData.itemBuyPrice.status.push(true);
                itemsData.itemBuyPrice.flag = true;
            } else {
                $(this).siblings(".field-warning").show();

                itemsData.itemBuyPrice.status.push(false);
                itemsData.itemBuyPrice.flag = false;
            }
        });

        itemsData.itemStock.value = [];
        itemsData.itemStock.status = [];
        $('.items-form-container .item-stock input').each(function () {
            if ($(this).val().length > 0) {
                itemsData.itemStock.value.push(unNumbro($(this).val()));
                $(this).siblings(".field-warning").hide();

                itemsData.itemStock.status.push(true);
                itemsData.itemStock.flag = true;
            } else {
                $(this).siblings(".field-warning").show();

                itemsData.itemStock.status.push(false);
                itemsData.itemStock.flag = false;
            }
        });

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

        //Warning null checker
        var maxLength = $('.items-form-container .item-name input').length;
        for (var i = maxLength - 1; i >= 0; i--) {
            if (itemsData.itemName.status[i] && itemsData.itemName.flag) {
                itemsData.itemName.flag = true;
            } else {
                itemsData.itemName.flag = false;
            }

            if (itemsData.itemSalePrice.status[i] && itemsData.itemSalePrice.flag) {
                itemsData.itemSalePrice.flag = true;
            } else {
                itemsData.itemSalePrice.flag = false;
            }

            if (itemsData.itemBuyPrice.status[i] && itemsData.itemBuyPrice.flag) {
                itemsData.itemBuyPrice.flag = true;
            } else {
                itemsData.itemBuyPrice.flag = false;
            }

            if (itemsData.itemStock.status[i] && itemsData.itemStock.flag) {
                itemsData.itemStock.flag = true;
            } else {
                itemsData.itemStock.flag = false;
            }
        }

        if (itemsData.itemName.flag && itemsData.itemSalePrice.flag && itemsData.itemBuyPrice.flag && itemsData.itemStock.flag) {
            document.getElementById("ItemName").value = itemsData.itemName.value.join('|');
            document.getElementById("ItemSalePrice").value = itemsData.itemSalePrice.value.join('|');
            document.getElementById("ItemBuyPrice").value = itemsData.itemBuyPrice.value.join('|');
            document.getElementById("ItemStorage").value = itemsData.itemStorage.value.join('|');
            document.getElementById("ItemBrandType").value = itemsData.itemBrandType.value.join('|');
            document.getElementById("ItemQuantity").value = itemsData.itemStock.value.join('|');
            document.getElementById("ItemVendor").value = itemsData.itemVendor.value.join('|');
            document.getElementById("ItemBrandCategory").value = itemsData.itemBrandCategory.value.join('|');

            $('#AddItemsPost').submit();    
        }
    });
}