using Domain.Enums;

namespace Utility;

public static class EnumUtils
{
    public static string ToFriendlyString(this OrderStatus status) =>
        status switch
        {
            OrderStatus.Pending => "Pending",
            OrderStatus.Approved => "Approved",
            OrderStatus.InProcess => "In Process",
            OrderStatus.Shipped => "Shipped",
            OrderStatus.Cancelled => "Cancelled",
            OrderStatus.Refunded => "Refunded",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };

    public static string ToFriendlyString(this PaymentStatus status) =>
        status switch
        {
            PaymentStatus.Pending => "Pending",
            PaymentStatus.Approved => "Approved",
            PaymentStatus.DelayedPayment => "Delayed Payment",
            PaymentStatus.Rejected => "Rejected",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}
