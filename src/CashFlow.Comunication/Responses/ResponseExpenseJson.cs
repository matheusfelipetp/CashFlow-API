using CashFlow.Comunication.Enums;

namespace CashFlow.Comunication.Responses
{
    public class ResponseExpenseJson
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentsType PaymentType { get; set; }
    }
}
