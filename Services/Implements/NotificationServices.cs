using System.Net.Mail;
using System.Net;
using CorePush.Apple;
using CorePush.Google;
using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repositories.Intefaces;
using Services.Interfaces;
using static Entities.ResponseObject.GoogleNotification;
using Org.BouncyCastle.Bcpg;

namespace Services.Implements
{
    public class NotificationServices : INotificationServices
    {
        private readonly IOptions<FcmNotificationSetting> _settings;
        private readonly IRepositoryManager _repositoryManager;

        public NotificationServices(IOptions<FcmNotificationSetting> settings, IRepositoryManager repositoryManager)
        {
            _settings = settings;
            _repositoryManager = repositoryManager;
        }

        public async Task<bool> ReadedAll(ReadedNoti info)
        {
            foreach (var id in info.NotiIds)
            {
                var noti = _repositoryManager.Notification.FindByCondition(x => x.Id == id, false)
                    .FirstOrDefault();

                noti.IsRead = true;
                _repositoryManager.Notification.Update(noti);
            }
            await _repositoryManager.SaveAsync();
            return true;
        }

        public async Task<NotiResponseModel> SendNotification(NotificationModel notificationModel)
        {
            var res = new NotiResponseModel();

            try
            {
                var payloadData = new DataPayload
                {
                    Body = notificationModel.Body,
                    Title = notificationModel.Title,
                };

                var notification = new GoogleNotification
                {
                    Data = payloadData,
                    Notification = payloadData
                };

                if (notificationModel.IsAndroiodDevice)
                {
                    var settings = new FcmSettings
                    {
                        SenderId = _settings.Value.SenderId,
                        ServerKey = _settings.Value.ServerKey,
                    };
                    var httpClient = new HttpClient();
                    var authorizationKey = $"key={settings.ServerKey}";
                    var deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                        .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmResponse.IsSuccess())
                    {
                        res.IsSuccess = true;
                        res.Message = "Notification sent successfully";
                    }
                    else
                    {
                        res.IsSuccess = false;
                        res.Message = fcmResponse.Results[0].Error;
                    }
                }
                else
                {
                    //Pending
                }
                return res;
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Some thing went wrong";
                return res;
            }
        }

        public async Task<NotiResponseModel> SendNotification(int userId, string title, string message, NotificationType type, int referenceInfo)
        {
            var user = await _repositoryManager.User.FindByCondition(x => x.Id == userId, false).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var savedNoti = new Notification
            {
                Content = message,
                IsRead = false,
                NotiDate = DateTime.UtcNow.AddHours(7),
                Title = title,
                UserId = userId,
                About = (int)type,
                ReferenceInfo = referenceInfo
            };

            _repositoryManager.Notification.Create(savedNoti);
            await _repositoryManager.SaveAsync();

            //if(user.LogingingDevice == null)
            //{
            //    return new NotiResponseModel 
            //    {
            //        IsSuccess = true,
            //        Message = "User don't have device to send noti"
            //    };
            //}

            //var res = await SendNotification(new NotificationModel
            //{
            //    Body = message,
            //    DeviceId = user.LogingingDevice,
            //    IsAndroiodDevice = user.IsAndroidDevice,
            //    Title = title
            //});

            //return res;

            return new NotiResponseModel
            {
                IsSuccess = true,
                Message = "Send Success"
            };
        }

        public async Task<NotiResponseModel> SendNotification(List<int> userIds, string title, string message, NotificationType type, int referenceInfo)
        {
            foreach (var userId in userIds)
            {
                var user = await _repositoryManager.User.FindByCondition(x => x.Id == userId, false).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var savedNoti = new Notification
                {
                    Content = message,
                    IsRead = false,
                    NotiDate = DateTime.UtcNow.AddHours(7),
                    Title = title,
                    UserId = userId,
                    About = (int)type,
                    ReferenceInfo = referenceInfo
                };

                _repositoryManager.Notification.Create(savedNoti);

                //if(user.LogingingDevice == null)
                //{
                //    return new NotiResponseModel 
                //    {
                //        IsSuccess = true,
                //        Message = "User don't have device to send noti"
                //    };
                //}

                //var res = await SendNotification(new NotificationModel
                //{
                //    Body = message,
                //    DeviceId = user.LogingingDevice,
                //    IsAndroiodDevice = user.IsAndroidDevice,
                //    Title = title
                //});

                //return res;
            }
            await _repositoryManager.SaveAsync();

            return new NotiResponseModel
            {
                IsSuccess = true,
                Message = "Send Success"
            };
        }



