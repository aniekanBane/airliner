namespace Domain.Entities.ARS.FlightAggregate;

public static partial class FareTemplates
{
    public static class Business
    {
        public static readonly ImmutableArray<string> Lite = [
            "2 hand baggage and 1 personal item (18kg total)*", 
            "1 checked baggage (31kg)",
            "No free seat selection before check-in",
            "Lounge access included",
            "Priority benefits*",
            "Ticket changes permitted (NGN 50000 fee + possible fare difference)",
            "Non-refundable"
        ];

        public static readonly ImmutableArray<string> Standard = [
            "2 hand baggage and 1 personal item (18kg total)*",
            "2 checked baggage (31kg each)",
            "Seat selection before check-in",
            "Lounge access included",
            "Priority benefits*",
            "Ticket changes permitted (only pay possible fare difference)", 
            "Non-refundable"
        ];

        public static readonly ImmutableArray<string> Flex = [
            "2 hand baggage and 1 personal item (18kg total)*",
            "2 checked baggage (31kg each)",
            "Seat selection before check-in",
            "Lounge access included",
            "Priority benefits*",
            "Ticket changes permitted (only pay possible fare difference)", 
            "Refundable if you cancel before the 1st flight in your trip"
        ];

        public static readonly ImmutableArray<ImmutableArray<string>> All = [
            Lite, Standard, Flex
        ];
    }
}
