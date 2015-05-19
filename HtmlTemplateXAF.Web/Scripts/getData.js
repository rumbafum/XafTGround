
function getData(document, panelMainElement, loadingPanel)
{
    loadingPanel.ShowInElement(panelMainElement);
    $.ajax({
        url: "Handlers/GetDataHandler.ashx?Document=" + document,
        cache: false,
        method: 'GET'
    })
    .done(function (data) {
        $(panelMainElement).find(".DocumentsCount").text(data);
    })
    .fail(function (jqXHR, textStatus) { console.log("Error!!!") })
    .always(function () {
        loadingPanel.Hide();
    });
}