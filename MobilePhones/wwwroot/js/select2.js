
$(".ordersSelect2").select2({
    placeholder: "Введите слово для поиска",
    language: "ru",
    //theme: "bootstrap4",
    allowClear: true,
    ajax: {
        url: "/Admin/Phones/Search",
        contentType: "application/json; charset=utf-8",
        data: function (params) {

            var query =
            {
                term: params.term,
            };
            return query;
        },
        processResults: function (result) {
            console.log(result)
            return {
                results: $.map(result, function (item) {
                    return {
                        id: item.id,
                        text: item.name + ' | ' + item.company + ' | $ ' + item.price,
                        file_name: item.imageName
                    };
                }),
            };
        }
    },
    templateResult: formatState
});

$(".defaultSelect2").select2({
    placeholder: "Выберите",
    theme: "bootstrap4",
    allowClear: true
});

function formatState(state) {
    if (!state.id) {
        return state.text;
    }
    var baseUrl = "/images/phone_images/" + state.file_name;
    var $state = $(
        '<span><img src="' + baseUrl + '" class="img-flag" width="30"/> ' + state.text + '</span>'
    );
    return $state;
};
