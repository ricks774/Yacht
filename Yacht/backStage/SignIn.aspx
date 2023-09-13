<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="Dashboard.Sign_in" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="~/backStage/assets/img/apple-icon.png" />
    <link rel="icon" type="image/png" href="~/backStage/assets/img/favicon.png" />
    <!--     Fonts and icons     -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700|Noto+Sans:300,400,500,600,700,800|PT+Mono:300,400,500,600,700" rel="stylesheet" />
    <!-- Nucleo Icons -->
    <link href="~/backStage/assets/css/nucleo-icons.css" rel="stylesheet" />
    <link href="~/backStage/assets/css/nucleo-svg.css" rel="stylesheet" />
    <!-- Font Awesome Icons -->
    <script src="https://kit.fontawesome.com/349ee9c857.js" crossorigin="anonymous"></script>
    <link href="~/backStage/assets/css/nucleo-svg.css" rel="stylesheet" />
    <!-- CSS Files -->
    <link id="pagestyle" href="~/backStage/assets/css/corporate-ui-dashboard.css?v=1.0.0" rel="stylesheet" />
    <title>登入</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <main class="main-content  mt-0">
                <section>
                    <div class="page-header min-vh-100">
                        <div class="container">
                            <div class="row">
                                <div class="col-xl-4 col-md-6 d-flex flex-column mx-auto">
                                    <div class="card card-plain mt-3">
                                        <div class="card-header pb-0 text-left bg-transparent">
                                            <h3 class="font-weight-black text-dark display-6">歡迎回來</h3>
                                            <p class="mb-0">歡迎回來! 登入後繼續使用其他功能</p>
                                        </div>
                                        <div class="card-body">
                                            <label>帳號</label>
                                            <div class="mb-3">
                                                <asp:TextBox ID="acc_input" runat="server" CssClass="form-control" placeholder="輸入帳號" aria-label="Name" aria-describedby="name-addon"></asp:TextBox>
                                            </div>
                                            <label>密碼</label>
                                            <div class="mb-3">
                                                <asp:TextBox ID="pwd_input" runat="server" CssClass="form-control" placeholder="輸入密碼" aria-label="Password" aria-describedby="password-addon" TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div class="d-flex align-items-center">
                                                <div class="form-check form-check-info text-left mb-0">
                                                    <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault" />
                                                    <label class="font-weight-normal text-dark mb-0" for="flexCheckDefault">
                                                        14 天內記住我
                                                    </label>
                                                </div>
                                                <a href="javascript:;" class="text-xs font-weight-bold ms-auto">忘記密碼?</a>
                                            </div>
                                            <div class="text-center">
                                                <asp:Button ID="btnLogin" runat="server" Text="登入" CssClass="btn btn-dark w-100 mt-4 mb-3" OnClick="Btn_login" />
                                            </div>
                                        </div>
                                        <div class="card-footer text-center pt-0 px-lg-2 px-1">
                                            <p class="mb-4 text-xs mx-auto">
                                                還沒有帳號?
                                            <a href="javascript:;" class="text-dark font-weight-bold">註冊</a>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="position-absolute w-40 top-0 end-0 h-100 d-md-block d-none">
                                        <div class="oblique-image position-absolute fixed-top ms-auto h-100 z-index-0 bg-cover ms-n8" style="background-image: url('../backStage/assets/img/image-sign-in.jpg')">
                                            <div class="blur mt-12 p-4 text-center border border-white border-radius-md position-absolute fixed-bottom m-4">
                                                <h2 class="mt-3 text-dark font-weight-bold">我的假髮 會撕裂你</h2>
                                                <h6 class="text-dark text-sm mt-5">Copyright © 2022 腦子來 腦子從四面八方來.</h6>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </main>
            <!--   Core JS Files   -->
            <script src="~/backStage/assets/js/core/popper.min.js"></script>
            <script src="~/backStage/assets/js/core/bootstrap.min.js"></script>
            <script src="~/backStage/assets/js/plugins/perfect-scrollbar.min.js"></script>
            <script src="~/backStage/assets/js/plugins/smooth-scrollbar.min.js"></script>
            <!-- Github buttons -->
            <script src="https://buttons.github.io/buttons.js"></script>
            <!-- Control Center for Corporate UI Dashboard: parallax effects, scripts for the example pages etc -->
            <script src="~/backStage/assets/js/corporate-ui-dashboard.min.js?v=1.0.0"></script>
        </div>
    </form>
</body>
</html>