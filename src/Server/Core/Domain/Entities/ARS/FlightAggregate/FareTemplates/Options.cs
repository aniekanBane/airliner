namespace Domain.Entities.ARS.FlightAggregate;

public static partial class FareTemplates
{
    public static class HandLuggauge
    {
        internal const string Option1 = "1 hand baggage and 1 personal item (9kg total)*";
        internal const string Option2 = "2 hand baggage and 1 personal item (12kg total)*";
        internal const string Option3 = "2 hand baggage and 1 personal item (18kg total)*";
        internal const string Option4 = "2 hand baggage and 2 personal item (20kg total)*";
        public readonly static ImmutableArray<string> All = [ Option1, Option2, Option3, Option4 ];
    }

    public static class CheckInLuggauge
    {
        internal const string Option1 = "No checked baggage";
        internal const string Option2 = "1 checked baggage (22kg)";
        internal const string Option3 = "2 checked baggage (22kg each)";
        internal const string Option4 = "1 checked baggage (31kg)";
        internal const string Option5 = "2 checked baggage (31kg each)";
        public readonly static ImmutableArray<string> All = [ 
            Option1, Option2, Option3, Option4, Option5 
        ];
    }

    public static class TicketChange
    {
        internal const string Option1 = "Ticket changes permitted (only pay possible fare difference)";
        internal const string Option2 = "Ticket changes permitted (fee + possible fare difference)";
        public readonly static ImmutableArray<string> All = [ Option1, Option2 ];
    }

    public static class TicketRefund
    {
        internal const string Option1 = "Non refundable";
        internal const string Option2 = "Refundable if you cancel 2 days before the 1st flight in your trip";
        internal const string Option3 = "Refundable if you cancel before the 1st flight in your trip";
        public readonly static ImmutableArray<string> All = [ 
            Option1, Option2, Option3
        ];
    }

    public static class SeatSelection
    {
        internal const string Option2 = "Front seat section";
        internal const string Option1 = "No free seat selection before check-in";
        internal const string Option3 = "Free Seat selection before check-in";
        public readonly static ImmutableArray<string> All = [ 
            Option1, Option2, Option3
        ];
    }

    public static class Priority
    {
        internal const string Option1 = "Priority benefits*";
        internal const string Option2 = "Lounge access inlcuded";
        public readonly static ImmutableArray<string> All = [ 
            Option1, Option2
        ];
    }
}
