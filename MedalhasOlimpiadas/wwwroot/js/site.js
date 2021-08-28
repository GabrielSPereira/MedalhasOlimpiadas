// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#formPesquisa").submit(function (e) {
    e.preventDefault();
    e.stopImmediatePropagation();

    var form = $(this);
    var url = form.attr('action');

    $.ajax({
        type: "POST",
        url: url,
        data: form.serialize(),
        success: function (response) {
            if (!response["content"].startsWith("Erro: ")) {
                $(".divGrid").css("display", "block");
                $(".divPesquisa").css("display", "block");
                $(".divPesquisa").html(response["content"]);
            }
        },
        failure: function (response) {
        },
        error: function (response) {
        }
    });
});

function detalhesAjax(entidade, id) {
    $.ajax({
        type: "POST",
        url: "/" + entidade + "/" + entidade + "AjaxForm/" + id,
        success: function (response) {
            if (!response["content"].startsWith("Erro: ")) {
                $(".divPesquisa").css("display", "none");
                $(".divGrid").css("display", "none");
                $(".divForm").css("display", "block");
                $(".divForm").html(response["content"]);
            }
        },
        failure: function (response) {
        },
        error: function (response) {
        }
    });
}

function removeAjax(entidade, id) {
    if (confirm('Deseja realmente alterar o status?')) {
        $.ajax({
            type: "POST",
            url: "/" + entidade + "/" + entidade + "AjaxRemover/" + id,
            contentType: "text/plain; charset=utf-8",
            dataType: "text",
            success: function (response) {
                if (!response.startsWith("Erro: ")) {
                    if ($("#botaoRemover" + id).val() == "Ativar") {
                        $("#botaoRemover" + id).val("Inativar");
                        $("#botaoRemover" + id).html("Inativar");
                    } else {
                        $("#botaoRemover" + id).val("Ativar");
                        $("#botaoRemover" + id).html("Ativar");
                    }
                }
            },
            failure: function (response) {
            },
            error: function (response) {
            }
        });
    } 
}
