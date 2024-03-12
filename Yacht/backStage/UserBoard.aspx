<%@ Page Title="" Language="C#" MasterPageFile="~/backStage/DashBoard.Master" AutoEventWireup="true" CodeBehind="UserBoard.aspx.cs" Inherits="Yacht.backStage.UserBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
            新增使用者
        </button>

        <!------------------------------------------------ Bootstarp彈跳視窗開始 ------------------------------------------------>
        <div class="modal fade" id="myModal">
            <div class="modal-dialog">
                <div id="modalContentDiv" class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">新增使用者</h4>
                        <asp:Button ID="newAccount_btn" runat="server" CssClass="close" Text="&times;" OnClientClick="return closeModal();" Visible="false" />
                    </div>
                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="m-2">
                            <asp:Label ID="lb_account" runat="server" Text="帳號: "></asp:Label>
                            <asp:TextBox ID="input_act" runat="server" CssClass="form-label"></asp:TextBox>
                            <asp:Label ID="lb_account_check" runat="server" Text="帳號重複" Visible="false"></asp:Label>
                        </div>
                        <br />
                        <div class="m-2">
                            <asp:Label ID="lb_password" runat="server" Text="密碼: "></asp:Label>
                            <asp:TextBox ID="input_pwd" runat="server" CssClass="form-label"></asp:TextBox>
                        </div>
                        <br />
                        <div class="m-2">
                            <asp:Label ID="lb_name" runat="server" Text="姓名: "></asp:Label>
                            <asp:TextBox ID="input_name" runat="server" CssClass="form-label"></asp:TextBox>
                        </div>
                        <br />
                        <%-- <div class="m-2">
                                                        <asp:CheckBox ID="chkMaxAlbum" runat="server" Text="最高權限" CssClass="mx-3" />
                                                    </div>--%>
                    </div>
                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <asp:Button ID="Btn_newAccount" runat="server" Text="新增" CssClass="btn btn-primary" OnClick="Btn_newAccount_Click" />
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!------------------------------------------------ Bootstarp彈跳視窗結束 ------------------------------------------------>
    </div>

    <asp:GridView ID="GV_all" runat="server" AutoGenerateColumns="False" DataKeyNames="Account" CssClass="table table-striped" OnRowCancelingEdit="GV_all_RowCancelingEdit" OnRowDeleting="GV_all_RowDeleting" OnRowEditing="GV_all_RowEditing" OnRowUpdating="GV_all_RowUpdating">
        <Columns>
            <asp:BoundField DataField="Account" HeaderText="帳號" ReadOnly="True" SortExpression="Account" />
            <asp:TemplateField HeaderText="名稱" SortExpression="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="最高權限" SortExpression="MaxPower">
                <EditItemTemplate>
                    <asp:CheckBox ID="CB_MaxPower" runat="server" Checked='<%# Bind("MaxPower") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CB_MaxPower" runat="server" Checked='<%# Bind("MaxPower") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LastLoginDate" ReadOnly="True" HeaderText="最後登入時間" SortExpression="LastLoginDate" />
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新" CssClass="btn btn-success"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" CssClass="btn btn-warning"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" CssClass="btn btn-primary"></asp:LinkButton>
                    &nbsp;
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定要刪除嗎？');" CssClass="btn btn-danger"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
