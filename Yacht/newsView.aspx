<%@ Page Title="" Language="C#" MasterPageFile="~/Yacht.Master" AutoEventWireup="true" CodeBehind="newsView.aspx.cs" Inherits="Yacht.news1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner02_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->
    <!--------------------------------換圖開始---------------------------------------------------->

    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->

    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">

            <div class="left1">
                <p><span>NEWS</span></p>
                <ul>
                    <li><a href="#">News & Events</a></li>
                </ul>
            </div>
        </div>

        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="index.aspx">Home</a> >> <a href="news.aspx">News </a>>> <a href="#"><span class="on1">News & Events</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>News & Events</span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box2_list">
                    <div class="row">
                        <div class="col-12">
                            <h6 runat="server" id="NewsTitle" ></h6>
                            <asp:Literal ID="ShowNewsContent" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>
        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
