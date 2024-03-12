<%@ Page Title="" Language="C#" MasterPageFile="~/Yacht.Master" AutoEventWireup="true" CodeBehind="company.aspx.cs" Inherits="Yacht.company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/company.jpg" alt="&quot;&quot;" width="967" height="371" />
    </div>
    <!--遮罩結束-->

    <!--------------------------------換圖開始---------------------------------------------------->

    <div class="banner">
        <ul>
            <li>
                <img src="images/company.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->

    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">

            <div class="left1">
                <p><span>COMPANY </span></p>
                <ul>
                    <li><a href="company.aspx">About Us</a></li>
                    <li><a href="company1.aspx">Certificat</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="index.aspx">Home</a> >> <a href="company.aspx">Company  </a>>> <a href="company.aspx"><span class="on1">About Us</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>About Us</span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box3">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </div>
                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>