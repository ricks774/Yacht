<%@ Page Title="" Language="C#" MasterPageFile="~/Yacht.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Yacht.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner00_masks.png" alt="&quot;&quot;" />
    </div>
    <!--遮罩結束-->
    <!--------------------------------換圖開始---------------------------------------------------->
    <div id="abgne-block-20110111">
        <div class="bd">
            <div class="banner" style="border-radius: 5px; height: 424px; width: 978px">
                <ul>
                    <asp:Literal ID="Lt_Banner" runat="server"></asp:Literal>
                </ul>
                <!--小圖開始-->
                <div class="bannerimg title" style="display: none">
                    <ul>
                        <asp:Literal ID="Lt_BannerNum" runat="server"></asp:Literal>
                    </ul>
                </div>
                <!--小圖結束-->
            </div>
        </div>
    </div>
    <!--------------------------------換圖結束---------------------------------------------------->


    <!--------------------------------最新消息---------------------------------------------------->
    <div class="news">
        <div class="newstitle">
            <p class="newstitlep1">
                <img src="images/news.gif" alt="news" />
            </p>
            <p class="newstitlep2"><a href="newsView.aspx">More>></a></p>
        </div>

        <ul>
            <!--TOP第一則最新消息-->
            <li>
                <div class="news01">
                    <!--TOP標籤-->
                    <div class="newstop">
                        <asp:Image ID="ImgIsTop1" runat="server" AlternateText="&qout;&quot;" Visible="false" ImageUrl="images/new_top01.png" />
                    </div>
                    <!--TOP標籤結束-->
                    <div class="news02p1">
                        <p class="news02p1img">
                            <asp:Literal ID="Lt_NewsImg1" runat="server"></asp:Literal>
                        </p>
                    </div>
                    <p class="news02p2">
                        <span>
                            <asp:Label ID="Lb_NewsDate1" runat="server" ForeColor="#02A5B8"></asp:Label>
                        </span>
                        <span>
                            <asp:HyperLink ID="HL_News1" runat="server"></asp:HyperLink>
                        </span>
                    </p>
                </div>
            </li>
            <!--TOP第一則最新消息結束-->

            <!--TOP第二則最新消息-->
            <li>
                <div class="news01">
                    <!--TOP標籤-->
                    <div class="newstop">
                        <asp:Image ID="ImgIsTop2" runat="server" AlternateText="&qout;&quot;" Visible="false" ImageUrl="images/new_top01.png" />
                    </div>
                    <!--TOP標籤結束-->
                    <div class="news02p1">
                        <p class="news02p1img">
                            <asp:Literal ID="Lt_NewsImg2" runat="server"></asp:Literal>
                        </p>
                    </div>
                    <p class="news02p2">
                        <span>
                            <asp:Label ID="Lb_NewsDate2" runat="server" ForeColor="#02A5B8"></asp:Label>
                        </span>
                        <span>
                            <asp:HyperLink ID="HL_News2" runat="server"></asp:HyperLink>
                        </span>
                    </p>
                </div>
            </li>
            <!--TOP第二則最新消息結束-->

            <!--TOP第三則最新消息-->
            <li>
                <div class="news01">
                    <!--TOP標籤-->
                    <div class="newstop">
                        <asp:Image ID="ImgIsTop3" runat="server" AlternateText="&qout;&quot;" Visible="false" ImageUrl="images/new_top01.png" />
                    </div>
                    <!--TOP標籤結束-->
                    <div class="news02p1">
                        <p class="news02p1img">
                            <asp:Literal ID="Lt_NewsImg3" runat="server"></asp:Literal>
                        </p>
                    </div>
                    <p class="news02p2">
                        <span>
                            <asp:Label ID="Lb_NewsDate3" runat="server" ForeColor="#02A5B8"></asp:Label>
                        </span>
                        <span>
                            <asp:HyperLink ID="HL_News3" runat="server"></asp:HyperLink>
                        </span>
                    </p>
                </div>
            </li>
            <!--TOP第三則最新消息結束-->




        </ul>
    </div>
    <!--------------------------------最新消息結束---------------------------------------------------->
</asp:Content>
