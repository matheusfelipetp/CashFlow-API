using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Extensions
{
    public static class PaymentTypeExtensions
    {
        public static string PaymentTypesToString(this PaymentsType? paymentType)
        {
            return paymentType switch
            {
                PaymentsType.Cash => ResourcePaymentTypesMessages.CASH,
                PaymentsType.CreditCard => ResourcePaymentTypesMessages.CREDIT_CARD,
                PaymentsType.DebitCard => ResourcePaymentTypesMessages.DEBIT_CARD,
                PaymentsType.EletronicTransfer => ResourcePaymentTypesMessages.ELETRONIC_TRANSFER,
                _ => string.Empty
            };
        }
    }
}
