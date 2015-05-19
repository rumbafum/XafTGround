<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliverableHtmlWebUserControl.ascx.cs" Inherits="HtmlTemplateXAF.Web.Controls.DeliverableHtmlWebUserControl" %>

<style type="text/css">
    td.dxheViewArea { border: none; }
    .error{
            border-color: red;
            border-style: solid;
            border-width: 1px;
        }
    .checkBox-error{
        outline: 1px solid red;
    }
</style>
<div id="editMode" style="display: none"><%= Convert.ToInt16(IsEditMode) %></div>

<div id="DeliverableHtmlEditorContainer">

    <%= HTML %>

</div>

<input type="hidden" id="formData" class="formData" runat="server">
