﻿using Calculator.Model.Models;
using System.Data;

namespace Calculator.Logic.Services
{
    public class CalculationService: ICalculationService
    {
        private readonly CalculatorView _calculatorView;

        public CalculationService(CalculatorView model)
        {
            _calculatorView = model;
        }

        public string GetResultString()
        {
            return _calculatorView.Result;
        }

        public void GetResult()
        {
            _calculatorView.Expression = _calculatorView.Result;

            UpdateExpression(_calculatorView.Expression);

            Calculate(_calculatorView.Expression);
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

            _calculatorView.Expression = string.Empty;
        }

        public void Clear()
        {
            if (_calculatorView.Result.Length == 0)
                return;

            _calculatorView.Result = string.Empty;
        }

        

        public void PlusMinus()
        {
            if (_calculatorView.Result == CalculatorSymbols.EXCEEDED || _calculatorView.Result == CalculatorSymbols.NOT_DIVISION_ZERO)
                return;

            double number = double.Parse(_calculatorView.Result);

            UpdateResult((number * (-1)).ToString());
        }

        private void UpdateResult(string value)
        {
            _calculatorView.Result = string.Empty;
            _calculatorView.Result = value;
        }

        private void UpdateExpression(string expression)
        {
            _calculatorView.Expression = string.Empty;
            _calculatorView.Expression = expression;
        }
    }
}
