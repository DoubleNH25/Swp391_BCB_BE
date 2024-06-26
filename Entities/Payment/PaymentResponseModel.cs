using System.Collections.Generic;

namespace BadmintonMatching.Payment
{
    public class PaymentResponseModel
    {
        public string CurrentUserId { get; set; }
        public string OwnerId { get; set; }
        public string UserId { get; set; }
        public string BillId { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PayerId { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Customer { get; set; }
        public List<PaymentDetailModel> ListItems { get; set; }
        public string TotalAmount { get; set; }
        public string PaymentCode { get; set; }
        public string MessageResponse { get; set; }
    }
}