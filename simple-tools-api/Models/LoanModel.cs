using System.Text.Json.Serialization;

namespace simple_tools_api.Models
{
    public class LoanModel
    {
        public string Principal { get; set; }

        public string Rate { get; set; }

        public string Years { get; set; }
    }

    public class LoanRequestModel
    {
        [JsonPropertyName("principal")]
        public decimal Principal { get; set; }

        [JsonPropertyName("interest_rate")]
        public double InterestRate { get; set; }

        [JsonPropertyName("years")]
        public int Years { get; set; }

        // Remove Flag or 400 error is returned stating it's required
        //public string Flag { get; set; }
    }
}
