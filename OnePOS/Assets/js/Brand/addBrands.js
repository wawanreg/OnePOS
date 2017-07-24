
var brandData = {
    brandName: {
        value: ""
    },
    brandDescription: {
        value: ""
    },
    brandCateogry: {
        value: ""
    }
};


//Add Branch Location
function functionAddFormBrand() {
    
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
        $('.brands-form-container .brand-name input').each(function () {
            if ($(this).val().length > 0) {
                brandData.brandName.value.push($(this).val());
            }
        });

        brandData.brandDescription.value = [];
        $('.brands-form-container .brand-description textarea').each(function () {
            if ($(this).val().length > 0) {
                brandData.brandDescription.value.push($(this).val());
            }
        });

        brandData.brandCateogry.value = [];
        $('.brands-form-container .brand-category select').each(function () {
            if ($(this).val().length > 0) {
                brandData.brandCateogry.value.push($(this).val());
            }
        });

       

        document.getElementById("BrandName").value = brandData.brandName.value.join('|');
        document.getElementById("BrandDescription").value = brandData.brandDescription.value.join('|');
        document.getElementById("BrandCategoryId").value = brandData.brandCateogry.value.join('|');
        
        
        $('#AddBrandsPost').submit();
        

        
    });
}