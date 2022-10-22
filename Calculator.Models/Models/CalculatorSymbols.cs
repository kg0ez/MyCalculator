namespace Calculator.Model.Models
{
    public static class CalculatorSymbols
    {
        public const string ZERO = "0";
        public const char MINUS = '-';
        public const string EXCEEDED = "EXCEEDED";
        public const string NOT_DIVISION_ZERO = "NOT ÷ 0";
        public static readonly List<char> ACTIONS = new List<char> { '-', '+', '/', '*' };
    }
}
