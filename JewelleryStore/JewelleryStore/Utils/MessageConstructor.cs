using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JewelleryStore;
using System.Windows;
using LiveCharts.Wpf;
using System.Net.Mime;

namespace EmailService
{
    public class MessageConstructor
    {
        string fromEmail = "youremail@gmail.com";
        string fromPassword = "yourapppassword";
        MailMessage message = new MailMessage();
        SmtpClient smtpClient;
        List<LinkedResource> images = new List<LinkedResource>();

        public MessageConstructor(string subject, string toEmail)
        {
            message.From = new MailAddress(fromEmail, "Aura Jewels");
            message.Subject = subject;
            message.To.Add(toEmail);
            message.IsBodyHtml = true;
            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true,
            };
        }
        
        public void SendMessage()
        {
            try
            {
                smtpClient.Send(message);
            }
            catch 
            {
                throw new ArgumentException("ErrorSendEmail");
            }
        }
        private void GenerateHeader(string name)
        {
            LinkedResource picture = new LinkedResource(Settings.projectPath + "/images/logotype.png", "image/png");
            picture.ContentId = "logo";
            images.Add(picture);
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    message.Body += "<html lang=\"ru\">\n" +
                        $"<body>\n" +
                        $"<div style=\"font-family: 'MS Sans Serif', Helvetica, sans-serif;\">\n" +
                        $"<img src=\"cid:logo\"/>\n" +
                        $"<h3>Здравствуйте, {name}!</h3>\n";

                    break;
                case Settings.Languages.EN:
                    message.Body += "<html lang=\"ru\">\n" +
                       $"<body>\n" +
                       $"<div style=\"font-family: 'MS Sans Serif', Helvetica, sans-serif;\">\n" +
                        $"<img src=\"cid:logo\"/>\n" +
                       $"<h3>Hello, {name}!</h3>\n";
                    break;
            }
        }
        public void GenerateOrderMessage(string name, Bill bill, int billId)
        {
            GenerateHeader(name);
            DatabaseUnit db = new DatabaseUnit();
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    message.Body += $"<p>Ваш заказ №{billId} оформлен.</p>\n" +
                            $"<h4>Информация о заказе</h4>\n";
                    break;
                case Settings.Languages.EN:
                    message.Body += $"<p>Your order №{billId} has been completed.</p>\n" +
                       $"<h4>Order information</h4>\n";
                    break;
            }
            message.Body += "<table>\n";
            int i = 0;
            foreach (OrderInfo oi in bill.OrderInfos)
            {   
                Product product = db.Products.Get(oi.ProductCode);
                string imageId = "image" + i;
                LinkedResource picture = new LinkedResource(Settings.projectPath + "/images/" + product.Pimage, "image/jpeg");
                picture.ContentId = imageId;
                images.Add(picture);
                
                string productName = "";
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        productName = db.Dictionary.Get(product.Pname).WordRus;
                        break;
                    case Settings.Languages.EN:
                        productName = db.Dictionary.Get(product.Pname).WordEn;
                        break;
                }
                message.Body += "<tr style=\"display: block; border-bottom: 1px solid gainsboro\">\n" +
                    $"<td><img src=\"cid:{imageId}\" width=\"100px\" height=\"100px\"></td>\n" +
                    "<td><div style=\"margin: 15px 0 0 10px; height: 100px;\">\n" +
                    $"<h4 style=\"margin: 0 0 30px 0\">{productName}</h4>\n" +
                    $"<p style=\"margin: 0\">${product.Price} x{oi.Quantity}</p>\n" +
                    "</div>\n" +
                    "</td>\n" +
                    "</tr>\n";
                i++;
            }
            message.Body += $"</table><h3>{Application.Current.Resources["Total"]}: ${bill.TotalPrice}</h3></div>\n";
            GenerateFooter();
        }
        public void GenerateRegistrationCodeMessage(string name, int code)
        {
            GenerateHeader(name);
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    message.Body += $"<p>Код для подтверждения электронной почты:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>\n" +
                        $"<p style=\"color: black;\">Спасибо за регистрацию в приложении нашего магазина!</p>\n";
                    break;
                case Settings.Languages.EN:
                    message.Body += $"<p>Email verification code:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>" +
                       $"<p style=\"color: black;\">Thank you for registering in our store app!</p>\n";
                    break;
            }
            GenerateFooter();
        }
        public void GenerateChangeEmailMessage(string name, int code)
        {
            GenerateHeader(name);
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    message.Body += $"<p>Код для подтверждения электронной почты:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>\n";
                    break;
                case Settings.Languages.EN:
                    message.Body += $"<p>Email verification code:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>";
                    break;
            }
            GenerateFooter();
        }
        public void GenerateRecoverCodeMessage(string name, int code)
        {
            GenerateHeader(name);
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    message.Body += $"<p>Код для восстановления аккаунта:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>\n";
                    break;
                case Settings.Languages.EN:
                    message.Body += $"<p>Account recovery code:</p>\n" +
                        $"<p style=\"font-size: 16pt\"><strong>{code}<strong/></p>";
                    break;
            }
            GenerateFooter();
        }
        private void GenerateFooter()
        {
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    message.Body += "<p style=\"color: gray\">Если вы не отправляли этот запрос, проигнорируйте данное письмо.</p>";
                    break;
                case Settings.Languages.EN:
                    message.Body += "<p style=\"color: gray\">If you did not make this request, please disregard this email.</p>";
                    break;
            }
                    AlternateView alternateView = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");
            message.AlternateViews.Add(alternateView);
            foreach (var img in images)
                alternateView.LinkedResources.Add(img);
        }
            
    }
}
