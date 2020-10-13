
$(".ordersSelect2").select2({
    placeholder: "Введите слово для поиска",
    theme: "bootstrap4",
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
                        text: item.name + ' | ' + item.company + ' | $ ' + item.price
                    };
                }),
            };
        }
    }
});

$(".defaultSelect2").select2({
    placeholder: "Выберите",
    theme: "bootstrap4",
    allowClear: true
});
