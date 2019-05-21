
var storageData = {
    storageName: {
        value: "",
        status: "",
        flag: false
    },
    storageDescription: {
        value: "",
        status: "",
        flag: false
    }
};


//Add Branch Location
function functionAddFormStorage() {
    
    var termTemplate = '<div class="panel panel-default storages-form-container">' + $('.storages-group-list .panel-default').html() + '</div>';

    // add form item
    $('.add-storage-btn').click(function (evt) {
        evt.preventDefault();
        $('.storages-group-list').append(termTemplate);

    });

    // remove form item
    $('.storages-group-list').on('click', '.close-storage-button', function (evt) {
        evt.preventDefault();
        
        if ($('.storages-group-list .storages-form-container').siblings().length > 1) {
            $(this).closest('.storages-form-container').remove();
        }

    });

    //submit data to db
    $('.add-storage-input-btn input').click(function (evt) {
        evt.preventDefault();

        
        storageData.storageName.value = [];
        storageData.storageName.status = [];
        $('.storages-form-container .storage-name input').each(function () {
            if ($(this).val().length > 0) {
                storageData.storageName.value.push($(this).val());
                $(this).siblings(".field-warning").hide();

                //if every brandNames is fill set the flag to true

                storageData.storageName.status.push(true);
                storageData.storageName.flag = true;
            } else {
                $(this).siblings(".field-warning").show();

                storageData.storageName.status.push(false);
                storageData.storageName.flag = false;
            }
        });

        storageData.storageDescription.value = [];
        $('.storages-form-container .storage-description textarea').each(function () {
            if ($(this).val().length > 0) {
                storageData.storageDescription.value.push($(this).val());
            } else {
                storageData.storageDescription.value.push(" ");
            }
        });
        
        var maxLength = $('.storages-form-container .storage-name input').length;
        for (var i = maxLength - 1; i >= 0; i--) {
            if (storageData.storageName.status[i] && storageData.storageName.flag) {
                storageData.storageName.flag = true;
            } else {
                storageData.storageName.flag = false;
            }
        }

        if (storageData.storageName.flag) {
            document.getElementById("StorageName").value = storageData.storageName.value.join('|');
            document.getElementById("StorageDescription").value = storageData.storageDescription.value.join('|');
        
            $('#AddStoragesPost').submit();            
        }
        
    });
}