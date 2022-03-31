using Order.Models;

namespace Order.Dtos
{
    public record OrderCmdDto(
        string Type, 
        string? ShippingAddress=null,
        decimal? ShippingFees=null,
        string? TransactionNumber=null,
        decimal? Amount=null,
        string? TrackingNumber=null
    )
    {
        public static OrderCmdDto FromOrderCmd(OrderCmd cmd)
        {
            if (cmd is OrderCmdSetInfos)
            {
                var cmdSI = (OrderCmdSetInfos)cmd;
                return new OrderCmdDto(
                    "set-infos",
                    ShippingAddress: cmdSI.ShippingAddress,
                    ShippingFees: cmdSI.ShippingFees
                );
            }
            else if (cmd is OrderCmdPay)
            {
                var cmdPay = (OrderCmdPay)cmd;
                return new OrderCmdDto(
                    "pay",
                    Amount: cmdPay.Amount,
                    TransactionNumber: cmdPay.Transaction
                );
            }
            else if (cmd is OrderCmdShip)
            {
                var cmdShip = (OrderCmdShip)cmd;
                return new OrderCmdDto(
                    "ship",
                    TransactionNumber: cmdShip.TrackingNumber
                );
            }
            else
                throw new NotSupportedException("Unknown Ordercmd");
        }
        public OrderCmd ToOrderCmd()
        {
            switch(Type)
            {
                case "set-infos":
                    if(ShippingAddress==null || !ShippingFees.HasValue)
                    {
                        throw new InvalidOperationException("Shipping information missing");
                    }
                    return new OrderCmdSetInfos()
                    {
                        ShippingAddress = ShippingAddress,
                        ShippingFees = ShippingFees.Value
                    };
                case "pay":
                    if (TransactionNumber == null || !Amount.HasValue)
                    {
                        throw new InvalidOperationException("Paiement information missing");
                    }
                    return new OrderCmdPay()
                    {
                        Transaction = TransactionNumber,
                        Amount = Amount.Value,

                    };
                case "ship":
                    if (TrackingNumber == null)
                    {
                        throw new InvalidOperationException("Tracking information missing");
                    }
                    return new OrderCmdShip()
                    {
                        TrackingNumber = TrackingNumber
                    };
                default:
                    throw new InvalidOperationException("Unknown order action");
            }
        }
    }
}
