// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    setPage();
});

$("#newModalBtn").on("click", function () {
    var btn = $(this);
    var newModalBody = $("#newModalBody");
    var newModalTitle = $("#newModalLongTitle");
    var newModalBtn = $("#submitNewModal");
    newModalBody.empty();
    newModalTitle.empty();
    newModalBtn.attr('data-url', "");

    $.ajax({
        url: btn.data("url"),
        type: 'GET',
        success: function (data) {
            newModalBody.html(data);
            newModalTitle.html(btn.data("label"));
            newModalBtn.attr('data-url', btn.data("create"));

            if (btn.data("id") == "schedule") {
                newModalBtn.addClass("submitNewSchedule");
            } else if (btn.data("id") == "room") {
                newModalBtn.addClass("submitNewRoom");
            }

            $('#newScheduleModal').modal('show');
        },
        error: function (result) {
            $.notify({
                message: 'Erro ao recuperar dados!'
            }, {
                type: 'danger'
            });

        },
        complete: function () {
            setPage();
        }
    });
});

$("#submitNewModal").on("click", function (e) {
    if ($(this).hasClass("submitNewSchedule")) {
        let form = $('#newScheduleForm');
        let start = $("#TimeStart").datetimepicker('getValue');
        let end = $("#TimeEnd").datetimepicker('getValue');

        if (start != "" && end != "" && start >= end) {
            var message = 'A data inicial não pode ser maior ou a mesma que a data final!';
            $("#errorForm").html(message);
            $("#errorForm").show();
        } else {

            formValidate(form);

            if (form.valid()) {
                let data = form.serialize();
                let tableBody = $("#tbody");

                ajaxCreate($(this), data, tableBody);
            }
        }
    }
    else if ($(this).hasClass("submitNewRoom")) {

        let form = $('#newRoomForm');

        formValidate(form);

        if (form.valid())
        {
            let data = form.serialize();
            let tableBody = $("#tbody");

            ajaxCreate($(this), data, tableBody);
        }
    }
});

$('#deleteBtn').on('click', function (e) {
    e.preventDefault();

    let url = $('#deleteBtn').data("url");
    $(this).data('url', "");

    let tableBody = $("#tbody");

    $.ajax({
        url: url,
        type: 'DELETE',
        success: function (data) {
            tableBody.empty();
            tableBody.html(data);
            $.notify({
                message: 'Registro deletado com sucesso!'
            }, {
                type: 'success'
            });
        },
        error: function (result) {
            $.notify({
                message: 'Erro ao excluir o registro! ' + result.responseText
            }, {
                type: 'danger'
            });
        },
        complete: function () {
            setPage();
            $('#deleteModal').modal('hide');
            $(this).attr('data-url', "");
        }

    });
});

$("#tbody").on("click", ".deleteButton", function () {
    let url = $(this).data("url");
    $("#deleteBtn").data("url", url);
    $("#deleteModal").modal('show');
});

function setPage() {

    $('.datepicker').datetimepicker({
        timepicker: true,
        mask: false,
        format: 'd/m/Y H:i:s',
    });
}

function ajaxCreate(btn, data, tableBody)
{
    $.ajax({
        url: btn.data("url"),
        data: data,
        type: 'POST',
        success: function (data) {
            tableBody.empty();
            tableBody.html(data);

            $.notify({
                message: 'Cadastro realizado com sucesso!'
            }, {
                type: 'success'
            });
        },
        error: function (result) {
            $.notify({
                message: 'Ocorreu um erro ao realizar o cadastro! ' + result.responseText
            }, {
                type: 'danger'
            });
        },
        complete: function () {
            setPage();
            $('#newModal').modal('hide');
        }

    });
}

function formValidate(form)
{
    form.validate({
        errorPlacement: function (error, element) {
        },
        invalidHandler: function (event, validator) {
            var errors = validator.numberOfInvalids();
            if (errors) {
                var message = 'Verifique se todos os campos foram preenchidos.';
                $("#errorForm").html(message);
                $("#errorForm").show();
            } else {
                $("#errorForm").hide();
            }
        }
    });
}