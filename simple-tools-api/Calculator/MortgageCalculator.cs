namespace simple_tools_api.Calculator
{
    public class MortgageCalculator
    {
        public static decimal CalculateMonthlyMortgagePayment(decimal A, double r, int n)
        {
            // Convert annual interest rate to monthly
            r = (double)(r / 100) / 12;
            // Number of monthly payments
            n *= 12;

            var pow = (double)Math.Pow(1 + r, n);

            var P = A * (decimal)(r * pow) / (decimal)(pow - 1);

            return P;
        }
    }
}
