$(document).ready(function () {
    $.get("../../cidades/get", function (data) {
        var cities = $('#CidadeId');
        cities.empty(); // remove any existing options
        $.each(data, function (index, item) {
            cities.append($('<option></option>').text(item.Nome).val(item.Codigo));
        });
        alert("Load was performed.");
    });
});
