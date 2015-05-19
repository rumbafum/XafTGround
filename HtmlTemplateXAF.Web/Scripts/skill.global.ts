/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/moment/moment.d.ts" />

module Skill.Global {

    export var skillsPopup = window['SkillsPopup'];
    export var ganttUrl: string;
    export var accessToken: string;
    export var fullScreen = false;
    export var inFullScreenChange = false;

    export function setGlobalSettings(gantt: string, token: string) {
        ganttUrl = gantt;
        accessToken = token;
    }

    export function setClientOffset() {
        window["RaiseXafCallback"](window["globalCallbackControl"], 'SkillsMainCallback', new Date().getTimezoneOffset(), '', false);
        //$.ajax({
        //    url: "Handlers/SetClientSettingsHandler.ashx?clientOffset=" + new Date().getTimezoneOffset(),
        //    cache: false,
        //    method: 'POST'
        //})
        //.done(function (data) {
        //})
        //.fail(function (jqXHR, textStatus) { console.log("Error!!!") })
        //.always(function () {});
        //$('#ClientOffsetHiddenField').val(moment().format());
    }

    export function openSkillsPopup(width, height, headerText, useIFrame, contentUrl) {
        skillsPopup = window['SkillsPopup'];
        skillsPopup.Shown.handlerInfoList = [];
        skillsPopup.Closing.handlerInfoList = [];

        if (typeof skillsPopup === 'undefined')
            return;
        if (typeof contentUrl !== 'undefined' && useIFrame) {
            skillsPopup.SetContentUrl(contentUrl);
            skillsPopup.Closing.AddHandler(function () { skillsPopup.SetContentUrl('about:blank'); });
        }
        //if (typeof contentUrl !== 'undefined' && !useIFrame) {
        //    if ($('#SkillsPopupInnerContainer').length == 0) {
        //        skillsPopup.contentUrl = '';
        //        $('.dxpc-content').html('');
        //        $('.dxpc-content').append("<div id='SkillsPopupInnerContainer'></div>");
        //    }
        //    $('#SkillsPopupInnerContainer').html('');
        //    $('#SkillsPopupInnerContainer').load(contentUrl);
        //}
        skillsPopup.SetHeight(height);
        skillsPopup.SetWidth(width);
        skillsPopup.popupVerticalOffset = -170;
        skillsPopup.SetHeaderText(headerText);
        skillsPopup.Show();
    }

    export function closeSkillsPopup() {
        if (typeof skillsPopup === 'undefined')
            return;
        skillsPopup.Hide();
    }

    export function updateActivePopupSize(width, height) {
        window.parent['ActivePopupControls'][0].SetWidth(width);
        window.parent['ActivePopupControls'][0].SetHeight(height);
        window.parent['ActivePopupControls'][0].UpdatePosition();
    }
    
    export function openSkillsPopupFromAction(objectId: string, actions: string, checkIfAssigned: boolean,
        originElement: any, statusId: string = "", callback: any = null, objectType: string = "", hasPermissionsToAssign: boolean = false) {
        var index = null;
        var actionNames = actions.split(";");
        if (actionNames.some((elem, i) => { index = i; return elem == "RequestConfirmation"; })
            && actionNames.length > 1) {
            var response = confirm("Are you sure you want to change state?");
            if (!response) return;
            actionNames.splice(index, 1);
        }
        switch (actionNames[0]) {
            case "Preview":
                openSkillsPopup(774, 240, 'Preview', true, 'Views/PopupWebForm.aspx');
                //skillsPopup.Shown.AddHandler(() => {
                //    Skill.DeliverableDetails.initDeliverableDetails(Skill.Global.ganttUrl, Skill.Global.accessToken, 'deliverablePreview', objectId, false, false);
                //    var element = $('.changeStatus');
                //    if (element != null && callback != null)
                //        element.on("click", () => {
                //            callback(originElement);
                //        });
                //});
                break;
            default:
                if (callback != null) callback(originElement);
                break;
        }
    }


    export function onTabClick(s, e) {
        e.processOnServer = false;
        window["RaiseXafCallback"](window["globalCallbackControl"], 'SkillsMainCallback', e.item.index, '', false);
        return true;
    }

}