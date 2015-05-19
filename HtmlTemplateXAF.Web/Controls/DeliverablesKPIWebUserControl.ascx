<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliverablesKPIWebUserControl.ascx.cs" Inherits="HtmlTemplateXAF.Web.Controls.DeliverablesKPIWebUserControl" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v14.2, Version=14.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>


<dx:ASPxCallbackPanel runat="server" ID="CallbackPanel" ClientInstanceName="CallbackPanel" 
    EnableCallbackAnimation="true" HideContentOnCallback="false" OnCallback="CallbackPanel_Callback"
    Width="200" Height="100">
    <SettingsLoadingPanel Enabled="false" ShowImage="true" Delay="0" />
    <PanelCollection>
        <dx:PanelContent>
            <div id="DeliverablesDiv" style="width:200px;height:100px;background-color:green;color:white">
                <asp:label runat="server" ID="DocumentsCount" CssClass="DocumentsCount" Text="---------------"></asp:label>
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:ASPxLoadingPanel runat="server" ID="KPILoadingPanel" ClientInstanceName="KPILoadingPanel"></dx:ASPxLoadingPanel>