        public bool TransactionDetailsEmail(Transaction transaction)
        {
            try
            {
                var user = _repositoryManager.User.FindByCondition(x => x.Id == transaction.UserId, false).FirstOrDefault();
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "nguyenhangnhathuy@gmail.com";
                string smtpPassword = "yfsu iswn xeqj knhb";

                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    string fromEmail = "nguyenhangnhathuy@gmail.com";
                    string subject = "Transaction Details";

                    // Construct the email body with the Transaction details
                    string body = GetTransactionDetailsHtml(transaction);

                    using (MailMessage mailMessage = new MailMessage(fromEmail, user.Email, subject, body))
                    {
                        mailMessage.IsBodyHtml = true;

                        // Send the email
                        smtpClient.Send(mailMessage);

                        Console.WriteLine("Email sent successfully!");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        public string GetTransactionDetailsHtml(Transaction transaction)
        {
            // HTML template for the email body
            string htmlTemplate = @"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Transaction Details</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                        }
                        .email-container {
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            border: 1px solid #ccc;
                            border-radius: 5px;
                        }
                        h2 {
                            color: #3498db;
                        }
                        table {
                            width: 100%;
                            border-collapse: collapse;
                            margin-top: 20px;
                        }
                        th, td {
                            padding: 10px;
                            border: 1px solid #ddd;
                            text-align: left;
                        }
                        th {
                            background-color: #f2f2f2;
                        }
                    </style>
                </head>
                <body>
                <h4>Xin chào {Fullname} !</h4>
                    <p>Chúng xin chân thành cảm ơn bạn vì đã sử dụng dịch vụ của chúng tôi</p>
                     <p>Chúng tôi xin gởi đến bạn chi tiết giao dịch có Id là :{TransactionId} </p>
                    <div class='email-container'>
                        <h2>Chi tiết giao dịch</h2>
                        <table>                                                                                
                            <tr>
                                <td>Thời gian giao dịch</td>
                                <td>{TransactionTime}</td>
                            </tr>
                            <tr>
                                <td>Loại giao dịch</td>
                                <td>{TransactionMethod}</td>
                            </tr>                        
                            <tr>
                                <td>Số tiền</td>
                                <td>{TransactionAmount}</td>
                            </tr>
                            <tr>
                                <td>Trạnng thái</td>
                                <td>{TransactionStatus}</td>
                            </tr>
                        </table>
                    </div>
                </body>
                </html>";

            var transactionStatus = "";
            switch (transaction.Status)
            {
                case (int)TransactionStatus.Booked:
                    transactionStatus = "Đặt sân thành !";
                    break;
                case (int)TransactionStatus.Canceled:
                    transactionStatus = "Đặt sân thất bại !";
                    break;
            }

            // Replace placeholders with actual values
            var user = _repositoryManager.User.FindByCondition(x => x.Id == transaction.UserId, false).FirstOrDefault();
            htmlTemplate = htmlTemplate.Replace("{TransactionId}", transaction.Id.ToString());
            htmlTemplate = htmlTemplate.Replace("{TransactionTime}", transaction.TimeTrans.ToString());
            htmlTemplate = htmlTemplate.Replace("{TransactionMethod}", transaction.MethodTrans ?? "N/A");
            htmlTemplate = htmlTemplate.Replace("{TransactionAmount}", transaction.MoneyTrans?.ToString("C") ?? "N/A");
            htmlTemplate = htmlTemplate.Replace("{TransactionStatus}", transactionStatus.ToString());
            htmlTemplate = htmlTemplate.Replace("{Fullname}", user.FullName.ToString() ?? "N/A");




            return htmlTemplate;
        }
    }
}
