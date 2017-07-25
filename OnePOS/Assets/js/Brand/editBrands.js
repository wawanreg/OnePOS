
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
function functionEditFormBrand() {
    
    //submit data to db
    $('.edit-brand-input-btn input').click(function (evt) {
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
        
        
        $('#EditBrandPost').submit();
        

        
    });
}