
var brandData = {
    brandName: {
        value: "",
        status: "",
        flag: false
    },
    brandDescription: {
        value: "",
        status: "",
        flag: false
    }
    //,
    //brandCateogry: {
    //    value: ""
    //}
};


//Add Branch Location
function functionAddFormBrand() {
    var submitFlag = false;
    
    var termTemplate = '<div class="panel panel-default brands-form-container">' + $('.brands-group-list .panel-default').html() + '</div>';

    // add form item
    $('.add-brand-btn').click(function (evt) {
        evt.preventDefault();
        $('.brands-group-list').append(termTemplate);

    });

    // remove form item
    $('.brands-group-list').on('click', '.close-brand-button', function (evt) {
        evt.preventDefault();
        
        if ($('.brands-group-list .brands-form-container').siblings().length > 1) {
            $(this).closest('.brands-form-container').remove();
        }

    });

    //submit data to db
    $('.add-brand-input-btn input').click(function (evt) {
        evt.preventDefault();

        
        brandData.brandName.value = [];
        brandData.brandName.status = [];
        $('.brands-form-container .brand-name input').each(function () {
            if ($(this).val().length > 0) {
                brandData.brandName.value.push($(this).val());
                $(this).siblings(".field-warning").hide();

                //if every brandNames is fill set the flag to true

                brandData.brandName.status.push(true);
                brandData.brandName.flag = true;
            } else {
                $(this).siblings(".field-warning").show();
                
                brandData.brandName.status.push(false);
                brandData.brandName.flag = false;
            }

        });

        brandData.brandDescription.value = [];
        $('.brands-form-container .brand-description textarea').each(function () {
            
            if ($(this).val().length > 0) {
                
                brandData.brandDescription.value.push($(this).val());
            } else {
                
                brandData.brandDescription.value.push(" ");
            }
        });

        var maxLength = $('.brands-form-container .brand-name input').length;
        for (var i = maxLength - 1; i >= 0; i--) {
            if (brandData.brandName.status[i] && brandData.brandName.flag) {
                brandData.brandName.flag = true;
            } else {
                brandData.brandName.flag = false;
            }
        }
       
        
        if (brandData.brandName.flag) {
            document.getElementById("BrandName").value = brandData.brandName.value.join('|');
            document.getElementById("BrandDescription").value = brandData.brandDescription.value.join('|');

            $('#AddBrandsPost').submit();
        }
    });
}