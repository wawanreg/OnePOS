var getNumbro;

function changeInputNumber(eInput) {
    getNumbro = $(eInput).val();
    $(eInput).val(numbro(getNumbro).format('0,0'));
}

function unNumbro(number) {
    var realNumber = 0;
    return realNumber = numbro().unformat(number);
}

function checkerEmailValid(eInput) {
    var pattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    return pattern.test($(eInput).val());

}