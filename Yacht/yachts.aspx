<%@ Page Title="" Language="C#" MasterPageFile="~/Yacht.Master" AutoEventWireup="true" CodeBehind="yachts.aspx.cs" Inherits="Yacht.yachts" %>

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
                    <li><a class="menu_yli01" href="../yachts.aspx">Interior</a></li>
                    <li><a class="menu_yli02" href="../yachts_2.aspx">Layout & deck pla</a>n</li>
                    <li><a class="menu_yli03" href="../yachts_3.aspx">Specification</a></li>
                    <li><a class="menu_yli03" href="../yachts_3.aspx">Video</a></li>
                </ul>
            </div>
            <!--次選單-->

            <div class="box1">
            </div>

            <div class="box3">
                <h4>PRINCIPAL DIMENSION</h4>
                <table class="table02">
                    <tr>
                        <td class="table02td01">
                            <table>
                                <tr>
                                    <th>L.O.A.</th>
                                    <td>72’-0”</td>
                                </tr>
                                <tr class="tr003">
                                    <th>L.W.L.</th>
                                    <td>60’-10”</td>
                                </tr>
                                <tr>
                                    <th>Beam</th>
                                    <td>20’-0”</td>
                                </tr>
                                <tr class="tr003">
                                    <th>Draft (Fin Keel)</th>
                                    <td>8’-6”</td>
                                </tr>
                                <tr>
                                    <th>Displacement</th>
                                    <td>96100lbs</td>
                                </tr>
                                <tr class="tr003">
                                    <th>Ballast (Fin Keel)</th>
                                    <td>24850lbs</td>
                                </tr>
                                <tr>
                                    <th>Sail Area (Main + 150% Triangle)<br />
                                        Main (9.0 oz)<br />
                                        Stays (9.0 oz)<br />
                                        No. 1 Genoa (7.2 oz)<br />
                                        Genoa (150%) (7.2 oz)<br />
                                        I :<br />
                                        J :<br />
                                        P :<br />
                                        E :</th>
                                    <td>2748 sq.
                                                <br />
                                        ft996 sq. ft<br />
                                        386 sq. ft<br />
                                        1167 sq. ft<br />
                                        1782 sq. ft<br />
                                        87’-0”<br />
                                        27’-1.75”<br />
                                        75’-4”<br />
                                        26’-0”<br />
                                    </td>
                                </tr>
                                <tr class="tr003">
                                    <th>D/L=191.47Ballast/Displacement</th>
                                    <td>28.10%</td>
                                </tr>
                                <tr>
                                    <th>Exterior Style, Interior Designer</th>
                                    <td>Andrew Winch</td>
                                </tr>
                                <tr class="tr003">
                                    <th>Naval Architect Designer</th>
                                    <td>Bill Dixon</td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <img src="images/ya01.jpg" alt="&quot;&quot;" width="278" height="345" /></td>
                    </tr>
                </table>
            </div>
            <p class="topbuttom">
                <img src="images/top.gif" alt="top" />
            </p>

            <!--下載開始-->
            <div class="downloads">
                <p>
                    <img src="images/downloads.gif" alt="&quot;&quot;" />
                </p>
                <ul>
                    <li><a href="#">Downloads 001</a></li>
                    <li><a href="#">Downloads 001</a></li>
                    <li><a href="#">Downloads 001</a></li>
                    <li><a href="#">Downloads 001</a></li>
                    <li><a href="#">Downloads 001</a></li>
                </ul>
            </div>
            <!--下載結束-->

            <!--------------------------------內容結束------------------------------------------------------>
        </div>
    </div>

    <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
