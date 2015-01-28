using iExchange.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Protocal
{
    public enum CommandType
    {
        None,
        TraderServer,
        Manager,
        TransactionServer
    }


    [DataContract]
    [KnownType(typeof(ChangeContentCommand))]
    public class Command
    {
        [DataMember]
        public CommandType Type { get; set; }

        [DataMember]
        public long Sequence { get; set; }
    }

    [DataContract]
    public sealed class ChangeContentCommand : Command
    {
        [DataMember]
        public string Content { get; set; }
    }

    [DataContract]
    public sealed class OrderRelaitonData
    {
        [DataMember]
        public Guid CloseOrderId { get; set; }

        [DataMember]
        public Guid OpenOrderId { get; set; }

        [DataMember]
        public decimal ClosedLot { get; set; }
    }


    [DataContract]
    public sealed class OrderData
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string OriginCode { get; set; }
        [DataMember]
        public string BlotterCode { get; set; }
        [DataMember]
        public bool IsOpen { get; set; }
        [DataMember]
        public bool IsBuy { get; set; }
        [DataMember]
        public string SetPrice { get; set; }
        [DataMember]
        public string SetPrice2 { get; set; }
        [DataMember]
        public string ExecutePrice { get; set; }
        [DataMember]
        public decimal Lot { get; set; }
        [DataMember]
        public decimal OriginalLot { get; set; }
        [DataMember]
        public decimal LotBalance { get; set; }
        [DataMember]
        public TradeOption TradeOption { get; set; }
        [DataMember]
        public int SetPriceMaxMovePips { get; set; }
        [DataMember]
        public int DQMaxMove { get; set; }
        [DataMember]
        public DateTime? PriceTimestamp { get; set; }
        [DataMember]
        public bool DisableAcceptLmtVariation { get; set; }

        [DataMember]
        public bool PriceIsQuote { get; set; }

        [DataMember]
        public List<OrderRelaitonData> OrderRelations { get; set; }
    }

    [DataContract]
    public sealed class TransactionData
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public Guid InstrumentId { get; set; }

        [DataMember]
        public Guid AccountId { get; set; }

        [DataMember]
        public TransactionType Type { get; set; }
        [DataMember]
        public TransactionSubType SubType { get; set; }
        [DataMember]
        public OrderType OrderType { get; set; }
        [DataMember]
        public ExpireType ExpireType { get; set; }
        [DataMember]
        public DateTime BeginTime { get; set; }
        [DataMember]
        public DateTime EndTime { get; set; }
        [DataMember]
        public DateTime SubmitTime { get; set; }
        [DataMember]
        public Guid SubmitorId { get; set; }
        [DataMember]
        public Guid? SourceOrderId { get; set; }
        [DataMember]
        public DateTime? SetPriceTimestamp { get; set; }
        [DataMember]
        public bool PlaceByRiskMonitor { get; set; }
        [DataMember]
        public bool FreePlacingPreCheck { get; set; }
        [DataMember]
        public bool FreeLmtVariationCheck { get; set; }

        [DataMember]
        public List<OrderData> Orders { get; set; }

    }


}
