
var items = $('.js-user-role-items');

$(items).on('click', function () {
    var checkBox = $(this).find("input.form-check-input");

    if (checkBox.length > 0) {
        if ($(checkBox).is(":checked")) {
            $(checkBox).prop("checked", false);
        } else {
            $(checkBox).prop("checked", true);
        }
    }
});