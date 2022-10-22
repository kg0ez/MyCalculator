using Calculator.Logic.Services;
using Calculator.Model.Models;


namespace Calculator.Tests
{
    public class MyCalculatorTests
    {
        private static readonly CalculatorView _model = new();
        private readonly CalculationService _calculationService = new(_model);
        private readonly ButtonPresseService _buttonPresseService = new(_model);

        public MyCalculatorTests()
        {
            CalculatorView.ResultChanged += SomeEventAction;
        }

        private void SomeEventAction() { }

        /// <summary>
        /// Тестирование результатов расчёта
        /// </summary>
        /// <param name="expression"> Пример </param>
        /// <param name="expectedResult"> Ожидаемый результат </param>
        [Theory]
        [InlineData("2+2*2", "8")]
        [InlineData("5*4", "20")]
        [InlineData("20/2+9", "19")]
        [InlineData("12*5", "60")]
        [InlineData("2+2/2", "2")]
        [InlineData("9/0", "NOT ÷ 0")]
        [InlineData("999999999+1", "EXCEEDED")]
        public void Calculate_ResultCalculation(string expression, string expectedResult)
        {
            // Arrange
            _model.Result = expectedResult;

            // Act
            string result = _calculationService.Equals(expression);

            // Assert
            Assert.Equal(result, _model.Result);
        }

        /// <summary>
        /// Тестирование удаление элементов при нажатии на кнопку
        /// </summary>
        [Theory]
        [InlineData("1+1")]
        public void Clear_EqualsToEmptyString(string expression)
        {
            // Arrange
            _model.Result = expression;

            // Act
            _calculationService.Clear();

            // Assert
            Assert.Empty(_model.Result);
        }

        /// <summary>
        /// Тестирование составления уравнения
        /// </summary>
        /// <param name="operation"> Operation value </param>
        [Theory]
        [InlineData("12", "/", "12/")]
        [InlineData("0", "*", "0*")]
        [InlineData("0.45", "-", "0.45-")]
        [InlineData("13+2", "+", "13+2+")]
        public void ActionButton_EnteredActionSymbol(string expression, string operation, string expected)
        {
            // Arrange
            _model.Result = expression;

            // Act
            _buttonPresseService.ActionButtonPressed(operation);

            // Assert
            Assert.EndsWith(expected, _model.Expression);
        }
    }
}
