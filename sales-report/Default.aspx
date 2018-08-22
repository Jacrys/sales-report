<%@ Page Title="Sales Report Generator" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Sales Report</h1>
        <p class="lead">Run and Download a Sales Report for selected dates.</p>
    </div>
    <div class="form-row align-items-end">
        <div class="form-group col-md-4">
            <asp:Label runat="server" AssociatedControlID="frmStartDate">Start Date: </asp:Label>
            <asp:TextBox ID="frmStartDate" runat="server" TextMode="Date" CssClass="form-control"/>
        </div>
        <div class="form-group col-md-4">
            <asp:Label runat="server" AssociatedControlID="frmEndDate">End Date: </asp:Label>
            <asp:TextBox ID="frmEndDate" runat="server" TextMode="Date" CssClass="form-control"/>
        </div>
        <div class="form-group col-md-1 offset-md-1">
            <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" ID="btnSubmit" OnClick = "Submit_Click" />
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="gridPanel">
        <ContentTemplate>
            <div class="form-row">
                <div class="col-md-10">
                    <asp:ListView ID="tblSalesOrders" runat="server" Visible="false"></asp:ListView>
                </div>
                <div class="col-md-3">
                    <asp:Button runat="server" CssClass="btn btn-success fa" Text="Download Report" ID="btnDownload" OnClick ="Download_Click" Visible ="false"/>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
