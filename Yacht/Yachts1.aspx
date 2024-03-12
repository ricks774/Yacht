<%@ Page Title="" Language="C#" MasterPageFile="~/Yacht.Master" AutoEventWireup="true" CodeBehind="Yachts1.aspx.cs" Inherits="Yacht.Yachts1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner01_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->
    <!--小圖開始-->
    <div class="banner">
        <div id="gallery" class="ad-gallery">
            <div class="ad-image-wrapper">
            </div>
            <div class="ad-controls" style="display: none">
            </div>
            <div class="ad-nav">
                <div class="ad-thumbs">
                    <ul class="ad-thumb-list">
                        <asp:Repeater ID="Rp_Img" runat="server">
                            <ItemTemplate>
                                <a href='<%# Eval("ImgPath") %>'>
                                    <img src='<%# Eval("ImgPath") %>' class="image0" height="59px" />
                                </a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <!--------------------------------小圖結束---------------------------------------------------->

    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>YACHTS</span></p>
                <ul>
                    <asp:Literal ID="Lr_LeftMenuHtml" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="#">Home</a> >> <a href="#">Yachts</a> >> <a href="#"><span id="Lb_RighTopLink" class="on1" runat="server">Dynasty 72</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title">
                    <span id="Lb_YachtsTitle" runat="server">Unite States</span>
                </div>
            </div>

            <!--------------------------------內容開始---------------------------------------------------->
            <!--次選單-->
            <div class="menu_y">
                <ul>
                    <li class="menu_y00">YACHTS</li>
                    <asp:Literal ID="Lr_Interior" runat="server"></asp:Literal>
                    <asp:Literal ID="Lr_Layout" runat="server"></asp:Literal>
                    <asp:Literal ID="Lr_Specification" runat="server"></asp:Literal>
                    <asp:Literal ID="Lr_Video" runat="server"></asp:Literal>
                </ul>
            </div>
            <!--次選單-->

            <!-- 內容開始 -->
            <div class="box1 mb-3">
                <asp:Literal ID="Lr_OverViewContent" runat="server"></asp:Literal>
            </div>
            <div class="box3 mt-3">
                <br />
                <h4 id="h4_DimensionTitle" runat="server">PRINCIPAL DIMENSION</h4>
                <asp:Literal ID="Lr_OverViewDimension" runat="server"></asp:Literal>
            </div>
            <!-- 內容結束 -->
            <p class="topbuttom">
                <%--<img src="images/top.gif" alt="top" />--%>
            </p>

            <!--下載開始-->
            <div id="Div_downloads" class="downloads" runat="server" visible="false">
                <p>
                    <img src="images/downloads.gif" alt="&quot;&quot;" />
                </p>
                <ul>
                    <li>
                        <asp:HyperLink ID="Hl_DownloadFile" runat="server"></asp:HyperLink>
                    </li>
                </ul>
            </div>
            <!--下載結束-->

            <!--------------------------------內容結束------------------------------------------------------>
        </div>
    </div>
    <!--------------------------------右邊選單結束---------------------------------------------------->
</asp:Content>
