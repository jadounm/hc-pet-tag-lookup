$.validator.unobtrusive.adapters.addSingleVal("requireone", "otherpropertynames");
$.validator.addMethod("requireone", function (value, element, params) {
    var param = params.toString().split(',');
    var isAllNull = true;
    $.each(param, function (i, val) {
        var valueOfItem = $('#' + val).val().trim();
        if (valueOfItem != '') {
            isAllNull = false;
            return false;
        }
    });
    if (isAllNull) {
        return false;
    }
    else {
        return true;
    }
});