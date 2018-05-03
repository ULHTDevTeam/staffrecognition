function maxlength(element)
{
    if (element.GetText().length > 500)
    {
        element.SetText(element.GetText.substring(0, 500));
    }
    else {
        txtCounter.SetText(500 - element.GetText().length);
    }
}

var isPostbackInitiated = false;
function OnClick(s, e) {
    ASPxClientEdit.ValidateEditorsInContainer(null);
    if (ASPxClientEdit.AreEditorsValid()) {
        e.processOnServer = !isPostbackInitiated;
        isPostbackInitiated = true;
        setTimeout(function () {
            s.SetEnabled(false);
            s.SetText("Processing...");
        }, 100);
    }
}