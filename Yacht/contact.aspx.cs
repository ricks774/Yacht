using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MailKit.Net.Smtp;
using System.Data.SqlClient;

namespace Yacht
{
    public partial class contact : System.Web.UI.Page
    {
        private YachtDBHelper helper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadModelList();
            }
        }

        private void LoadModelList()
        {
            string sql = "select * from Yachts";
            var reader = helper.SearchData(sql);
            while (reader.Read())
            {
                string typeStr = reader["yachtModel"].ToString();
                string isNewDesign = reader["isNewDesign"].ToString();
                string isNewBuilding = reader["isNewBuilding"].ToString();

                //加入遊艇型號下拉選單選項
                ListItem listItem = new ListItem();
                if (isNewDesign.Equals("True"))
                {
                    listItem.Text = $"{typeStr} (New Design)";
                    listItem.Value = $"{typeStr} (New Design)";
                    Yachts.Items.Add(listItem);
                }
                else if (isNewBuilding.Equals("True"))
                {
                    listItem.Text = $"{typeStr} (New Building)";
                    listItem.Value = $"{typeStr} (New Building)";
                    Yachts.Items.Add(listItem);
                }
                else
                {
                    listItem.Text = typeStr;
                    listItem.Value = typeStr;
                    Yachts.Items.Add(listItem);
                }
            }
        }

        protected void Btn_submit_Click(object sender, ImageClickEventArgs e)
        {
            if (String.IsNullOrEmpty(Recaptcha1.Response))
            {
                lb_bot.Visible = true;
                lb_bot.Text = "請驗證身分";
            }
            else
            {
                var result = Recaptcha1.Verify();
                if (result.Success)
                {
                    // 驗證成功時才寄出Email
                    SendGmail();
                }
                else
                {
                    lb_bot.Text = "Error(s): ";

                    foreach (var err in result.ErrorCodes)
                    {
                        lb_bot.Text = lb_bot.Text + err;
                    }
                }
            }
        }

        // ! 寄送email
        public void SendGmail()
        {
            //宣告使用 MimeMessage
            var message = new MimeMessage();
            //設定發信地址 ("發信人", "發信 email")
            message.From.Add(new MailboxAddress("TayanaYacht", "XXXXXXX@gmail.com"));
            //設定收信地址 ("收信人", "收信 email")
            message.To.Add(new MailboxAddress(Name.Text.Trim(), Email.Text.Trim()));
            //寄件副本email
            message.Cc.Add(new MailboxAddress("收信人名稱", "XXXXXXX@gmail.com"));
            //設定優先權
            //message.Priority = MessagePriority.Normal;
            //信件標題
            message.Subject = "TayanaYacht Auto Email";
            //建立 html 郵件格式
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
                "<h1>Thank you for contacting us!</h1>" +
                $"<h3>Name : {Name.Text.Trim()}</h3>" +
                $"<h3>Email : {Email.Text.Trim()}</h3>" +
                $"<h3>Phone : {Phone.Text.Trim()}</h3>" +
                $"<h3>Country : {Country.SelectedValue}</h3>" +
                $"<h3>Type : {Yachts.SelectedValue}</h3>" +
                $"<h3>Comments : </h3>" +
                $"<p>{Comments.Text.Trim()}</p>";
            //設定郵件內容
            message.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient())
            {
                //有開防毒時需設定 false 關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密)
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("ricks774@gmail.com", "puuqbfraxzaydhck");
                //發信
                client.Send(message);
                //結束連線
                client.Disconnect(true);
            }
        }
    }
}