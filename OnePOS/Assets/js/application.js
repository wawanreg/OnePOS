var getNumbro;

function changeInputNumber(eInput) {
    getNumbro = $(eInput).val();
    $(eInput).val(numbro(getNumbro).format('0,0'));
}

function unNumbro(number) {
    var realNumber = 0;
    return realNumber = numbro().unformat(number);
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