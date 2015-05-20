/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/moment/moment.d.ts" />
var Skill;
(function (Skill) {
    (function (Global) {
        Global.skillsPopup = window['SkillsPopup'];
        Global.ganttUrl;
        Global.accessToken;
        Global.fullScreen = false;
        Global.inFullScreenChange = false;

        function setGlobalSettings(gantt, token) {
            Global.ganttUrl = gantt;
            Global.accessToken = token;
        }
        Global.setGlobalSettings = setGlobalSettings;

        function setClientOffset(timezone) {
            //window["RaiseXafCallback"](window["globalCallbackControl"], 'SkillsMainCallback', new Date().getTimezoneOffset(), '', false);
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
        Global.setClientOffset = setClientOffset;

        function openSkillsPopup(width, height, headerText, useIFrame, contentUrl) {
            Global.skillsPopup = window['SkillsPopup'];
            Global.skillsPopup.Shown.handlerInfoList = [];
            Global.skillsPopup.Closing.handlerInfoList = [];

            if (typeof Global.skillsPopup === 'undefined')
                return;
            if (typeof contentUrl !== 'undefined' && useIFrame) {
                Global.skillsPopup.SetContentUrl(contentUrl);
                Global.skillsPopup.Closing.AddHandler(function () {
                    Global.skillsPopup.SetContentUrl('about:blank');
                });
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
            Global.skillsPopup.SetHeight(height);
            Global.skillsPopup.SetWidth(width);
            Global.skillsPopup.popupVerticalOffset = -170;
            Global.skillsPopup.SetHeaderText(headerText);
            Global.skillsPopup.Show();
        }
        Global.openSkillsPopup = openSkillsPopup;

        function closeSkillsPopup() {
            if (typeof Global.skillsPopup === 'undefined')
                return;
            Global.skillsPopup.Hide();
        }
        Global.closeSkillsPopup = closeSkillsPopup;

        function updateActivePopupSize(width, height) {
            window.parent['ActivePopupControls'][0].SetWidth(width);
            window.parent['ActivePopupControls'][0].SetHeight(height);
            window.parent['ActivePopupControls'][0].UpdatePosition();
        }
        Global.updateActivePopupSize = updateActivePopupSize;

        function openSkillsPopupFromAction(objectId, actions, checkIfAssigned, originElement, statusId, callback, objectType, hasPermissionsToAssign) {
            if (typeof statusId === "undefined") { statusId = ""; }
            if (typeof callback === "undefined") { callback = null; }
            if (typeof objectType === "undefined") { objectType = ""; }
            if (typeof hasPermissionsToAssign === "undefined") { hasPermissionsToAssign = false; }
            var index = null;
            var actionNames = actions.split(";");
            if (actionNames.some(function (elem, i) {
                index = i;
                return elem == "RequestConfirmation";
            }) && actionNames.length > 1) {
                var response = confirm("Are you sure you want to change state?");
                if (!response)
                    return;
                actionNames.splice(index, 1);
            }
            switch (actionNames[0]) {
                case "Preview":
                    openSkillsPopup(774, 240, 'Preview', true, 'Views/PopupWebForm.aspx');

                    break;
                default:
                    if (callback != null)
                        callback(originElement);
                    break;
            }
        }
        Global.openSkillsPopupFromAction = openSkillsPopupFromAction;

        function onTabClick(s, e) {
            e.processOnServer = false;
            window["RaiseXafCallback"](window["globalCallbackControl"], 'SkillsMainCallback', e.item.index, '', false);
            return true;
        }
        Global.onTabClick = onTabClick;
    })(Skill.Global || (Skill.Global = {}));
    var Global = Skill.Global;
})(Skill || (Skill = {}));
//# sourceMappingURL=skill.global.js.map
