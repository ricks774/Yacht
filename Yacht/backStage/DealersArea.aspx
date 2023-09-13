<%@ Page Title="" Language="C#" MasterPageFile="~/backStage/DashBoard.Master" AutoEventWireup="true" CodeBehind="DealersArea.aspx.cs" Inherits="Yacht.backStage.DealersManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contain">
        <div class="row">
            <div class="col-6">
                <div>
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
                        新增國家
                    </button>
                    <!------------------------------------------------ Bootstarp彈跳視窗開始 ------------------------------------------------>
                    <div class="modal fade" id="myModal">
                        <div class="modal-dialog">
                            <div id="modalContentDiv" class="modal-content">
                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4 class="modal-title">新增國家</h4>
                                    <asp:Button ID="btn_newItem" runat="server" CssClass="close" Text="&times;" OnClientClick="return closeModal();" Visible="false" />
                                </div>
                                <!-- Modal body -->
                                <div class="modal-body">
                                    <div class="m-2">
                                        <asp:Label ID="lb_country" runat="server" Text="國家 : "></asp:Label>
                                        <asp:TextBox ID="input_newCountry" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <!-- Modal footer -->
                                <div class="modal-footer">
                                    <asp:Button ID="btn_AddNewCountry" runat="server" Text="新增" CssClass="btn btn-primary" OnClick="Btn_AddNewCountry_Click" />
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">關閉</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!------------------------------------------------ Bootstarp彈跳視窗結束 ------------------------------------------------>
                </div>

                <asp:GridView ID="GV_all" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped" OnRowCancelingEdit="GV_all_RowCancelingEdit" OnRowDeleting="GV_all_RowDeleting" OnRowEditing="GV_all_RowEditing" OnRowUpdating="GV_all_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:TemplateField HeaderText="國家" SortExpression="CountrySort">
                            <EditItemTemplate>
                                <asp:TextBox ID="input_country" runat="server" Width="120px" CssClass="form-control" Text='<%# Bind("CountrySort") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lb_country" runat="server" Text='<%# Bind("CountrySort") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreateDate" HeaderText="建立日期" SortExpression="CreateDate" ReadOnly="true" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="更新" CssClass="btn btn-primary"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" CssClass="btn btn-warning"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" CssClass="btn btn-primary"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定要刪除嗎？');" CssClass="btn btn-danger"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!------------------------------------------------ 右側地區 ------------------------------------------------>
            <div class="col-6">
                <div>
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myAreaModal">
                        新增地區</button>
                    <!------------------------------------------------ Bootstarp彈跳視窗開始 ------------------------------------------------>
                    <div class="modal fade" id="myAreaModal">
                        <div class="modal-dialog">
                            <div id="AreamodalContentDiv" class="modal-content">
                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4 class="modal-title">新增地區</h4>
                                    <asp:Button ID="Button1" runat="server" CssClass="close" Text="&times;" OnClientClick="return closeModal();" Visible="false" />
                                </div>
                                <!-- Modal body -->
                                <div class="modal-body">
                                     
                                    <div class="m-2">
                                        <asp:Label ID="Label1" runat="server" Text="地區 : "></asp:Label>
                                        <asp:TextBox ID="input_area" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <!-- Modal footer -->
                                <div class="modal-footer">
                                    <asp:Button ID="Button2" runat="server" Text="新增" CssClass="btn btn-primary" OnClick="Btn_AddNewArea_Click" />
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">關閉</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!------------------------------------------------ Bootstarp彈跳視窗結束 ------------------------------------------------>
                    <asp:Literal ID="Literal1" runat="server">選擇國家 : </asp:Literal>
                    <asp:DropDownList ID="area_list" runat="server" AutoPostBack="true" OnSelectedIndexChanged="area_list_SelectedIndexChanged" CssClass="dropdown-toggle"></asp:DropDownList>
                </div>

                <asp:GridView ID="GV_area" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-striped" OnRowCancelingEdit="GV_area_RowCancelingEdit" OnRowDeleting="GV_area_RowDeleting" OnRowEditing="GV_area_RowEditing" OnRowUpdating="GV_area_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="編號" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:TemplateField HeaderText="地區" SortExpression="CountrySort">
                            <EditItemTemplate>
                                <asp:TextBox ID="update_area" runat="server" Width="120px" CssClass="form-control" Text='<%# Bind("AreaSort") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lb_area" runat="server" Text='<%# Bind("AreaSort") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreateDate" HeaderText="建立日期" SortExpression="CreateDate" ReadOnly="true" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="area_update" runat="server" CausesValidation="True" CommandName="Update" Text="更新" CssClass="btn btn-primary"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="area_cancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" CssClass="btn btn-warning"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="area_edit" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" CssClass="btn btn-primary"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="area_del" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('確定要刪除嗎？');" CssClass="btn btn-danger"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <!------------------------------------------------ 右側地區 ------------------------------------------------>

</asp:Content>
