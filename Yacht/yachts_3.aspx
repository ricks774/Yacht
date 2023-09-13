<%@ Page Title="" Language="C#" MasterPageFile="~/Yacht.Master" AutoEventWireup="true" CodeBehind="yachts_3.aspx.cs" Inherits="Yacht.yachts_3" %>

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

                <div class="box5">
                    <h4>DETAIL SPECIFICATION</h4>

                    <p>HULL STRUCTURE & DECKS</p>
                    <ul>
                        <li>Yanmar 4LHA-HTP 160HP (or equal)</li>
                        <li>White formica counters in hgalley. Teak veneer ctt</li>
                        <li>White formica counters in hgalley. Teak veneer c</li>
                        <li>White formica counters in hgalley. Teak veneer c</li>
                        <li>WTeak veneer ctte table 0005</li>
                        <li>WTeak veneer ctte table 0005</li>
                    </ul>

                    <p>HULL STRUCTURE & DECKS</p>
                    <ul>
                        <li>Yanmar 4LHA-HTP 160HP (or equal)</li>
                        <li>White formica counters in hgalley. Teak veneer ctt</li>
                        <li>White formica counters in hgalley. Teak veneer c</li>
                        <li>White formica counters in hgalley. Teak veneer c</li>
                        <li>WTeak veneer ctte table 0005</li>
                        <li>WTeak veneer ctte table 0005</li>
                    </ul>
                </div>
                <p class="topbuttom">
                    <img src="images/top.gif" alt="top" />
                </p>
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>