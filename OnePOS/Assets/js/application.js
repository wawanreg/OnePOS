var getNumbro;

function changeInputNumber(eInput) {
    
    getNumbro = $(eInput).val();
    
    if (getNumbro == "") {
        getNumbro = 0;
    }
    $(eInput).val(numbro(getNumbro).format('0,0'));
    
}

function setNumbro(number) {
    if (number == "") {
        number = 0;
    }
    
    return numbro(number).format('0,0');
}

function unNumbro(number) {
    if (number == "")
        number = 0;

    return numbro().unformat(number);
}


function checkWarning(m) {
    
    if ($(m).val().length > 0) {
        $(m).siblings(".field-warning").hide();
    } else {
        $(m).siblings(".field-warning").show();
    }
}


function checkerEmailValid(eInput) {
    var pattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    return pattern.test($(eInput).val());

}

function downloadExcel(target) {
    window.location.assign("/Dashboard/Excel/" + target);
    return false;
}
