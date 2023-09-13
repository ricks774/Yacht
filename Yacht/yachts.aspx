﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Yacht.Master" AutoEventWireup="true" CodeBehind="yachts.aspx.cs" Inherits="Yacht.yachts" %>

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
                        <li>
                            <a href="images/test1.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <%--                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>
                        <li>
                            <a href="images/test002.jpg">
                                <img src="images/pit003.jpg">
                            </a>
                        </li>--%>
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
                <p><span>YACHTS?</span></p>
                <ul>
                    <li><a href="#">Dynasty 72</a></li>
                    <li><a href="#">Tayana 64</a></li>
                    <li><a href="#">Tayana 58</a></li>
                    <li><a href="#">Tayana 55</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="#">Home</a> >> <a href="#">Yachts</a> >> <a href="#"><span class="on1">Dynasty 72</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>Dynasty 72</span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <!--次選單-->
                <div class="menu_y">
                    <ul>
                        <li class="menu_y00">YACHTS</li>
                        <li><a class="menu_yli01" href="../yachts.aspx">Interior</a></li>
                        <li><a class="menu_yli02" href="../yachts_2.aspx">Layout & deck pla</a>n</li>
                        <li><a class="menu_yli03" href="../yachts_3.aspx">Specification</a></li>
                    </ul>
                </div>
                <!--次選單-->

                <div class="box1">
                    With the world renowned pedigree combination of Ta Yang Yacht Builders, Andrew Winch Designs, and Bill Dixon Naval Architects, the Tayana Dynasty 72 ranks as an exceptional high performance cruising yacht. Space abounds in the Dynasty 72, with two spacious cockpits and a sunbathing area on the deck. The central cockpit houses twin steering positions with outdoor dining for eight and access forward into the pilothouse. All control and command equipment is readily available for minimal crew handling. The aft cockpit is accessed from the large owner's cabin and provides a pleasant seating area which opens out through a drop-down transom to the bathing platform. The Dynasty is very much a semi-custom yacht. The interior styling, furniture, and fabrics will reflect the owner's ideals and will blend with an extensive range of high quality fittings and equipment. The technical specification of the yacht will be to a very high standard. Three interior styles have been developed by Andrew Winch. Two owner versions each have four staterooms but different positions for the galley; a charter version has six double cabins with en suite heads. All versions have separate crew quarters, and all versions have the magnificent split level pilot house connecting the forward and aft lower accommodation levels. Custom interiors are available to fit the needs of you and your crew. Ta Yang has been constructing first class yachts for many years. The reputation of Chinese craftsmen over thousands of years is renowned, and it is the combination of their skills with modern design and naval architecture that has created the Tayana Dynasty 72.<br />
                    <br />
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