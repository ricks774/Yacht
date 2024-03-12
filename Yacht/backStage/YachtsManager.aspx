<%@ Page Title="" Language="C#" MasterPageFile="~/backStage/DashBoard.Master" AutoEventWireup="true" CodeBehind="YachtsManager.aspx.cs" Inherits="Yacht.backStage.Yahts_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-4">
            <h6>新增遊艇</h6>
            <asp:CheckBox ID="CBox_NewDesign" runat="server" Text="NewDesign" Width="50%" />
            <asp:CheckBox ID="CBox_NewBuilding" runat="server" Text="NewBuilding" Width="50%" />
            <div class="input-group mb-3">
                <asp:TextBox ID="TBox_AddYachtModel" runat="server" type="text" class="form-control" placeholder="Model" Width="30%"></asp:TextBox>
                <asp:TextBox ID="TBox_AddYachtLength" runat="server" type="text" class="form-control" placeholder="Length"></asp:TextBox>
                <asp:Button ID="Btn_AddYachtModel" runat="server" Text="Add" class="btn btn-outline-primary btn-block" OnClick="Btn_AddYachtModel_Click" />
            </div>
        </div>
        <div class="col-8">
            <h6>遊艇型號封面</h6>
            <div class="input-group my-3">
                <asp:DropDownList ID="Dl_bannerList" runat="server" AutoPostBack="True" DataValueField="id" DataTextField="YachtModel" Width="30%" Font-Bold="True" class="btn btn-outline-primary dropdown-toggle" OnSelectedIndexChanged="Dl_bannerList_SelectedIndexChanged"></asp:DropDownList>
                <asp:FileUpload ID="BannerImg_FileUpload" runat="server" class="btn btn-outline-primary btn-block" AllowMultiple="True" />
                <asp:Button ID="Btn_BannerImgUpload" runat="server" Text="Upload" class="btn btn-primary" OnClick="Btn_BannerImgUpload_Click" />
                <br />
                <asp:Image ID="YachtsImg" runat="server" Width="400px" />
            </div>
        </div>
    </div>
    <hr />
    <div class="row">
        <asp:GridView ID="GV_all" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped table-hover" OnRowCancelingEdit="GV_all_RowCancelingEdit" OnRowDeleting="GV_all_RowDeleting" OnRowEditing="GV_all_RowEditing" OnRowUpdating="GV_all_RowUpdating">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:TemplateField HeaderText="YachtModelName" SortExpression="YachtModelName">
                    <EditItemTemplate>
                        <asp:TextBox ID="Ed_YachtsModelName" runat="server" Text='<%# Bind("YachtModelName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Lb_YachtModelName" runat="server" Text='<%# Bind("YachtModelName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="YachtModelNo" SortExpression="YachtModelNo">
                    <EditItemTemplate>
                        <asp:TextBox ID="Ed_YachtsModelNo" runat="server" Text='<%# Bind("YachtModelNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Lb_YachtModelNo" runat="server" Text='<%# Bind("YachtModelNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField DataField="IsNewDesign" HeaderText="IsNewDesign" SortExpression="IsNewDesign" />
                <asp:CheckBoxField DataField="IsNewBuilding" HeaderText="IsNewBuilding" SortExpression="IsNewBuilding" />
                <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" SortExpression="CreateDate" ReadOnly="True" />
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新" CssClass="btn btn-success"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" CssClass="btn btn-warning"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" CssClass="btn btn-primary"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定要刪除嗎？');" CssClass="btn btn-danger"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
