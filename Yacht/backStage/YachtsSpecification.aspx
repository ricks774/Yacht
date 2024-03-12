<%@ Page Title="" Language="C#" MasterPageFile="~/backStage/DashBoard.Master" AutoEventWireup="true" CodeBehind="YachtsSpecification.aspx.cs" Inherits="Yacht.backStage.YachtsSpecification" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-3">
            <h6>YachtsModel：</h6>
            <asp:GridView ID="GV_Yachts" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:HyperLinkField DataTextField="YachtModel" HeaderText="型號" DataNavigateUrlFields="YachtsId" DataNavigateUrlFormatString="~/backStage/YachtsSpecification.aspx?Id={0}" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-9">
            <div class="row">
                <div>
                    <asp:Label ID="Lb_OverViewDimensions" runat="server" Text="Detail Specification" CssClass="h6" Visible="false"></asp:Label>
                    <CKEditor:CKEditorControl ID="CKED" runat="server" BasePath="/Scripts/ckeditor/"
                        Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize
                    TextColor|BGColor
                    Link|Image"
                        Height="400px" Visible="false"></CKEditor:CKEditorControl>
                    <asp:Button ID="Btn_UpdateDetail" runat="server" Text="更新規格" class="btn btn-outline-primary btn-block mt-3" Visible="false" OnClick="Btn_UpdateDetail_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
