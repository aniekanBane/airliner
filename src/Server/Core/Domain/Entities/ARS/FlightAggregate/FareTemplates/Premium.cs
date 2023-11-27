namespace Domain.Entities.ARS.FlightAggregate;

public static partial class FareTemplates
{
    // public static readonly ImmutableDictionary<string, ImmutableArray<string>> Premium = 
    //     new Dictionary<string, ImmutableArray<string>> {
    //         {"Lite", PremiumOptions.Lite},
    //         {"Standard", PremiumOptions.Standard},
    //         {"Flex", PremiumOptions.Flex},
    // }.ToImmutableDictionary();

    public static class PremiumOptions
    {
        public static readonly ImmutableArray<string> Lite = [
            HandLuggauge.Option2,
            CheckInLuggauge.Option2,
            SeatSelection.Option1,
            Priority.Option1,
            TicketChange.Option2,
            TicketRefund.Option1
        ];

        public static readonly ImmutableArray<string> Standard = [
            HandLuggauge.Option2,
            CheckInLuggauge.Option3,
            SeatSelection.Option3,
            Priority.Option1,
            TicketChange.Option1,
            TicketRefund.Option2
        ];

        public static readonly ImmutableArray<string> Flex = [
            HandLuggauge.Option2,
            CheckInLuggauge.Option3,
            SeatSelection.Option3,
            Priority.Option1,
            TicketChange.Option1,
            TicketRefund.Option3
        ];

        public static readonly ImmutableArray<ImmutableArray<string>> All = [
            Lite, Standard, Flex
        ];
    }
}
