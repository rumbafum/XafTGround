<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardWebUserControl.ascx.cs" Inherits="HtmlTemplateXAF.Web.Controls.DashboardWebUserControl" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v14.2, Version=14.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Src="~/Controls/DeliverablesKPIWebUserControl.ascx" TagPrefix="uc" TagName="DeliverablesKPIWebUserControl" %>


<div style="margin-bottom:20px">
    <uc:DeliverablesKPIWebUserControl runat="server" ID="DeliverablesUC" DocumentType="Deliverable"></uc:DeliverablesKPIWebUserControl>
</div>

<div>
    <uc:DeliverablesKPIWebUserControl runat="server" ID="ProjectsUC" DocumentType="Project"></uc:DeliverablesKPIWebUserControl>
</div>

<br />
<br />

<script type="text/javascript" id="dxss_dashboardMain">

    //function OnInit(s, e) {
    //    SkillsCallback.PerformCallback('deliverable');
    //    SkillsCallback.PerformCallback('project');
    //}

    //function OnCallbackComplete(s, e) {
    //    console.log(e.result);
    //}

</script>

<%--<dx:ASPxCallback ID="SkillsCallback" runat="server" ClientInstanceName="SkillsCallback"
        OnCallback="SkillsCallback_Callback">
        <ClientSideEvents CallbackComplete="OnCallbackComplete" Init="OnInit" />
    </dx:ASPxCallback>--%>