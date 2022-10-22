using Calculator.Model.Models;

namespace Calculator.Logic.Services
{
    public class ButtonPresseService
    {
        private readonly CalculatorView _calculatorView;
        public ButtonPresseService(CalculatorView calculatorView)
        {
            _calculatorView = calculatorView;
        }
        public void NumberButtonPressed(string input)
        {
            if (string.IsNullOrEmpty(input) || !char.IsDigit(input[0]) || _calculatorView.Result.Length > 8)
                return;

            if (_calculatorView.Result == CalculatorSymbols.EXCEEDED || _calculatorView.Result == CalculatorSymbols.NOT_DIVISION_ZERO)
            {
                _calculatorView.Result = string.Empty;
                _calculatorView.Result = input;
                return;
            }

            _calculatorView.Result = input;
        }

        public void ActionButtonPressed(string input)
        {
            if (_calculatorView.Result == CalculatorSymbols.EXCEEDED || _calculatorView.Result == CalculatorSymbols.NOT_DIVISION_ZERO)
                return;

            if (_calculatorView.Result.Length == 0)
            {
                if (_calculatorView.Expression.Length > 0)
                    foreach (var action in CalculatorSymbols.ACTIONS)
                        if (_calculatorView.Expression[_calculatorView.Expression.Length - 1] == action)
                        {
                            string equation = _calculatorView.Expression.Substring(0, _calculatorView.Expression.Length - 1);
                            _calculatorView.Expression = string.Empty;
                            _calculatorView.Expression = equation + input;
                            return;
                        }

                _calculatorView.Result.Append('0');
                return;
            }

            if ((char.IsSymbol(_calculatorView.Result[^1]) || char.IsPunctuation(_calculatorView.Result[^1])) && input != "-")
                _calculatorView.Result.Remove(_calculatorView.Result.Length - 1, 1);

            _calculatorView.Result = input;

            SaveAndClearOutputString();
        }
        private void SaveAndClearOutputString()
        {
            if (string.IsNullOrEmpty(_calculatorView.Result))
                return;

            _calculatorView.Expression = _calculatorView.Result;
            _calculatorView.Result = string.Empty;
        }
    }
}
