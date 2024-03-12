<%@ Page Title="" Language="C#" MasterPageFile="~/backStage/DashBoard.Master" AutoEventWireup="true" CodeBehind="YachtsOverview.aspx.cs" Inherits="Yacht.backStage.YachtsEdit" %>

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
                    <asp:HyperLinkField DataTextField="YachtModel" HeaderText="型號" DataNavigateUrlFields="YachtsId" DataNavigateUrlFormatString="~/backStage/YachtsOverview.aspx?Id={0}" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-9">
            <div class="row">
                <div>
                    <asp:Label ID="Lb_UploadTitle" runat="server" Text="Download File" CssClass="h6" Visible="false"></asp:Label>
                    <br />
                    <asp:HyperLink ID="Lr_FileName" runat="server" Target="_blank"></asp:HyperLink>
                    <div class="input-group my-3">
                        <asp:FileUpload ID="Fi_UploadFile" runat="server" class="btn btn-outline-primary btn-block" Visible="false" />
                        <asp:Button ID="Btn_UploadFile" runat="server" Text="上傳型錄檔案" class="btn btn-primary" Visible="false" OnClick="Btn_UploadFile_Click" />
                    </div>
                </div>
                <div class="mb-4">
                    <asp:Label ID="Lb_OverViewContents" runat="server" Text="OverView Contents" CssClass="h6" Visible="false"></asp:Label>
                    <asp:TextBox ID="Tbox_OverViewContent" runat="server" Height="250px" Width="100%" TextMode="MultiLine" CssClass="form-control" Visible="false"></asp:TextBox>
                    <asp:Button ID="Btn_UpdateYachtsContents" runat="server" Text="更新內容" class="btn btn-outline-primary btn-block mt-3" Visible="false" OnClick="Btn_UpdateYachtsContents_Click" />
                </div>
                <div>
                    <asp:Label ID="Lb_OverViewDimensions" runat="server" Text="OverView Dimensions" CssClass="h6" Visible="false"></asp:Label>
                    <CKEditor:CKEditorControl ID="CKED" runat="server" BasePath="/Scripts/ckeditor/"
                        Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize
                    TextColor|BGColor
                    Link|Image"
                        Height="400px" Visible="false"></CKEditor:CKEditorControl>
                    <asp:Button ID="Btn_UpdateYachtsDimensions" runat="server" Text="更新規格" class="btn btn-outline-primary btn-block mt-3" OnClick="Btn_UpdateYachtsDimensions_Click" Visible="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
    </div>
</asp:Content>
