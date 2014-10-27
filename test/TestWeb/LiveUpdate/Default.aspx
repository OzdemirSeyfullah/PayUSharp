<%@ Page Language="C#" Inherits="TestWeb.LiveUpdate.Default" MasterPageFile="~/Master.master" %>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <div class="page-header">
        <h1>Örnek Ödeme Sayfası</h1>
    </div>

     <form id="form1" runat="server" class="form-horizontal">
        <div class="control-group">
            <asp:Label AssociatedControlID="textBoxEmail" runat="server" CssClass="control-label">
                Email Adresi:
            </asp:Label>
            <div class="controls">
                <asp:TextBox id="textBoxEmail" runat="server" />
            </div>
        </div>
        <div class="control-group">
            <asp:Label AssociatedControlID="textBoxName" runat="server" CssClass="control-label">
                Ad:
            </asp:Label>
            <div class="controls">
                <asp:TextBox id="textBoxName" runat="server" />
            </div>
        </div>
        <div class="control-group">
            <asp:Label AssociatedControlID="textBoxLastName" runat="server" CssClass="control-label">
                Soyad:
            </asp:Label>
            <div class="controls">
                <asp:TextBox id="textBoxLastName" runat="server" />
            </div>
        </div>
        <div class="form-actions">
            <asp:Button id="button1" runat="server" CssClass="btn btn-primary" Text="Ödeme Yap" OnClick="button1Clicked" />
        </div>
     </form>
     <hr/>
     
    <asp:Literal id="ltrLiveUpdateForm" runat="server" />
</asp:Content>