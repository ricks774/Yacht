<%@ Page Title="" Language="C#" MasterPageFile="~/Yacht.Master" AutoEventWireup="true" CodeBehind="dealers.aspx.cs" Inherits="Yacht.dealers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/DEALERS.jpg" alt="&quot;&quot;" width="967" height="371" />
    </div>
    <!--遮罩結束-->

    <!--------------------------------換圖開始---------------------------------------------------->

    <div class="banner_cus">
        <ul>
            <li>
                <img src="images/DEALERS.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->

    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>DEALERS</span></p>
                <ul>
<%--                    <li><a href="#">Unite States</a></li>
                    <li><a href="#">Europe</a></li>
                    <li><a href="#">Asia</a></li>--%>
                    <asp:Literal ID="LeftMenu" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>

        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="../index.aspx">Home</a> >> <a href="../dealers.aspx">Dealers </a>>> <a href="#"><span class="on1" id="LabLink" runat="server">Unite States</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title">
                    <span id="LitTitle" runat="server">Unite States</span></div>
                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box2_list">
                    <ul>
                        <asp:Literal ID="DealersList" runat="server"></asp:Literal>
                    </ul>
                    <div class="pagenumber"></div>
                    <%--<div class="pagenumber">| <span>1</span> | <a href="#">2</a> | <a href="#">3</a> | <a href="#">4</a> | <a href="#">5</a> |  <a href="#">Next</a>  <a href="#">LastPage</a></div>--%>
                    <%--<div class="pagenumber1">Items：<span>89</span>  |  Pages：<span>1/9</span></div>--%>
                </div>
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>