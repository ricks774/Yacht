<%@ Page Title="" Language="C#" MasterPageFile="~/backStage/DashBoard.Master" AutoEventWireup="true" CodeBehind="YachtsAlbum.aspx.cs" Inherits="Yacht.backStage.YachtsAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-3">
            <h6>YachtsModel：</h6>
            <asp:GridView ID="GV_Yachts" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:HyperLinkField DataTextField="YachtModel" HeaderText="型號" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/backStage/YachtsAlbum.aspx?id={0}" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-9">
            <div class="row">
                <div class="mb-3">
                    <h6>Layout :</h6>
                    <div class="input-group">
                        <asp:FileUpload ID="Fi_UploadLayout" runat="server" class="btn btn-outline-primary btn-block" AllowMultiple="true" Visible="false" />
                        <asp:Button ID="Btn_UploadLayout" runat="server" Text="上傳照片" class="btn btn-primary" OnClick="Btn_UploadLayout_Click" Visible="false" />
                    </div>
                    <div>
                        <asp:GridView ID="GV_Layout" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped" OnRowDeleting="GV_Layout_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                                <asp:ImageField DataImageUrlField="ImgPath" ControlStyle-Height="150px">
                                    <ControlStyle Height="200px" Width="300px"></ControlStyle>
                                </asp:ImageField>
                                <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" ReadOnly="true" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="Btn_delLayout" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定要刪除嗎？');" CssClass="btn btn-danger" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div>
                    <h6>遊艇型錄照片 :</h6>
                    <div class="input-group">
                        <asp:FileUpload ID="Fi_UploadImg" runat="server" class="btn btn-outline-primary btn-block" AllowMultiple="true" Visible="false" />
                        <asp:Button ID="Btn_UploadImg" runat="server" Text="上傳照片" class="btn btn-primary" OnClick="Btn_UploadImg_Click" Visible="false" />
                    </div>
                    <div>
                        <asp:GridView ID="GV_Img" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped" OnRowDeleting="GV_Img_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                                <asp:ImageField DataImageUrlField="ImgPath" ControlStyle-Height="150px">
                                    <ControlStyle Height="200px" Width="300px"></ControlStyle>
                                </asp:ImageField>
                                <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" ReadOnly="true" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="Btn_delImg" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定要刪除嗎？');" CssClass="btn btn-danger" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
