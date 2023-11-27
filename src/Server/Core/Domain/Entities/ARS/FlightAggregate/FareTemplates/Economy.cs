namespace Domain.Entities.ARS.FlightAggregate;

public static partial class FareTemplates
{
    // public static readonly ImmutableDictionary<string, ImmutableArray<string>> Economy = 
    //     new Dictionary<string, ImmutableArray<string>> {
    //         {"Saver", EconomyOptions.Saver}, 
    //         {"Lite", EconomyOptions.Lite},
    //         {"Standard", EconomyOptions.Standard},
    //         {"Flex", EconomyOptions.Flex},
    //     }.ToImmutableDictionary();

    public static class EconomyOptions
    {
        public static readonly ImmutableArray<string> Saver = [
            HandLuggauge.Option1,
            CheckInLuggauge.Option1,
            SeatSelection.Option1,
            TicketChange.Option2,
            TicketRefund.Option1
        ];

        public static readonly ImmutableArray<string> Lite = [
            HandLuggauge.Option1,
            CheckInLuggauge.Option2,
            SeatSelection.Option1,
            TicketChange.Option2,
            TicketRefund.Option1
        ];

        public static readonly ImmutableArray<string> Standard = [
            HandLuggauge.Option1,
            CheckInLuggauge.Option3,
            SeatSelection.Option3,
            TicketChange.Option1, 
            TicketRefund.Option2
        ];

        public static readonly ImmutableArray<string> Flex = [
            HandLuggauge.Option1,
            CheckInLuggauge.Option3,
            SeatSelection.Option3,
            TicketChange.Option1, 
            TicketRefund.Option3
        ];

        public static readonly ImmutableArray<ImmutableArray<string>> All = [
            Saver, Lite, Standard, Flex
        ];
    }
}
