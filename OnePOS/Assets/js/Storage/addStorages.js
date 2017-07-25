
var storageData = {
    storageName: {
        value: ""
    },
    storageDescription: {
        value: ""
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
        $('.storages-form-container .storage-name input').each(function () {
            if ($(this).val().length > 0) {
                storageData.storageName.value.push($(this).val());
            }
        });

        storageData.storageDescription.value = [];
        $('.storages-form-container .storage-description textarea').each(function () {
            if ($(this).val().length > 0) {
                storageData.storageDescription.value.push($(this).val());
            }
        });

       
        document.getElementById("StorageName").value = storageData.storageName.value.join('|');
        document.getElementById("StorageDescription").value = storageData.storageDescription.value.join('|');
        
        
        $('#AddStoragesPost').submit();
        

        
    });
}