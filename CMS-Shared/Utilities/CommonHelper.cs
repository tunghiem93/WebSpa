using CMS_Common;
using CMS_DataModel.Models;
using CMS_DTO.CMSContact;
using CMS_DTO.CMSOrder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.Utilities
{
    public class CommonHelper
    {
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob00900X";
        public static string Encrypt(string text)
        {
            var ret = "";
            try
            {
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    using (var tdes = new TripleDESCryptoServiceProvider())
                    {
                        tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                        tdes.Mode = CipherMode.ECB;
                        tdes.Padding = PaddingMode.PKCS7;

                        using (var transform = tdes.CreateEncryptor())
                        {
                            byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                            byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                            ret = Convert.ToBase64String(bytes, 0, bytes.Length);
                        }
                    }
                }
            }
            catch(Exception ex) { };
            return ret;
            
        }

        public static string Decrypt(string cipher)
        {
            var ret = "";
            try
            {
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    using (var tdes = new TripleDESCryptoServiceProvider())
                    {
                        tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                        tdes.Mode = CipherMode.ECB;
                        tdes.Padding = PaddingMode.PKCS7;

                        using (var transform = tdes.CreateDecryptor())
                        {
                            byte[] cipherBytes = Convert.FromBase64String(cipher);
                            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                            ret = UTF8Encoding.UTF8.GetString(bytes);
                        }
                    }
                }
            }
            catch (Exception ex) { };
            return ret;
        }

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        public static bool SendContentMail(string EmailTo, string Content, string Name, string Subject, string imgUrl = "", string attachment = "")
        {
            bool isOk = false;
            try
            {
               
                    string email = ConfigurationManager.AppSettings["LamodeMail"];
                    string passWord = ConfigurationManager.AppSettings["LamodePass"];
                    string smtpServer = "smtp.gmail.com";
                    if (email != "" && passWord != "")
                    {
                        MailMessage mail = new MailMessage(email, EmailTo);
                        mail.Subject = Subject;
                        mail.Body = Content;
                        mail.IsBodyHtml = true;
                        if (!string.IsNullOrEmpty(imgUrl))
                            mail.Body = string.Format("<div><img src='{0}'/><div>", imgUrl);

                        SmtpClient client = new SmtpClient();
                        client.Port = 587;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(email, passWord);
                        client.Host = smtpServer;
                        client.Timeout = 10000;
                        client.EnableSsl = true;
                        if (!string.IsNullOrEmpty(attachment))
                            mail.Attachments.Add(new System.Net.Mail.Attachment(attachment));
                        client.Send(mail);
                        isOk = true;
                    }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Send Mail Error", ex);
            }
            return isOk;
        }

        public static List<string> GenerateCode(int number, List<string> listCodeInDB, int length = 10)
        {
            List<string> listCode = new List<string>();
            Random random = new Random();
            for (int i = 0; i < number; i++)
            {
                string code = "";
                do
                {
                    code = new string(Enumerable.Repeat(Commons.PasswordChar, length).Select(s => s[random.Next(s.Length)]).ToArray());
                } while (listCodeInDB.Contains(code) || listCode.Contains(code));
                listCode.Add(code);
            }
            return listCode;
        }

        public static string RandomNumberOrder()
        {
            Random R = new Random();
            return "NO_" +((long)R.Next(0, 100000) * (long)R.Next(0, 100000)).ToString().PadLeft(10, '0');
        }

        ///<summary>
        ////* 1000~1400 => 1000 || 1500~1900 => 1500 */
        ///</summary>
        public static double RoundingOption4(double number, double x = 0.01)
        {
            int xX = (int)(number * x);
            int p1 = xX % 10;
            xX -= p1;
            switch (p1)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4: p1 = 0; break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9: p1 = 5; break;
            }
            xX += p1;

            return (double)xX / x;
        }

        public static string GenerateOrderNo(string _storeID, byte mode, byte orderType)
        {
            string no = string.Empty;
            try
            {
                using (var _db = new CMS_Context())
                {
                    int startNo = 1;
                    string prefix = "OR";
                    if (orderType == (byte)Commons.EOrderType.Expense)
                        prefix = "EX";
                    prefix += DateTime.Now.ToString("yyyyMMdd") + "-";
                    
                    int nextNum = startNo;
                    int currentNum = 0;
                    string currentOrderNo = _db.CMS_Order.Where(o => o.StoreID == _storeID && o.OrderNo.Contains(prefix) && !string.IsNullOrEmpty(o.OrderNo)).OrderByDescending(o => o.OrderNo).Select(o => o.OrderNo).FirstOrDefault();
                    if (!string.IsNullOrEmpty(currentOrderNo))
                    {
                        currentOrderNo = currentOrderNo.Replace(prefix, "");
                        currentNum = int.Parse(currentOrderNo);
                        if (currentNum >= startNo)
                            nextNum = currentNum + 1;
                    }

                    if (nextNum.ToString().Length > 2)
                        no = prefix + nextNum.ToString();
                    else
                        no = prefix + CreateStringLengthDigit(nextNum, 3);
                }
            }
            catch { }
            return no;
        }

        public static string GenerateReceiptNo(string _storeID, byte mode, byte orderType)
        {
            string no = string.Empty;
            try
            {
                using (var _db = new CMS_Context())
                {
                    int startNo = 1;
                    string prefix = "RC";
                    if (orderType == (byte)Commons.EOrderType.Expense)
                        prefix = "RCEX";
                    prefix += DateTime.Now.ToString("yyyyMMdd") + "-";

                    int nextNum = startNo;
                    int currentNum = 0;
                    string currentReceiptNo = _db.CMS_Order.Where(o => o.StoreID == _storeID && o.ReceiptNo.Contains(prefix) && !string.IsNullOrEmpty(o.ReceiptNo)).OrderByDescending(o => o.ReceiptNo).Select(o => o.ReceiptNo).FirstOrDefault();
                    if (!string.IsNullOrEmpty(currentReceiptNo))
                    {
                        currentReceiptNo = currentReceiptNo.Replace(prefix, "");
                        currentNum = int.Parse(currentReceiptNo);
                        if (currentNum >= startNo)
                            nextNum = currentNum + 1;
                    }

                    if (nextNum.ToString().Length > 2)
                        no = prefix + nextNum.ToString();
                    else
                        no = prefix + CreateStringLengthDigit(nextNum, 3);
                }
            }
            catch { }
            return no;
        }


        private static string CreateStringLengthDigit(int number, int length)
        {
            string result = number.ToString();
            while (result.Length < length)
            {
                result = "0" + result;
            }
            return result;
        }

        public static bool ContactAdmin(CMS_ContactModels _ctInfo )
        {
            var ret = false;
            try
            {
                string lamodMail = ConfigurationManager.AppSettings["LamodeMail"];

                /* send mail to lamode */
                SendContentMail(lamodMail, _ctInfo.GetContactInfo(), _ctInfo.Name, "[Customer Contact]" + _ctInfo.Subject);

                /* send mail to customer */
                if (!string.IsNullOrEmpty(_ctInfo.Email))
                    SendContentMail(_ctInfo.Email, _ctInfo.GetContactInfo(), _ctInfo.Name, "[Lamode Home]" + " thanks for contacting us "  + _ctInfo.Subject);

                ret = true;
            }
            catch(Exception ex) { }
            return ret;
        }

        public static string CreateBodyMail(CMS_OrderModels model)
        {
            string body = string.Empty;
            try
            {
                body += "<div class='payment-order clearfix'>";
                body += "<h3>Mã đơn hàng của bạn: <b>#" + model.OrderNo + "</b></h3>";
                body += "<p><b>Ngày đặt:</b> <i>" + model.CreatedDate.ToString("dd/MM/yyyy hh:mm tt") + "</i></p>";
                body += "<p><b>Tên khách hàng:</b> <i>" + model.CustomerName + "</i></p>";
                body += "<p><b>Số điện thoại:</b> <i>" + model.Phone + "</i></p>";
                body += "<p><b>Email:</b> <i>" + model.Email + "</i></p>";
                body += "<p><b>Địa chỉ:</b> <i>" + model.Address + "</i></p>";
                body += "<p><b>Phương thức thanh toán:</b> <i></i></p>";
                body += "<h1 class='page-heading' style= 'font-size: 16px;color: #958457;margin-bottom: 5px;'>Thông tin đơn hàng</h1>";
                body += "<table class='table' style='width: 100%;margin-bottom: 20px;max-width: 100%;border-collapse: collapse;border-spacing: 0;'>";
                body += "<thead style='background-color: #a0ca50 !important;color:#FFF'>";
                body += "<tr>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>STT</th>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>SẢN PHẨM</th>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>GIÁ</th>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>SỐ LƯỢNG</th>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>THÀNH TIỀN</th>";
                body += "</tr>";
                body += "</thead>";
                body += " <tbody>";
                if (model.Items != null && model.Items.Any())
                {
                    var Index = 1;
                    foreach (var item in model.Items)
                    {
                        body += "<tr>";
                        body += " <td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>" + Index + "</td>";
                        body += "<td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>";
                        body += "<span>" + item.ProductName + "</span>";
                        body += " <p class='note'></p>";
                        body += "</td>";
                        body += "<td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>" + item.Price.ToString("#,0") + " đ</td>";
                        body += "<td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>" + item.Quantity + "</td>";
                        body += "<td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>" + item.TotalPrice.ToString("#,0") + " đ</td>";
                        body += "</tr>";
                        Index = Index + 1;
                    }
                }
                body += "</tbody>";
                body += "<tfoot>";
                body += "<tr>";
                body += "<td class='label-payment' style='text-transform: uppercase;padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align: left;background-color: #a0ca50 !important;color:#FFF'><b>Thành tiền (Chưa VAT)</b></td>";
                body += "<td colspan='4' class='total-payment text-center' style='color: #ff0000;border-top: 1px solid #e7ecf1;text-align:center'>" + model.SubTotal.Value.ToString("#,0") + " đ</td>";
                body += "</tr>";
                body += "<tr>";
                body += "<td class='label-payment' style='text-transform: uppercase;padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align: left;background-color: #a0ca50 !important;color:#FFF'><b>Vận chuyển</b></td>";
                body += "<td colspan='4' class='total-payment text-center' style='color: #ff0000;border-top: 1px solid #e7ecf1;text-align:center'>Phí vận chuyển sẽ được ước tính khi bạn nhập địa chỉ.</td>";
                body += "</tr>";
                body += "<tr>";
                body += "<td class='text-right label-payment' style='text-transform: uppercase;padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align: left;background-color: #a0ca50 !important;color:#FFF'><b>Thành tiền</b></td>";
                body += "<td colspan='4' class='total-payment' style='color: #ff0000;border-top: 1px solid #e7ecf1;text-align:center'>" + model.TotalBill.Value.ToString("#,0") + " đ</td>";
                body += "</tr>";
                body += "</tfoot>";
                body += "</table>";
                body += "<br clear=\"all\">";
                body += "</div>";
            }
            catch (Exception ex) { }

            return body;
        }
    }
}
