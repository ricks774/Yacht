<%@ Page Title="" Language="C#" MasterPageFile="~/backStage/DashBoard.Master" AutoEventWireup="true" CodeBehind="NewsEdit.aspx.cs" Inherits="Yacht.backStage.NewEdit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <!------------------------------------------------ 新聞標題開始 ------------------------------------------------>
        <div class="row">
            <div class="col-5">
                <h6>新增新聞標題 :</h6>
                <asp:CheckBox ID="CBoxIsTop" runat="server" Text="Top Tag" Width="100%" />
                <asp:TextBox ID="headlineTbox" runat="server" type="text" class="form-control" placeholder="Enter headline text" MaxLength="75"></asp:TextBox>
                <asp:Button ID="AddHeadlineBtn" runat="server" Text="新增標題" class="btn btn-outline-primary btn-block mt-3" OnClick="AdHeadlineBtn_Click" />
                <br />
                <h6>日期 :</h6>
                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="100%" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#3399FF" ForeColor="White" Font-Bold="True" />
                    <TitleStyle BackColor="White" BorderColor="#3399FF" BorderWidth="3px" Font-Bold="True" Font-Size="12pt" ForeColor="#3399FF" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                </asp:Calendar>
                <hr />
            </div>
            <div class="col-6 ms-3">
                <h6>新聞列表 :
                <asp:Label ID="LabIsTop" runat="server" Text="* Select item is top news !" ForeColor="Red" Visible="False" class="badge badge-pill badge-warning text-dark"></asp:Label></h6>
                <asp:RadioButtonList ID="headlineRadioBtnList" runat="server" class="my-3" AutoPostBack="True" OnSelectedIndexChanged="HeadlineRadioBtnList_SelectedIndexChanged"></asp:RadioButtonList>
                <asp:Button ID="deleteNewsBtn" runat="server" Text="刪除新聞" type="button" class="btn btn-danger btn-sm" OnClientClick="return confirm('Are you sure you want to delete？')" Visible="False" OnClick="DeleteNewsBtn_Click" />
                <hr />
                <h6>封面縮圖 :</h6>
                <asp:Literal ID="Show_Cover" runat="server" Visible="false"></asp:Literal>
                <br />
                <div class="input-group my-3">
                    <asp:FileUpload ID="Fi_coverUpload" runat="server" class="btn btn-outline-primary btn-block" />
                    <asp:Button ID="Btn_CoverUpload" runat="server" Text="上傳縮圖" class="btn btn-primary" OnClick="Btn_CoverUpload_Click" />
                </div>
            </div>
        </div>
        <!------------------------------------------------ 概括編輯開始 ------------------------------------------------>
        <div class="row">
            <div class="col-12">
                <h6>新聞概括 :</h6>
                <asp:TextBox ID="Summary_tbx" runat="server" type="text" class="form-control" placeholder="輸入新聞概括" TextMode="MultiLine" Height="170px" MaxLength="325"></asp:TextBox>
                <asp:Label ID="lb_UploadSummary" runat="server" Text="*成功更新!" ForeColor="green" class="d-flex justify-content-center" Visible="False"></asp:Label>
                <asp:Button ID="Btn_SummaryUpdate" runat="server" Text="更新概括" CssClass="btn btn-outline-primary btn-block mt-3" OnClick="Btn_SummaryUpdate_Click" />
            </div>
        </div>
        <!------------------------------------------------ 新聞標題結束 ------------------------------------------------>
        <hr />
        <!------------------------------------------------ 新聞內頁開始 ------------------------------------------------>
        <div class="row">
            <div class="col-12">
                <h6>新聞內容 :</h6>
                <CKEditor:CKEditorControl ID="CKED" runat="server" BasePath="/Scripts/ckeditor/"
                    Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat
                    NumberedList|BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|BidiLtr|BidiRtl
                    /
                    Styles|Format|Font|FontSize
                    TextColor|BGColor
                    Link|Image"
                    Height="400px">
                </CKEditor:CKEditorControl>
                <asp:Label ID="lb_UpdateNewContent" runat="server" Visible="False" ForeColor="#009933" class="d-flex justify-content-center"></asp:Label>
                <asp:Button ID="Btn_UpdateNewContent" runat="server" Text="更新新聞內容" class="btn btn-outline-primary btn-block mt-3" OnClick="Btn_UpdateNewContent_Click" />
            </div>
        </div>
        <!------------------------------------------------ 新聞內頁結束 ------------------------------------------------>
    </div>




</asp:Content>
