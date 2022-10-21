using Calculator.Model.Models;
using System.Data;

namespace Calculator.Logic.Services
{
    public class CalculationService: ICalculationService
    {
        private readonly CalculatorView _model;

        public CalculationService(CalculatorView model)
        {
            _model = model;
        }

        public string GetResultString()
        {
            return _model.Result;
        }

        public void GetResult()
        {
            _model.Expression = _model.Result;

            UpdateExpression(_model.Expression);

            Calculate(_model.Expression);
        }

        public void Calculate(string expression)
        {
            if (string.IsNullOrEmpty(expression) || char.IsSymbol(expression[^1]) || char.IsPunctuation(expression[^1]))
                return;

            if (expression.StartsWith(CalculatorSymbols.MINUS))
                expression = "0-" + expression[1..];

            if (expression.Contains("/0"))
            {
                UpdateResult(CalculatorSymbols.NOT_DIVISION_ZERO);
                return;
            }

            string result = default;

            try
            {
                result = new DataTable().Compute(expression, "").ToString()!.Replace(',', '.');
            }
            catch
            {
                result = CalculatorSymbols.EXCEEDED;
            }

            if ((result.Length >= 8 && result.Contains('.')) || result == "∞" || result == "не число")
                result = CalculatorSymbols.EXCEEDED;

            UpdateResult(result);

            _model.Expression = string.Empty;
        }

        public void ClearOutputField()
        {
            if (_model.Result.Length == 0)
                return;

            _model.Result = string.Empty;
        }

        

        public void PlusMinus()
        {
            if (_model.Result == CalculatorSymbols.EXCEEDED || _model.Result == CalculatorSymbols.NOT_DIVISION_ZERO)
                return;

            double number = double.Parse(_model.Result);

            UpdateResult((number * (-1)).ToString());
        }

        private void UpdateResult(string value)
        {
            _model.Result = value;
        }

        private void UpdateExpression(string expression)
        {
            _model.Expression = expression;
        }
    }
}
