<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default" EnableViewState="false"
    ValidateRequest="false" CodeBehind="Default.aspx.cs" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v14.2, Version=14.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v14.2, Version=14.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" 
    TagPrefix="cc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Page</title>
    <meta http-equiv="Expires" content="0" />
    <style type="text/css">

        .disabledDiv {
    pointer-events: none;
    opacity: 0.4;
}

    #Horizontal_NTAC_PC_CC{
        float: right !important;
    }

    </style>
    <script type="text/javascript" src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/moment.min.js"></script>
    <script type="text/javascript" src="//underscorejs.org/underscore-min.js"></script>
    <script type="text/javascript" src="../Scripts/trackContainerDataPlugin.js"></script>
    <script type="text/javascript" src="../Scripts/skill.global.js"></script>
    <script type="text/javascript" src="../Scripts/getData.js"></script>
    <script type="text/javascript" src="../Scripts/js.cookie.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/jstimezonedetect/1.0.4/jstz.min.js"></script>
</head>
<body class="VerticalTemplate">
    <script type="text/javascript">
        var timezone = jstz.determine();
        var timezone_cookie = "skillsTimezoneOffset";
        var timezoneName_cookie = "skillsTimezone";

        if (Cookies.get(timezone_cookie) === undefined) {
            Cookies.set(timezone_cookie, new Date().getTimezoneOffset());
            Cookies.set(timezoneName_cookie, timezone.name());
        }
        else {
            var storedOffset = parseInt(Cookies.get(timezone_cookie));
            var currentOffset = new Date().getTimezoneOffset();
            if (storedOffset !== currentOffset) {
                Cookies.set(timezone_cookie, new Date().getTimezoneOffset());
            }
            var storedTimezone = Cookies.get(timezoneName_cookie);
            var currentTimezone = timezone.name();
            if (storedTimezone !== currentTimezone) {
                Cookies.set(timezoneName_cookie, currentTimezone);
            }
        }

    </script>
    <form id="form2" runat="server">
    <cc3:XafUpdatePanel ID="UPPopupWindowControl" runat="server">
        <cc4:XafPopupWindowControl runat="server" ID="PopupWindowControl" />
    </cc3:XafUpdatePanel>
    <cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
    <cc5:ASPxPopupControl runat="server" EnableClientSideAPI="true" ClientInstanceName="SkillsPopup" ID="SkillsPopup" AllowResize="true" 
            AutoUpdatePosition="true" AllowDragging="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Modal="true"
            EnableViewState="false" EnableHierarchyRecreation="true" CloseAction="CloseButton" Width="0px" Height="0px" EnableCallbackAnimation="false">
            <ClientSideEvents Closing="function(s, e) { if(s.contentUrl != '') s.SetContentUrl('about:blank');}" />
            <ContentCollection>
                <cc5:PopupControlContentControl  runat="server" ID="SkillsPopupContentControl" ClientIDMode="Static" SupportsDisabledAttribute="True">
                    <div id="SkillsPopupInnerContainer"></div>
                </cc5:PopupControlContentControl>
            </ContentCollection>
        </cc5:ASPxPopupControl>
        <cc5:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="100%" 
			HideContentOnCallback="False" ShowLoadingPanel="False">
			<PanelCollection>
				<cc5:PanelContent runat="server">
					
				</cc5:PanelContent>
			</PanelCollection>
		</cc5:ASPxCallbackPanel>
        <cc5:ASPxCallbackPanel ID="ASPxCallbackPanel4" runat="server" HideContentOnCallback="False">
									<PanelCollection>
										<cc5:PanelContent runat="server"></cc5:PanelContent>
									</PanelCollection>
								</cc5:ASPxCallbackPanel>
    <div runat="server" id="Content" />
    </form>
</body>
</html>
