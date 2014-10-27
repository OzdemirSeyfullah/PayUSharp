<%@ Page Language="C#" Inherits="TestWeb.Default" MasterPageFile="~/Master.master" %>
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
        <hr />
        <div class="control-group">
            <asp:Label AssociatedControlID="textBoxCardHolderName" runat="server" CssClass="control-label">
                Kart Sahibinin Adı Soyadı:
            </asp:Label>
            <div class="controls">
                <asp:TextBox id="textBoxCardHolderName" runat="server" />
            </div>
        </div>
        <div class="control-group">
            <asp:Label AssociatedControlID="textBoxCardNumber" runat="server" CssClass="control-label">
                Kart Numarası:
            </asp:Label>
            <div class="controls">
                <asp:TextBox id="textBoxCardNumber" runat="server" />
            </div>
        </div>
        <div class="control-group">
            <asp:Label AssociatedControlID="textBoxExpiryMonth" runat="server" CssClass="control-label">
                Son Kullanma (Ay - Yıl):
            </asp:Label>
            <div class="controls">
                <asp:TextBox id="textBoxExpiryMonth" runat="server" CssClass="span1"/>
                <asp:TextBox id="textBoxExpiryYear" runat="server" CssClass="span1" />
            </div>
        </div>
        <div class="control-group">
            <asp:Label AssociatedControlID="textBoxCVV" runat="server" CssClass="control-label">
                Güvenlik Numarası:
            </asp:Label>
            <div class="controls">
                <asp:TextBox id="textBoxCVV" runat="server" />
            </div>
        </div>
        <div class="control-group">
            <asp:Label AssociatedControlID="textBoxExpiryYear" runat="server" CssClass="control-label">
                Taksit Sayısı:
            </asp:Label>
            <div class="controls">
                <asp:DropDownList id="ddlInstallmentCount" runat="server">
                    <asp:ListItem Text="Peşin" Value="1" />
                    <asp:ListItem Text="2 Taksit" Value="2" />
                    <asp:ListItem Text="3 Taksit" Value="3" />
                    <asp:ListItem Text="4 Taksit" Value="4" />
                    <asp:ListItem Text="5 Taksit" Value="5" />
                    <asp:ListItem Text="6 Taksit" Value="6" />
                    <asp:ListItem Text="7 Taksit" Value="7" />
                    <asp:ListItem Text="8 Taksit" Value="8" />
                    <asp:ListItem Text="9 Taksit" Value="9" />
                    <asp:ListItem Text="10 Taksit" Value="10" />
                    <asp:ListItem Text="11 Taksit" Value="11" />
                    <asp:ListItem Text="12 Taksit" Value="12" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-actions">
            <asp:Button id="button1" runat="server" CssClass="btn btn-primary" Text="Ödeme Yap" OnClick="button1Clicked" />
        </div>
     </form>
     <asp:Literal id="ltrOutput" runat="server" />
</asp:Content>