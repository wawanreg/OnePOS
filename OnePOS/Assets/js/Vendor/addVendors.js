
var vendorData = {
    vendorName: {
        value: "",
        status:"",
        flag: false
    },
    vendorAddress: {
        value: "",
        status: "",
        flag: false
    },
    vendorPhone: {
        value: "",
        status: "",
        flag: false
    },
    vendorEmail: {
        value: "",
        status: "",
        flag: false
    },
    vendorOwner: {
        value: "",
        status: "",
        flag: false
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
        vendorData.vendorName.status = [];
        $('.vendors-form-container .vendor-name input').each(function () {

            if ($(this).val().length > 0) {
                vendorData.vendorName.value.push($(this).val());
                $(this).siblings(".field-warning").hide();

                vendorData.vendorName.status.push(true);
                vendorData.vendorName.flag = true;
            } else {
                $(this).siblings(".field-warning").show();

                vendorData.vendorName.status.push(false);
                vendorData.vendorName.flag = false;
            }
        });

        vendorData.vendorAddress.value = [];
        vendorData.vendorAddress.status = [];
        $('.vendors-form-container .vendor-address textarea').each(function () {
            if ($(this).val().length > 0) {
                vendorData.vendorAddress.value.push($(this).val());
                $(this).siblings(".field-warning").hide();
                vendorData.vendorAddress.status.push(true);
                vendorData.vendorAddress.flag = true;
            } else {
                $(this).siblings(".field-warning").show();
                vendorData.vendorAddress.status.push(false);
                vendorData.vendorAddress.flag = false;
            }
        });

        vendorData.vendorPhone.value = [];
        vendorData.vendorPhone.status = [];
        $('.vendors-form-container .vendor-phone input').each(function () {
            if ($(this).val().length > 0) {
                vendorData.vendorPhone.value.push($(this).val());
                $(this).siblings(".field-warning").hide();
                vendorData.vendorPhone.status.push(true);
                vendorData.vendorPhone.flag =true;
            } else {
                $(this).siblings(".field-warning").show();
                vendorData.vendorPhone.status.push(false);
                vendorData.vendorPhone.flag = false;
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

        var maxLength = $('.vendors-form-container .vendor-owner input').length;
        for (var i = maxLength - 1; i >= 0; i--) {
            if (vendorData.vendorName.status[i] && vendorData.vendorName.flag) {
                vendorData.vendorName.flag = true;
            } else {
                vendorData.vendorName.flag = false;
            }

            if (vendorData.vendorPhone.status[i] && vendorData.vendorPhone.flag) {
                vendorData.vendorPhone.flag = true;
            } else {
                vendorData.vendorPhone.flag = false;
            }

            if (vendorData.vendorAddress.status[i] && vendorData.vendorAddress.flag) {
                vendorData.vendorAddress.flag = true;
            } else {
                vendorData.vendorAddress.flag = false;
            }

        }

        
        if (vendorData.vendorName.flag && vendorData.vendorPhone.flag && vendorData.vendorAddress.flag) {
            document.getElementById("VendorName").value = vendorData.vendorName.value.join('|');
            document.getElementById("VendorAddress").value = vendorData.vendorAddress.value.join('|');
            document.getElementById("VendorPhone").value = vendorData.vendorPhone.value.join('|');
            document.getElementById("VendorEmail").value = vendorData.vendorEmail.value.join('|');
            document.getElementById("VendorOwner").value = vendorData.vendorOwner.value.join('|');

            $('#AddVendorsPost').submit();
        }
        

        
    });
}