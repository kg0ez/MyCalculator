namespace Calculator.Logic.Services
{
    public interface ICalculationService
    {
        string GetResultString();
        void GetResult();
        void Calculate(string expression);
        void Clear();
        void PlusMinus();
    }
}
