namespace Api.Models.Configs;

public class BillingConfig
{
    public static string SectionKey => "Billing";
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, Plan> Plans { get; set; } = new();
    public double DailyBaseCost { get; set; }
    public double RangeBaseCost { get; set; }

    public class Plan
    {
        public VehicleType Type { get; set; }
        public double DailyIncrement { get; set; }
        public double RangeIncrement { get; set; }
        public double OtherIncrement { get; set; }
    }
}