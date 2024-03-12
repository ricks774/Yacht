<%@ Page Title="" Language="C#" MasterPageFile="~/backStage/DashBoard.Master" AutoEventWireup="true" CodeBehind="CompanyEdit.aspx.cs" Inherits="Yacht.backStage.CompanyEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h6>About Us :</h6>
    <CKEditor:CKEditorControl ID="CKED" runat="server" BasePath="/Scripts/ckeditor/"
        Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
        NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
        /
        Styles|Format|Font|FontSize
        TextColor|BGColor
        Link|Image"
        Height="400px">
    </CKEditor:CKEditorControl>
    <asp:Label ID="UploadAboutUsLab" runat="server" Visible="False" ForeColor="#009933" class="d-flex justify-content-center"></asp:Label>
    <asp:Button ID="UploadAboutUsBtn" runat="server" Text="Upload About Us Content" class="btn btn-outline-primary btn-block mt-3 mb-5" OnClick="UploadAboutUsBtn_Click" />

    <h6>Certificat :</h6>
    <%--<asp:TextBox ID="certificatTbox" runat="server" type="text" class="form-control" placeholder="Enter certificat text" TextMode="MultiLine" Height="200px"></asp:TextBox>--%>
        <CKEditor:CKEditorControl ID="CKED_certif" runat="server" BasePath="/Scripts/ckeditor/"
        Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
        NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
        /
        Styles|Format|Font|FontSize
        TextColor|BGColor
        Link|Image"
        Height="400px">
    </CKEditor:CKEditorControl>
    <asp:Label ID="uploadCertificatLab" runat="server" Visible="False" ForeColor="#009933" class="d-flex justify-content-center"></asp:Label>
    <asp:Button ID="uploadCertificatBtn" runat="server" Text="Upload Certificat Text" class="btn btn-outline-primary btn-block mt-3" OnClick="UploadCertificatBtn_Click" />

</asp:Content>
