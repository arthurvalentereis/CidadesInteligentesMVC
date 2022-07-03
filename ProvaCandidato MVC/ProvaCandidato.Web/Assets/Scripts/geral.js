$(document).ready(function () {
    $(".alert-success").delay(1000).fadeOut(3000);

    $("#searchInput").keyup(function () {
        var rows = $("#fbody").find("tr").hide();
        if (this.value.length) {
            var data = this.value.split(" ");
            $.each(data, function (i, v) {
                rows.filter(":contains('" + v + "')").show();
            });
        } else rows.show();
    });

    $('.AddObs').on('click', function (){
        $("#AddObs").dialog({
            autoOpen: true,
            position: { my: "center", at: "top+350", ot: window },
            width: 1000,
            resizable: false,
            title: "Add Observação",
            modal: true,
            open: function () {
                $(this).load('../../Clientes/AddUserPartialView/9');
            },
        });
        return false;
    });

});