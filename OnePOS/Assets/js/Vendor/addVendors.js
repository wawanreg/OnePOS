
var vendorData = {
    vendorName: {
        value: ""
    },
    vendorAddress: {
        value: ""
    },
    vendorPhone: {
        value: ""
    },
    vendorEmail: {
        value: ""
    },
    vendorOwner: {
        value: ""
    }
};


//Add Branch Location
function functionAddFormVendor() {
    
    var termTemplate = '<div class="panel panel-default vendors-form-container">' + $('.vendors-group-list .panel-default').html() + '</div>';

    // add form item
    $('.add-vendor-btn').click(function (evt) {
        evt.preventDefault();
        $('.vendors-group-list').append(termTemplate);

    });

    // remove form item
    $('.vendors-group-list').on('click', '.close-vendor-button', function (evt) {
        evt.preventDefault();
        
        if ($('.vendors-group-list .vendors-form-container').siblings().length > 1) {
            $(this).closest('.vendors-form-container').remove();
        }

    });

    //submit data to db
    $('.add-vendor-input-btn input').click(function (evt) {
        evt.preventDefault();

        
        vendorData.vendorName.value = [];
        $('.vendors-form-container .vendor-name input').each(function () {
            if ($(this).val().length > 0) {
                vendorData.vendorName.value.push($(this).val());
            }
        });

        vendorData.vendorAddress.value = [];
        $('.vendors-form-container .vendor-address textarea').each(function () {
            if ($(this).val().length > 0) {
                vendorData.vendorAddress.value.push($(this).val());
            }
        });

        vendorData.vendorPhone.value = [];
        $('.vendors-form-container .vendor-phone input').each(function () {
            if ($(this).val().length > 0) {
                vendorData.vendorPhone.value.push($(this).val());
            }
        });

        vendorData.vendorEmail.value = [];
        $('.vendors-form-container .vendor-email input').each(function () {
            if ($(this).val().length > 0) {
                vendorData.vendorEmail.value.push($(this).val());
            } else {
                vendorData.vendorEmail.value.push(" ");
            }
        });

        vendorData.vendorOwner.value = [];
        $('.vendors-form-container .vendor-owner input').each(function () {
            if ($(this).val().length > 0) {
                vendorData.vendorOwner.value.push($(this).val());
            } else {
                vendorData.vendorOwner.value.push(" ");
            }
        });

        document.getElementById("VendorName").value = vendorData.vendorName.value.join('|');
        document.getElementById("VendorAddress").value = vendorData.vendorAddress.value.join('|');
        document.getElementById("VendorPhone").value = vendorData.vendorPhone.value.join('|');
        document.getElementById("VendorEmail").value = vendorData.vendorEmail.value.join('|');
        document.getElementById("VendorOwner").value = vendorData.vendorOwner.value.join('|');
        
        $('#AddVendorsPost').submit();
        

        
    });
}