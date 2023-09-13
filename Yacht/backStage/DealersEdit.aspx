<%@ Page Title="" Language="C#" MasterPageFile="~/backStage/DashBoard.Master" AutoEventWireup="true" CodeBehind="DealersEdit.aspx.cs" Inherits="Yacht.backStage.DealersEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contain">
        <div class="row d-flex justify-content-center">
            <div class="col-3">
                <div class="mb-2">
                    <asp:Label ID="Label2" runat="server" Text="經銷商列表" CssClass="h6"></asp:Label>
                    <br />
                    <div>
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">
                            新增經銷商
                        </button>
                        <!------------------------------------------------ Bootstarp彈跳視窗開始 ------------------------------------------------>
                        <div class="modal fade" id="myModal">
                            <div class="modal-dialog">
                                <div id="modalContentDiv" class="modal-content">
                                    <!-- Modal Header -->
                                    <div class="modal-header">
                                        <h4 class="modal-title">新增經銷商</h4>
                                        <asp:Button ID="btn_newDealer" runat="server" CssClass="close" Text="&times;" OnClientClick="return closeModal();" Visible="false" />
                                    </div>
                                    <!-- Modal body -->
                                    <div class="modal-body">
                                        <div class="m-2">
                                            <asp:Label ID="lb_country" runat="server" Text="Name : "></asp:Label>
                                            <asp:TextBox ID="tbx_addName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="m-2">
                                            <asp:Label ID="Label11" runat="server" Text="Country : "></asp:Label>
                                            <asp:DropDownList ID="dl_addCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Dl_addCountry_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="m-2">
                                            <asp:Label ID="Label12" runat="server" Text="Area : "></asp:Label>
                                            <asp:DropDownList ID="dl_addArea" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="m-2">
                                            <asp:Label ID="Label13" runat="server" Text="Contact : "></asp:Label>
                                            <asp:TextBox ID="tbx_addContact" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="m-2">
                                            <asp:Label ID="Label14" runat="server" Text="Address : "></asp:Label>
                                            <asp:TextBox ID="tbx_addAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="m-2">
                                            <asp:Label ID="Label15" runat="server" Text="Tel : "></asp:Label>
                                            <asp:TextBox ID="tbx_addTel" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="m-2">
                                            <asp:Label ID="Label16" runat="server" Text="Fax : "></asp:Label>
                                            <asp:TextBox ID="tbx_addFax" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="m-2">
                                            <asp:Label ID="Label17" runat="server" Text="Email : "></asp:Label>
                                            <asp:TextBox ID="tbx_addEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="m-2">
                                            <asp:Label ID="Label18" runat="server" Text="Link : "></asp:Label>
                                            <asp:TextBox ID="tbx_addLink" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="m-2">
                                            <asp:Label ID="Label19" runat="server" Text="Thumbnail : "></asp:Label>
                                            <br />
                                            <asp:FileUpload ID="upload_thumbnail" runat="server" />
                                        </div>
                                    </div>
                                    <!-- Modal footer -->
                                    <div class="modal-footer">
                                        <asp:Button ID="btn_AddNewDealer" runat="server" Text="新增" CssClass="btn btn-primary" OnClick="Btn_AddNewDealer_Click" />
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">關閉</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!------------------------------------------------ Bootstarp彈跳視窗結束 ------------------------------------------------>
                    </div>
                </div>
                <asp:GridView ID="GV_dealers" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" DataKeyNames="Id">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:HyperLinkField DataTextField="Name" HeaderText="經銷商" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/backStage/DealersEdit.aspx?Id={0}" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-7">
                <div class="mb-2">
                    <asp:Label ID="Label1" runat="server" Text="經銷商資料" CssClass="h6"></asp:Label>
                </div>
                <div class="">
                    <asp:Repeater ID="rp_dealerDetail" runat="server" OnItemCommand="Rp_dealerDetail_ItemCommand" OnItemDataBound="Rp_dealerDetail_ItemDataBound">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="編輯" CommandName="Edit" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary mb-2" />
                            <asp:Button ID="btn_del" runat="server" Text="刪除" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' CssClass="btn  btn-outline-danger mb-2 ms-2" OnClientClick="return confirm('確定要刪除嗎？');" />
                            <!------------------------------------------------ 資料展示開始 ------------------------------------------------>
                            <table class="table table-striped border">
                                <asp:Panel ID="plItem" runat="server">
                                    <tr>
                                        <th>Name</th>
                                        <td><%# Eval("Name") %></td>
                                    </tr>
                                    <tr>
                                        <th>Country</th>
                                        <td><%# Eval("CountrySort") %></td>
                                    </tr>
                                    <tr>
                                        <th>Area</th>
                                        <td><%# Eval("AreaSort") %></td>
                                    </tr>
                                    <tr>
                                        <th>Contact</th>
                                        <td><%# Eval("Contact") %></td>
                                    </tr>
                                    <tr>
                                        <th>Address</th>
                                        <td><%# Eval("Address") %></td>
                                    </tr>
                                    <tr>
                                        <th>Tel</th>
                                        <td><%# Eval("Tel") %></td>
                                    </tr>
                                    <tr>
                                        <th>Fax</th>
                                        <td><%# Eval("Fax") %></td>
                                    </tr>
                                    <tr>
                                        <th>Email</th>
                                        <td><%# Eval("Email") %></td>
                                    </tr>
                                    <tr>
                                        <th>Link</th>
                                        <td><%# Eval("Link") %></td>
                                    </tr>
                                    <tr>
                                        <th>Thumbnail</th>
                                        <td>
                                            <img src='<%# Eval("DealerImgPath") %>' alt="Thumbnail" width="400" height="250">
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <!------------------------------------------------ 資料展示結束 ------------------------------------------------>

                                <!------------------------------------------------ 資料編輯開始 ------------------------------------------------>
                                <asp:Panel ID="plEdit" runat="server">
                                    <tr>
                                        <th>
                                            <asp:Label runat="server" ID="Label1" Text='Name'></asp:Label>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="tbx_name" runat="server" Text='<%#Eval("Name") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Label runat="server" ID="Label5" Text='Country'></asp:Label>
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="list_country" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Country_list_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Label runat="server" ID="Label3" Text='Area'></asp:Label>
                                        </th>
                                        <td>
                                            <asp:DropDownList ID="list_area" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Area_list_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Label runat="server" ID="Label4" Text='Contact'></asp:Label>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="tbx_contact" runat="server" Text='<%#Eval("Contact") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Label runat="server" ID="Label6" Text='Address'></asp:Label>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="tbx_address" runat="server" Text='<%#Eval("Address") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Label runat="server" ID="Label7" Text='Tel'></asp:Label>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="tbx_tel" runat="server" Text='<%#Eval("Tel") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Label runat="server" ID="Label8" Text='Fax'></asp:Label>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="tbx_fax" runat="server" Text='<%#Eval("Fax") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Label runat="server" ID="Label9" Text='Email'></asp:Label>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="tbx_email" runat="server" Text='<%#Eval("Email") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:Label runat="server" ID="Label10" Text='Link'></asp:Label>
                                        </th>
                                        <td>
                                            <asp:TextBox ID="tbx_link" runat="server" Text='<%#Eval("Link") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <img src='<%# Eval("DealerImgPath") %>' alt="Thumbnail" width="200" height="150">
                                        </th>
                                        <td>
                                            <asp:FileUpload ID="update_thumbnail" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lbtupdate" CommandName="Update" CommandArgument='<%#Eval("id") %>' runat="server" CssClass="btn btn-primary">更新</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtCancel" CommandName="Cancel" CommandArgument='<%#Eval("id") %>' runat="server" CssClass="btn btn-warning">取消</asp:LinkButton>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                            <!------------------------------------------------ 資料編輯結束 ------------------------------------------------>



                            <%--<table class="table table-striped border">
                                <tr>
                                    <th>Name</th>
                                    <td><%# Eval("Name") %></td>
                                </tr>
                                <tr>
                                    <th>Country</th>
                                    <td><%# Eval("CountryId") %></td>
                                </tr>
                                <tr>
                                    <th>Area</th>
                                    <td><%# Eval("AreaId") %></td>
                                </tr>
                                <tr>
                                    <th>Contact</th>
                                    <td><%# Eval("Contact") %></td>
                                </tr>
                                <tr>
                                    <th>Address</th>
                                    <td><%# Eval("Address") %></td>
                                </tr>
                                <tr>
                                    <th>Tel</th>
                                    <td><%# Eval("Tel") %></td>
                                </tr>
                                <tr>
                                    <th>Fax</th>
                                    <td><%# Eval("Fax") %></td>
                                </tr>
                                <tr>
                                    <th>Email</th>
                                    <td><%# Eval("Email") %></td>
                                </tr>
                                <tr>
                                    <th>Link</th>
                                    <td><%# Eval("Link") %></td>
                                </tr>
                            </table>--%>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
