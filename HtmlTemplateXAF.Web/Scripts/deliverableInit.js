
var tracker = null;

function setupDeliverables(readOnly) {
    var editMode = $("#editMode").text();
    if(editMode == "1")
    {
        //tracker = $("#DeliverableHtmlEditorContainer").find("#DeliverableHtmlEditor").find("#DeliverableHtmlEditor_PreviewIFrame").contents().find('div').trackControls({
        //    loadDefaultValues: true,
        //    appendToForm: false
        //});
        tracker = $("#DeliverableHtmlEditorContainer").find('div').trackControls({
            loadDefaultValues: true,
            appendToForm: false
        });
    }
}

