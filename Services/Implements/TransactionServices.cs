using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities.Models;
using Entities.RequestObject;
using Entities.ResponseObject;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Repositories.Intefaces;
using Services.Interfaces;
using System.Globalization;
using System.Net.Mail;
using System.Net;

namespace Services.Implements
{
    public class TransactionServices : ITransactionServices
    {
        private readonly IRepositoryManager _repositoryManager;

        public TransactionServices(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<int> CreateForBuySlot(TransactionCreateInfo info)
        {


            var listSlot = new List<SlotPost>();
            decimal price = 0;

            foreach (PostSlot item in info.postSlot)
            {
                foreach (string slot in item.slot)
                {
                    var slotpost = await _repositoryManager.SlotPost.FindByCondition(x => x.IdPost == info.IdPost && x.SlotDate == item.DateSlot && x.ContextPost == slot, false).FirstOrDefaultAsync();
                    slotpost.Status = (int)SlotStatus.Pending;
                    if (slotpost != null)
                    {
                        listSlot.Add(slotpost);
                        price += slotpost.SlotPrice;
                    }
                }
            }

            if (listSlot.Count() == 0)
            {
                return 0;
            }

            var deadLine = DateTime.MinValue;

            var tran = new Transaction
            {
                DeadLine = deadLine.AddDays(1),
                UserId = info.IdUser,
                MoneyTrans = price,
                MethodTrans = "buy_slot",
                TypeTrans = "buy _slot",
                TimeTrans = DateTime.UtcNow.AddHours(7),
                Status = (int)TransactionStatus.Booked,
                Idpost = info.IdPost,
            };
            _repositoryManager.Transaction.Create(tran);
            await _repositoryManager.SaveAsync();

            foreach (var slot in listSlot)
            {
                Slot newSlot = new Slot();
                newSlot.IdSlot = slot.IdSlot;
                newSlot.IdPost = slot.IdPost;
                newSlot.TransactionId = tran.Id;
                _repositoryManager.Slot.Create(newSlot);
                await _repositoryManager.SaveAsync();

            }

            var adminId = 2;

            var UsertranHistory = new HistoryTransaction
            {
                UserId = tran.UserId,
                IdTransaction = tran.Id,
                MoneyTrans = tran.MoneyTrans,
                Status = true,
                Date = DateTime.UtcNow,

            };
            _repositoryManager.HistoryTransaction.Create(UsertranHistory);


            await _repositoryManager.SaveAsync();
            TransactionDetailsEmail(tran);
            return tran.Id;
        }

        public async Task DeleteSlot(int transaction_id)
        {
            var slots = _repositoryManager.Slot.FindByCondition(x => x.TransactionId == transaction_id, true).ToList();
            foreach (var slot in slots)
            {
                _repositoryManager.Slot.Delete(slot);
            }
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteTran(int transaction_id)
        {
            var transaction = await _repositoryManager.Transaction.FindByCondition(x => x.Id == transaction_id, true).FirstOrDefaultAsync();
            if (transaction != null)
            {
                transaction.Status = (int)TransactionStatus.Canceled;
                await _repositoryManager.SaveAsync();
            }
        }

        public bool ExistTran(int tran_id)
        {
            return _repositoryManager.Transaction.FindByCondition(x => x.Id == tran_id, false).FirstOrDefault() != null;
        }

        public async Task<TransactionDetail> GetDetail(int transaction_id)
        {
            double dCancelDate = 0;
            var tran = await _repositoryManager.Transaction.FindByCondition(x => x.Id == transaction_id, false)
                .Select(x => new TransactionDetail
                {
                    Id = x.Id,
                    BuyerName = x.User.FullName,
                    PayTime = x.TimeTrans.Value.ToString("dd/MM/yyyy HH:mm"),
                    SlotCount = x.Slots.Count,
                    Total = x.MoneyTrans.Value.ToString(),
                    TranStatus = (TransactionStatus)x.Status
                }).FirstOrDefaultAsync();

            if (tran != null)
            {

                var slot = await _repositoryManager.Slot.FindByCondition(x => x.TransactionId == tran.Id, false)
                    .Include(x => x.IdSlotNavigation).ThenInclude(x => x.IdPostNavigation).ThenInclude(x => x.IdUserToNavigation).FirstOrDefaultAsync();
                if (slot != null && slot.IdSlotNavigation != null)
                {
                    tran.Post = new PostInTransaction
                    {
                        Address = slot.IdSlotNavigation.IdPostNavigation.AddressSlot,
                        Id = slot.IdSlotNavigation.IdPostNavigation.Id,
                        ImageUrls = slot.IdSlotNavigation.IdPostNavigation.ImageUrls.Split(";").ToList(),
                        Title = slot.IdSlotNavigation.IdPostNavigation.Title,
                        TitleImage = slot.IdSlotNavigation.IdPostNavigation.ImgUrl,
                        CategorySlot = slot.IdSlotNavigation.IdPostNavigation.CategorySlot,
                        CreateUser = slot.IdSlotNavigation.IdPostNavigation.IdUserToNavigation.FullName

                    };
                    var bookedInfos = new List<PostSlot>();
                    var trans = await _repositoryManager.Transaction
                                   .FindByCondition(x => x.Id == transaction_id, false).Include(x => x.Slots).ThenInclude(x => x.IdSlotNavigation).FirstOrDefaultAsync();
                    foreach (var infoStr in trans.Slots)
                    {
                        var existingSlot = bookedInfos.FirstOrDefault(slot => slot.DateSlot == infoStr.IdSlotNavigation.SlotDate);
                        if (existingSlot != null)
                        {
                            existingSlot.slot.Add(infoStr.IdSlotNavigation.ContextPost);
                        }
                        else
                        {
                            var newSlot = new PostSlot
                            {
                                DateSlot = infoStr.IdSlotNavigation.SlotDate,
                                slot = new List<string> { infoStr.IdSlotNavigation.ContextPost },
                                PricePerSlot = infoStr.IdSlotNavigation.SlotPrice.ToString(),

                            };
                            bookedInfos.Add(newSlot);
                        }
                        tran.Post.Slots = bookedInfos;
                    }

                    tran.IsCancel = false;
                    var slots = tran.Post.Slots.FirstOrDefault();
                    if (slots != null)
                    {
                        string time = slots.slot.FirstOrDefault().Split('h').FirstOrDefault();

                        DateTime dateTimes = DateTime.ParseExact(slots.DateSlot, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(int.Parse(time));
                        if ((dateTimes - DateTime.UtcNow).TotalHours > 24)
                        {
                            tran.IsCancel = true;
                        }
                    }


                }
                return tran;
            }
            return new TransactionDetail { Id = 0, };
        }


        public async Task<Transaction> GetTransaction(int transaction_id)
        {

            var tran = await _repositoryManager.Transaction.FindByCondition(x => x.Id == transaction_id, false)
                .FirstOrDefaultAsync();
            return tran == null ? new Transaction { Id = 0 } : tran;
        }
        public bool TransactionDetailsEmail(Transaction transaction)
        {
            try
            {
                var user = _repositoryManager.User.FindByCondition(x => x.Id == transaction.UserId, false).FirstOrDefault();
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "langtusayonly@gmail.com";
                string smtpPassword = "idvb bbwu civw vbas";

                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    string fromEmail = "langtusayonly@gmail.com";
                    string subject = "Vietnam Badminton Court (VBC)";

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
                     <p>Chúng tôi xin gởi đến bạn chi tiết giao dịch có mã là :{TransactionId} </p>
 <p>Vui lòng xuất trình đơn hàng trên web hoặc email khi đến chơi để xác nhận ! </p>
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
                    transactionStatus = "Đặt sân thành công !";
                    break;
                case (int)TransactionStatus.Canceled:
                    transactionStatus = "Đã hủy !";
                    break;
                default:
                    transactionStatus = "Trạng thái không xác định";
                    break;
            }

            // Replace placeholders with actual values
            var user = _repositoryManager.User.FindByCondition(x => x.Id == transaction.UserId, false).FirstOrDefault();
            htmlTemplate = htmlTemplate.Replace("{TransactionId}", transaction.Id.ToString());
            htmlTemplate = htmlTemplate.Replace("{TransactionTime}", transaction.TimeTrans.ToString());
            htmlTemplate = htmlTemplate.Replace("{TransactionMethod}", transaction.MethodTrans ?? "N/A");
            CultureInfo vndCulture = new CultureInfo("vi-VN");
            htmlTemplate = htmlTemplate.Replace("{TransactionAmount}", transaction.MoneyTrans?.ToString("C0", vndCulture) ?? "N/A");
            htmlTemplate = htmlTemplate.Replace("{TransactionStatus}", transactionStatus.ToString());
            htmlTemplate = htmlTemplate.Replace("{Fullname}", user.FullName.ToString() ?? "N/A");




            return htmlTemplate;
        }

        public async Task<int> UpdateStatus(TransactionCreateInfo info)
        {
            int adminId = 2;
            foreach (PostSlot item in info.postSlot)
            {
                foreach (string slot in item.slot)
                {
                    var slotpost = await _repositoryManager.SlotPost.FindByCondition(x => x.IdPost == info.IdPost && x.SlotDate == item.DateSlot && x.ContextPost == slot, true).FirstOrDefaultAsync();
                    if (slotpost == null)
                    {
                        return -1;
                    }
                    if (slotpost.Status == (int)SlotStatus.Pending)
                    {
                        slotpost.Status = (int)SlotStatus.Checkin;
                        await _repositoryManager.SaveAsync();

                        var adminWallet = await _repositoryManager.Wallet.FindByCondition(x => x.UserId == adminId, true).FirstOrDefaultAsync();
                        //Create admin transaction
                        var admintrans = new Transaction
                        {
                            Id = 0,
                            DeadLine = null,
                            UserId = adminId,
                            MoneyTrans = slotpost.SlotPrice,
                            MethodTrans = "booking_free",
                            TypeTrans = "booking_free",
                            TimeTrans = DateTime.UtcNow.AddHours(7),
                            Status = (int)TransactionStatus.Complete,
                            Idpost = info.IdPost,
                        };
                        _repositoryManager.Transaction.Create(admintrans);
                        await _repositoryManager.SaveAsync();

                        if (adminWallet != null)
                        {
                            adminWallet.Balance += slotpost.SlotPrice;


                            _repositoryManager.HistoryWallet.Create(new HistoryWallet
                            {
                                Amount = slotpost.SlotPrice.ToString(),
                                UserId = adminId,
                                IdWallet = adminWallet.Id,
                                Status = (int)HistoryWalletStatus.Success,
                                Time = DateTime.UtcNow.AddHours(7),
                                Type = "Nhận tiền đặt sân của slot :  " + slotpost.IdSlot.ToString(),
                            });
                        }
                        await _repositoryManager.SaveAsync();
                        break;

                    }
                    else if (slotpost.Status == (int)SlotStatus.Checkin)
                    {
                        slotpost.Status = (int)SlotStatus.CheckOut;
                        await _repositoryManager.SaveAsync();
                        break;
                    }
                }
            }
            return 1;
        }
    }
}
