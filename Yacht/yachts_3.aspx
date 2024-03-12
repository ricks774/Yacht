<%@ Page Title="" Language="C#" MasterPageFile="~/Yacht.Master" AutoEventWireup="true" CodeBehind="Yachts_3.aspx.cs" Inherits="Yacht.yachts_3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner01_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->

    <!--------------------------------小圖開始---------------------------------------------------->
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

            <div class="box5">
                <h4>DETAIL SPECIFICATION</h4>
                <asp:Literal ID="Lr_LoadSpecification" runat="server"></asp:Literal>
            </div>
            <p class="topbuttom">
                <%--<img src="images/top.gif" alt="top" />--%>
            </p>
            <!--------------------------------內容結束------------------------------------------------------>
        </div>
    </div>

    <!--------------------------------右邊選單結束---------------------------------------------------->
</asp:Content>
